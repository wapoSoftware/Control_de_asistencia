using Assistance_ControlBLL.TablesClasses;
using AssistanceControl_BLL.AssistanceService;
using AssistanceControl_BLL.TablesClasses;
using Assistence_Control;
using Assistence_Control.TemplateSelectors;
using Assistence_Control.Utilerias.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Assistance_Control.Views.Horarios
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MasterHorarios : Page
    {
        //Variables Globales
        Frame thisFrame = Window.Current.Content as Frame;
        List<Area> listAreas = new List<Area>();
        List<Horario> listHorarios = new List<Horario>();
        tcHorario horarioDAO;
        tcArea areaDAO;
        enum ACCION { INSERTAR = 1, ACTUALIZAR = 2, ELIMINAR = 3 };
        int estado = 0;
        Horario horarioSeleccionado = null;
        Horario nuevoHorario = null;

        //Constructos
        public MasterHorarios()
        {
            this.InitializeComponent();
            thisFrame.SizeChanged += RootFrame_SizeChanged;
            horarioDAO = new tcHorario(App.uriServicio);
            areaDAO = new tcArea(App.uriServicio);
            cargarMenus();
            cargarCombos();
        }
        //Metodos de carga
        private void cargarMenus()
        {
            List<DatosMenu> menus = new List<DatosMenu>();
            menus.Add(new DatosMenu()
            {
                Titulo = "Crear/Modificar",
                Descripcion = "Crear/Modificar",
                BackGroundColor = "#959595",
                TileSize = 1,
                Imagen = ""
            });
            menus.Add(new DatosMenu()
            {
                Titulo = "Asignar/Reasignar",
                Descripcion = "Asignar/Reasignar",
                BackGroundColor = "#959595",
                TileSize = 1,
                Imagen = ""
            });
            menus.Add(new DatosMenu()
            {
                Titulo = "Permisos",
                Descripcion = "Permisos",
                BackGroundColor = "#959595",
                TileSize = 1,
                Imagen = ""
            });
            gvMenu.ItemsSource = menus;
        }
        private async void cargarHorarios()
        {
            listHorarios = await horarioDAO.getAllHorarios();
            DataGrid.ItemsSource = listHorarios;
        }
        private async void cargarCombos()
        {
            try
            {
                listAreas = await areaDAO.getAllAreas();
                cbAreas.ItemsSource = listAreas;
                listHorarios = await horarioDAO.getAllHorarios();
                cbHorarios.ItemsSource = listHorarios;
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }
        //Eventos
        private void RootFrame_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MenuTemplateSelector mts = new MenuTemplateSelector();
            mts.MenuMobileTemplate = Application.Current.Resources["MenuMobileTemplate"] as DataTemplate;
            mts.MenuTabletTemplate = Application.Current.Resources["MenuTabletTemplate"] as DataTemplate;
            mts.MenuMobileTemplateLarge = Application.Current.Resources["MenuMobileTemplateLarge"] as DataTemplate;
            mts.MenuTabletTemplateLarge = Application.Current.Resources["MenuTabletTemplateLarge"] as DataTemplate;
            gvMenu.ItemTemplateSelector = mts;
        }
        private void gvMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            DatosMenu menu = ((DatosMenu)e.ClickedItem);
            switch (menu.Titulo)
            {
                case "Asignar/Reasignar":
                    mainPivot.SelectedIndex = 1;
                    cargarHorarios();
                    break;
                case "Crear/Modificar":
                    mainPivot.SelectedIndex = 2;
                    break;
                case "Permisos":
                    // mainPivot.SelectedIndex = 3;
                    break;


            }
        }
        private void cbAreas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Area item = (Area)cb.SelectedItem;
            //TODO: Buscar si el area seleccionada ya fue asignada y cargar datos
            lblAreaSeleccionada.Text = item.Nombre;
        }
        private void cbHorarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            Area item = (Area)cb.SelectedItem;
            lblHorario.Text = item.Nombre;
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.ELIMINAR;
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.INSERTAR;
            if (gridAgregar.Visibility == Visibility.Collapsed)
            {
                gridAgregar.Visibility = Visibility.Visible;
            }
            else
            {
                limpiarCampos();
            }
        }
        private async void btnGuardarHorario_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                prLoading.IsActive = true;
                switch (estado)
                {
                    case (int)ACCION.ACTUALIZAR:
                        if (await obtenerDatosVista())
                        {
                            await horarioDAO.Actualizar(nuevoHorario);
                            cargarHorarios();
                            DataGrid.SelectedItem = horarioSeleccionado;
                            await new MessageDialog("Area actualizada correctamente!", "").ShowAsync();
                        }
                        break;
                    case (int)ACCION.INSERTAR:
                        if (await obtenerDatosVista())
                        {
                            await horarioDAO.Insertar(nuevoHorario);
                            cargarHorarios();
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
        private async Task<bool> obtenerDatosVista()
        {
            if (await validarCampos())
            {
                nuevoHorario = new Horario()
                {
                    Nombre = tbNombreHorario.Text.ToUpper(),
                    UsuarioRegistro = App.usuarioAutentificado.UsuarioId,
                    FechaHoraRegistro = DateTime.Now,
                    Estatus = 1,
                    HoraEntrada = dpEntrada.Time.ToString(),
                    HoraSalida = dpSalida.Time.ToString()
                };
                if (estado == (int)ACCION.ACTUALIZAR)
                {
                    nuevoHorario.HorarioId = horarioSeleccionado.HorarioId;
                }
                return true;
            }
            return false;
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            gridAgregar.Visibility = Visibility.Collapsed;
        }
        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            estado = (int)ACCION.ACTUALIZAR;
            horarioSeleccionado = (Horario)DataGrid.SelectedItem;
            if (horarioSeleccionado != null)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbNombreHorario.Text = horarioSeleccionado.Nombre.ToUpper();
                //dpEntrada.Time = horarioSeleccionado.HoraEntrada,

                //= areaSeleccionada.Nombre;
                //    tbDescripcion.Text = areaSeleccionada.Descripcion;
            }
            else
            {
                await new MessageDialog("Seleccione un area para editar", "Atención").ShowAsync();
            }
        }
        //Administracion
        private async Task<bool> validarCampos()
        {
            if (string.IsNullOrWhiteSpace(tbNombreHorario.Text))
            {
                await new MessageDialog("Capture el nombre del horario.").ShowAsync();
                tbNombreHorario.Focus(FocusState.Programmatic);
                return false;
            }
            return true;
        }
        private void limpiarCampos()
        {
            //tbNumeroEmpleado.Text = string.Empty;
            //tbNombre.Text = string.Empty;
            //tbApellidoPaterno.Text = string.Empty;
            //tbApellidoMaterno.Text = string.Empty;
            //empleadoSeleccionado = null;
        }
    }
}
