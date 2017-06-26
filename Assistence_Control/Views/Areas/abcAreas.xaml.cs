using Assistance_ControlBLL.TablesClasses;
using AssistanceControl_BLL.AssistanceService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Assistence_Control.Views.Areas
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class abcAreas : Page
    {
        //Variables Globales
        Area areaSeleccionada = null;
        Area nuevaArea = null;
        List<Area> areas = null;
        tcArea areaDAO;
        enum ACCION { INSERTAR = 1, ACTUALIZAR = 2, ELIMINAR = 3 };
        int estado = 0;
        //Constructor
        public abcAreas()
        {
            this.InitializeComponent();
            areaDAO = new tcArea(App.uriServicio);
            cargarAreas();
        }
        //Metodos de carga
        private async void cargarAreas()
        {
            areas = await areaDAO.getAllAreas();
            DataGrid.ItemsSource = areas;
        }
        //Eventos
        private async void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.INSERTAR;

            tbClave.Text = (await getNextAreaId()).ToString();
            if (gridAgregar.Visibility == Visibility.Collapsed)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbNombreArea.Focus(FocusState.Programmatic);
            }
            else
            {
                limpiarCampos();
            }
        }
        private void tbNombreArea_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = tb.Text.ToUpper();
            tb.SelectionStart = tb.Text.Length;
            if (e.Key == VirtualKey.Space)
            {
                tb.Text = tb.Text.Replace(' ', '_');
                tb.SelectionStart = tb.Text.Length;
            }
        }
        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.ACTUALIZAR;
            areaSeleccionada = (Area)DataGrid.SelectedItem;
            if (areaSeleccionada != null)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbClave.Text = areaSeleccionada.AreaId.ToString();
                tbNombreArea.Text = areaSeleccionada.Nombre;
                tbDescripcion.Text = areaSeleccionada.Descripcion;
            }
            else
            {
                await new MessageDialog("Seleccione un area para editar", "Atención").ShowAsync();
            }
        }
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            prLoading.IsActive = true;
            try
            {
                if (areaSeleccionada != null)
                {
                    estado = (int)ACCION.ELIMINAR;
                    MessageDialog messageDialog = new MessageDialog("Esta seguro de que desea eliminar el area seleccionada?", "Aviso");
                    messageDialog.Commands.Add(new UICommand("Si", (command) =>
                    {
                        areaDAO.Eliminar(areaSeleccionada);
                    }));
                    messageDialog.Commands.Add(new UICommand("No", (command) =>
                    {
                        return;
                    }));
                    await messageDialog.ShowAsync();
                }
                else
                {
                    await new MessageDialog("Seleccione un area.").ShowAsync();
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
                            await areaDAO.Actualizar(nuevaArea);
                            cargarAreas();
                            DataGrid.SelectedItem = areaSeleccionada;
                            await new MessageDialog("Area actualizada correctamente!", "").ShowAsync();
                        }
                        break;
                    case (int)ACCION.INSERTAR:
                        if (await obtenerDatosVista())
                        {
                            await areaDAO.Insertar(nuevaArea);
                            cargarAreas();
                            await new MessageDialog("El area fue creada correctamente.", "").ShowAsync();
                        }
                        break;
                }
                limpiarCampos();
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
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyToolkit.Controls.DataGrid dg = (MyToolkit.Controls.DataGrid)sender;
            areaSeleccionada = (Area)dg.SelectedItem;
        }
        //Administracion
        private async void limpiarCampos()
        {
            tbClave.Text = (await getNextAreaId()).ToString();
            tbNombreArea.Text = string.Empty;
            tbDescripcion.Text = string.Empty;
            areaSeleccionada = null;
        }
        private async Task<int> getNextAreaId()
        {
            return await areaDAO.getNextId();
        }
        private async Task<bool> obtenerDatosVista()
        {
            if (await validarCampos())
            {
                nuevaArea = new Area()
                {
                    Nombre = tbNombreArea.Text.ToUpper(),
                    Descripcion = tbDescripcion.Text.ToUpper(),
                    UsuarioRegistro = int.Parse(App.usuarioAutentificado.UsuarioId),
                    FechaHoraRegistro = DateTime.Now
                };
                if(estado == (int)ACCION.ACTUALIZAR)
                {
                    nuevaArea.AreaId = areaSeleccionada.AreaId;
                }
                return true;
            }
            return false;
        }
        private async Task<bool> validarCampos()
        {
            if (string.IsNullOrWhiteSpace(tbNombreArea.Text))
            {
                await new MessageDialog("Capture el nombre del area.").ShowAsync();
                tbNombreArea.Focus(FocusState.Programmatic);
                return false;
            }
            return true;
        }

    }
}
