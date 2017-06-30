using Assistance_ControlBLL.TablesClasses;
using AssistanceControl_BLL;
using AssistanceControl_BLL.AssistanceService;
using Assistence_Control.Utilerias.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Assistence_Control.Views.Empleados
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class abcEmpleado : Page
    {
        //Variables Globales
        Empleado empleadoSeleccionado = null;
        Empleado nuevoEmpleado = null;
        List<Area> areas = null;
        tcArea areaDAO;
        enum ACCION { INSERTAR = 1, ACTUALIZAR = 2, ELIMINAR = 3 };
        int estado = 0;
        List<Empleado> empleados = null;
        tcEmpleado empDAO;

        //Constructor
        public abcEmpleado()
        {
            this.InitializeComponent();
            empDAO = new tcEmpleado(App.uriServicio);
            areaDAO = new tcArea(App.uriServicio);
            cargarEmpleados();
            cargarAreas();
        }
        //Metodos de carga
        private async void cargarEmpleados()
        {
            try
            {
                empleados = await empDAO.getAllEmpleados();
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = empleados.OrderBy(o => o. ApellidoPaterno).ThenBy(o => o.ApellidoMaterno).ThenBy(o => o.Nombre).ToList();
            }
            catch (Exception ex)
            {
                await new MessageDialog("Error al obtener empleados.").ShowAsync();
            }
        }
        private async void cargarAreas()
        {
            areas = await areaDAO.getAllAreas();
            cbAreas.ItemsSource = areas;
        }

        //Eventos
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.INSERTAR;
            if (gridAgregar.Visibility == Visibility.Collapsed)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbNumeroEmpleado.Focus(FocusState.Programmatic);
                tbNumeroEmpleado.IsReadOnly = false;

            }
            else
            {
                limpiarCampos();
            }
        }
        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.ACTUALIZAR;
            empleadoSeleccionado = (Empleado)DataGrid.SelectedItem;
            if (empleadoSeleccionado != null)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbNumeroEmpleado.Text = empleadoSeleccionado.EmpleadoId.ToString();
                tbNumeroEmpleado.IsReadOnly = true;
                tbNombre.Text = empleadoSeleccionado.Nombre;
                tbApellidoPaterno.Text = empleadoSeleccionado.ApellidoPaterno;
                tbApellidoMaterno.Text = empleadoSeleccionado.ApellidoMaterno;
                tbCurp.Text = empleadoSeleccionado.CURP;
                tbRfc.Text = empleadoSeleccionado.RFC;
                cbAreas.SelectedItem = areas.Where(w => w.AreaId == empleadoSeleccionado.AreaId).FirstOrDefault();
                dpNacimiento.Date = Convert.ToDateTime(empleadoSeleccionado.FechaNacimiento);
                dpIngreso.Date = empleadoSeleccionado.FechaIngreso;
            }
            else
            {
                await new MessageDialog("Seleccione un empleado para editar", "Atención").ShowAsync();
            }
        }
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            prLoading.IsActive = true;
            try
            {
                if (empleadoSeleccionado != null)
                {
                    estado = (int)ACCION.ELIMINAR;
                    MessageDialog messageDialog = new MessageDialog("Esta seguro de que desea eliminar a este empleado?", "Aviso");
                    messageDialog.Commands.Add(new UICommand("Si", async (command)  => 
                    {
                        await empDAO.Eliminar(empleadoSeleccionado);
                        cargarEmpleados();

                    }));
                    messageDialog.Commands.Add(new UICommand("No", (command) =>
                    {
                        return;
                    }));
                    await messageDialog.ShowAsync();
                }
                else
                {
                    await new MessageDialog("Seleccione un empleado.").ShowAsync();
                }

            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            finally
            {
                prLoading.IsActive = false;
            }
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                prLoading.IsActive = true;
                switch (estado)
                {
                    case (int)ACCION.ACTUALIZAR:
                        if (await obtenerDatosVista())
                        {
                            await empDAO.Actualizar(nuevoEmpleado);
                            cargarEmpleados();
                            DataGrid.SelectedItem = empleadoSeleccionado;
                            await new MessageDialog("Empleado actualizado correctamente!", "").ShowAsync();
                            limpiarCampos();
                        }
                        break;
                    case (int)ACCION.INSERTAR:
                        if (await obtenerDatosVista())
                        {
                            await empDAO.Insertar(nuevoEmpleado);
                            cargarEmpleados();
                            await new MessageDialog("El empleado fue dado de alta correctamente.", "").ShowAsync();
                            limpiarCampos();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            finally
            {
                prLoading.IsActive = false;
            }
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            gridAgregar.Visibility = Visibility.Collapsed;
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = dataset;
                var matchingEmployees = Assistance_Control.Utilerias.Utils.obtenerEmpleadosBusqueda(empleados, sender.Text);
                sender.ItemsSource = matchingEmployees.ToList();
                DataGrid.ItemsSource = null;
                if (matchingEmployees.ToList().Count > 1)
                {
                    DataGrid.ItemsSource = matchingEmployees.OrderBy(o => o. ApellidoPaterno).ThenBy(o => o.ApellidoMaterno).ThenBy(o => o.Nombre).ToList();
                }
                else if (matchingEmployees.ToList().Count == 1)
                {
                    DataGrid.ItemsSource = matchingEmployees.OrderBy(o => o. ApellidoPaterno).ThenBy(o => o.ApellidoMaterno).ThenBy(o => o.Nombre).ToList();
                    DataGrid.SelectedItem = matchingEmployees.ToList().FirstOrDefault();
                }
            }
        }
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Empleado empleado = (Empleado)args.SelectedItem;
            DataGrid.SelectedItem = empleado;
            sender.Text = empleado.Nombre;
        }
        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }
        private void numeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            string val = txt.Text;
            double aux = 0.0;
            if (string.IsNullOrEmpty(val))
            {
                return;
            }
            if (!double.TryParse(val, out aux))
            {
                if (!char.IsNumber(val[val.Length - 1]))
                {
                    val = val.Remove(val.Length - 1);
                }
                else if (!char.IsNumber(val[0]))
                {
                    val = val.Remove(0, 1);
                }
                txt.Text = val;
                txt.SelectionStart = val.Length;
                //TODO: AGEGAR ETIQUETA "SOLO SE PERMITEN NUMEROS"
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyToolkit.Controls.DataGrid dg = (MyToolkit.Controls.DataGrid)sender;
            empleadoSeleccionado = (Empleado)dg.SelectedItem;
        }
        //Administracion
        private void limpiarCampos()
        {
            tbNumeroEmpleado.Text = string.Empty;
            tbNombre.Text = string.Empty;
            tbApellidoPaterno.Text = string.Empty;
            tbApellidoMaterno.Text = string.Empty;
            tbCurp.Text = string.Empty;
            tbRfc.Text = string.Empty;
            empleadoSeleccionado = null;
            DataGrid.SelectedItem = null;
            dpIngreso.Date = DateTime.Now;
            dpNacimiento.Date = DateTime.Now;
            cbAreas.SelectedItem = null;
            tbNumeroEmpleado.IsReadOnly = true;
        }
        private async Task<bool> obtenerDatosVista()
        {
            if (await validarCampos())
            {
                Area area = (Area)cbAreas.SelectedItem;
                nuevoEmpleado = new Empleado()
                {
                    Nombre = tbNombre.Text.ToUpper(),
                    ApellidoPaterno = tbApellidoPaterno.Text.ToUpper(),
                    ApellidoMaterno = tbApellidoMaterno.Text.ToUpper(),
                    EmpleadoId = int.Parse(tbNumeroEmpleado.Text),
                    CURP = tbCurp.Text,
                    Area = area,
                    RFC = tbRfc.Text,
                    AreaId = area.AreaId,
                    FechaHoraRegistro = DateTime.Now,
                    UsuarioRegistro = App.usuarioAutentificado.UsuarioId,
                    Estatus = 1,
                    FechaIngreso = DateTime.Now,
                    FechaNacimiento = dpNacimiento.Date.ToString("d")
                };
                return true;
            }
            return false;
        }
        private async Task<bool> validarCampos()
        {
            if (string.IsNullOrWhiteSpace(tbNumeroEmpleado.Text))
            {
                await new MessageDialog("Capture el numero de empleado.").ShowAsync();
                tbNumeroEmpleado.Focus(FocusState.Programmatic);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbNombre.Text))
            {
                await new MessageDialog("Capture el nombre.").ShowAsync();
                tbNombre.Focus(FocusState.Programmatic);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbApellidoPaterno.Text))
            {
                await new MessageDialog("Capture el apellido paterno.").ShowAsync();
                tbApellidoPaterno.Focus(FocusState.Programmatic);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbApellidoMaterno.Text))
            {
                await new MessageDialog("Capture el apellido materno.").ShowAsync();
                tbApellidoMaterno.Focus(FocusState.Programmatic);
                return false;
            }
            else if (cbAreas.SelectedItem == null)
            {
                await new MessageDialog("Seleccione un area.").ShowAsync();
                cbAreas.Focus(FocusState.Programmatic);
                return false;
            }
            return true;
        }
    }
}
