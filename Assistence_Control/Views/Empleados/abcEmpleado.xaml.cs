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
        enum ACCION { INSERTAR = 1, ACTUALIZAR = 2, ELIMINAR = 3 };
        int estado = 0;
        List<Empleado> empleados = null;
        tcEmpleado empDAO;

        //Constructor
        public abcEmpleado()
        {
            this.InitializeComponent();
            empDAO = new tcEmpleado(App.uriServicio);
            cargarEmpleados();
        }
        //Metodos de carga
        private async void cargarEmpleados()
        {
            try
            {
                empleados = await empDAO.getAllEmpleados();
                DataGrid.ItemsSource = empleados;

            }
            catch (Exception ex)
            {
                await new MessageDialog("Error al obtener empleados.").ShowAsync();
            }        }
        //Eventos
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            estado =(int)ACCION.INSERTAR;
            if (gridAgregar.Visibility == Visibility.Collapsed)
            {
                gridAgregar.Visibility = Visibility.Visible;
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
                tbNombre.Text = empleadoSeleccionado.Nombre;
                tbApellidoPaterno.Text = empleadoSeleccionado.Apellido1;
                tbApellidoMaterno.Text = empleadoSeleccionado.Apellido2;
            }
            else
            {
                await new MessageDialog("Seleccione un empleado para editar", "Atención").ShowAsync();
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.ELIMINAR;
        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (estado)
                {
                    case (int)ACCION.ACTUALIZAR:
                        int selectedIndex = empleados.IndexOf(empleadoSeleccionado);
                        empleados[selectedIndex].EmpleadoId = int.Parse(tbNumeroEmpleado.Text);
                        empleados[selectedIndex].Nombre = tbNombre.Text;
                        empleados[selectedIndex].Apellido1 = tbApellidoMaterno.Text;
                        empleados[selectedIndex].Apellido2 = tbApellidoPaterno.Text;
                        DataGrid.ItemsSource = null;
                        DataGrid.ItemsSource = empleados;
                        DataGrid.SelectedItem = empleadoSeleccionado;
                        await new MessageDialog("Empleado actualizado correctamente!", "").ShowAsync();
                        break;
                    case (int)ACCION.INSERTAR:
                        obtenerDatosVista();
                        empDAO.Insertar(nuevoEmpleado);
                        await new MessageDialog("El empleado fue dado de alta correctamente.", "").ShowAsync();
                        break;
                }
                limpiarCampos();
            }
            catch (Exception ex)
            {

                throw;
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
                DataGrid.ItemsSource = matchingEmployees.ToList();
            }
        }
        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            EmpleadoItem empleado = (EmpleadoItem)args.SelectedItem;
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
        //Administracion
        private void limpiarCampos()
        {
            tbNumeroEmpleado.Text = string.Empty;
            tbNombre.Text = string.Empty;
            tbApellidoPaterno.Text = string.Empty;
            tbApellidoMaterno.Text = string.Empty;
            empleadoSeleccionado = null;
        }
        private async void obtenerDatosVista()
        {
            if (await validarCampos())
            {
                nuevoEmpleado = new Empleado()
                {
                    Nombre = tbNombre.Text,
                    Apellido1 = tbApellidoPaterno.Text,
                    Apellido2 = tbApellidoMaterno.Text,
                    EmpleadoId = int.Parse(tbNumeroEmpleado.Text),
                    Asistencia = null,
                    Edad = "25",
                    EmpleadoArea = null,
                    EmpleadoPermiso = null,
                    FechaHoraRegistro = DateTime.Now,
                    UsuarioRegistro = 1
                    
                };
            }
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
            return true;
        }
    }
}
