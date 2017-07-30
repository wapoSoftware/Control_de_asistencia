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
        enum ACCION { INSERTAR = 1, ACTUALIZAR = 2, ELIMINAR = 3, DEFAULT = 4 };
        int estado = 0;
        Horario horarioSeleccionado = null;
        Horario nuevoHorario = null;
        tcHorarioArea haDAO;
        HorarioArea nuevoHorarioArea = null;
        HorarioArea horarioAreaSeleccionado = null;
        Area areaSeleccionada = null;

        //Constructos
        public MasterHorarios()
        {
            this.InitializeComponent();
            thisFrame.SizeChanged += RootFrame_SizeChanged;
            horarioDAO = new tcHorario(App.uriServicio);
            areaDAO = new tcArea(App.uriServicio);
            haDAO = new tcHorarioArea(App.uriServicio);
            cargarMenus();
            cargarCombos();
            cargarHorarios();
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
        private async void cbAreas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            areaSeleccionada = (Area)cb.SelectedItem;
            dgHorarios.ItemsSource = await haDAO.getHorariosByAreaId(areaSeleccionada.AreaId);
            estado = (int)ACCION.INSERTAR;
        }
        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            prLoading.IsActive = true;
            try
            {
                if (horarioSeleccionado != null)
                {
                    estado = (int)ACCION.ELIMINAR;
                    MessageDialog messageDialog = new MessageDialog("Esta seguro de que desea eliminar el horario seleccionado?");
                    messageDialog.Commands.Add(new UICommand("Si", async (command) =>
                    {
                        await horarioDAO.Eliminar(horarioSeleccionado);
                        cargarHorarios();
                        limpiarCampos();
                    }));
                    messageDialog.Commands.Add(new UICommand("No", (command) =>
                    {
                        return;
                    }));
                    await messageDialog.ShowAsync();
                }
                else
                {
                    await new MessageDialog("Seleccione un horario.").ShowAsync();
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
                            await new MessageDialog("Horario actualizado correctamente!", "").ShowAsync();
                        }
                        break;
                    case (int)ACCION.INSERTAR:
                        if (await obtenerDatosVista())
                        {
                            await horarioDAO.Insertar(nuevoHorario);
                            cargarHorarios();
                            await new MessageDialog("El horario fue creado correctamente.", "").ShowAsync();
                        }
                        break;
                    case (int)ACCION.DEFAULT:
                        if (await obtenerDatosVista())
                        {
                            await horarioDAO.Insertar(nuevoHorario);
                            cargarHorarios();
                            await new MessageDialog("El horario fue creado correctamente.", "").ShowAsync();
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
        private async Task<bool> obtenerDatosVistaHorarioArea()
        {
            if (await validarCamposHorarioArea())
            {
                nuevoHorarioArea = new HorarioArea()
                {
                    HorarioId = ((Horario)cbHorarios.SelectedItem).HorarioId,
                    AreaId = areaSeleccionada.AreaId,
                    FechaInicio = dpFechaInicial.Date.DateTime,
                    FechaFinal = dpFechaFinal.Date.DateTime,
                    UsuarioRegistro = App.usuarioAutentificado.UsuarioId,
                    FechaHoraRegistro = DateTime.Now,
                    Area = await areaDAO.getAreaById(areaSeleccionada.AreaId),
                    Horario = await horarioDAO.getHorarioByHorarioId(((Horario)cbHorarios.SelectedItem).HorarioId),
                    UsuarioModificacion = App.usuarioAutentificado.UsuarioId,
                    FechaHoraModificacion = DateTime.Now
                };
                if (estado == (int)ACCION.ACTUALIZAR)
                {
                    nuevoHorarioArea.HorarioId = horarioSeleccionado.HorarioId;
                    nuevoHorario.UsuarioRegistro = horarioAreaSeleccionado.UsuarioRegistro;
                    nuevoHorario.FechaHoraRegistro = horarioAreaSeleccionado.FechaHoraRegistro;
                }
                return true;
            }
            return false;
        }
        private async Task<bool> validarCamposHorarioArea()
        {
            if (areaSeleccionada == null)
            {
                await new MessageDialog("Seleccione el Area.").ShowAsync();
                cbAreas.Focus(FocusState.Programmatic);
                return false;
            }
            if (cbHorarios.SelectedItem == null)
            {
                await new MessageDialog("Seleccione el horario.").ShowAsync();
                cbHorarios.Focus(FocusState.Programmatic);
                return false;
            }
            return true;
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
                    HoraEntrada = Utilerias.Utils.formatearHoras(dpEntrada.Time),
                    HoraSalida = Utilerias.Utils.formatearHoras(dpSalida.Time),
                    FechaHoraModificacion = DateTime.Now,
                    UsuarioModificacion = App.usuarioAutentificado.UsuarioId
                };
                if (estado == (int)ACCION.ACTUALIZAR)
                {
                    nuevoHorario.HorarioId = horarioSeleccionado.HorarioId;
                    nuevoHorario.UsuarioRegistro = horarioSeleccionado.UsuarioRegistro;
                    nuevoHorario.FechaHoraRegistro = horarioSeleccionado.FechaHoraRegistro;
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
                TimeSpan time;
                // TimeSpan.TryParse(horarioSeleccionado.HoraEntrada, out time);
                TimeSpan.TryParse(horarioSeleccionado.HoraEntrada.Replace("A", "").Replace("P", "").Replace("M",""), out time);

                dpEntrada.Time = time;
                TimeSpan.TryParse(horarioSeleccionado.HoraSalida.Replace("A", "").Replace("P", "").Replace("M", ""), out time);
                dpSalida.Time = time;

                //= areaSeleccionada.Nombre;
                //    tbDescripcion.Text = areaSeleccionada.Descripcion;
            }
            else
            {
                await new MessageDialog("Seleccione un horario para editar", "Atención").ShowAsync();
            }
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyToolkit.Controls.DataGrid dg = (MyToolkit.Controls.DataGrid)sender;
            horarioSeleccionado = (Horario)dg.SelectedItem;
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
            tbNombreHorario.Text = string.Empty;
            dpEntrada.Time = DateTime.Now.TimeOfDay;
            dpSalida.Time = DateTime.Now.TimeOfDay;
            estado = (int)ACCION.DEFAULT;
        }
        private async void asignarHorarioArea()
        {
            try
            {
                bool existe = false;
                List<HorarioArea> horariosArea = await haDAO.getHorariosByAreaId(nuevoHorarioArea.AreaId);
                if (horariosArea != null)
                {
                    List<HorarioArea> horarios = horariosArea.Where(w => w.HorarioId == nuevoHorarioArea.HorarioId).ToList();
                    if (horarios != null && horarios.Count > 0)
                    {
                        HorarioArea ultimoHorario = horarios.OrderByDescending(o => o.FechaFinal).FirstOrDefault();
                        if (nuevoHorarioArea.FechaInicio < ultimoHorario.FechaFinal)
                        {
                            existe = true;
                        }
                    }
                }
                if (!existe)
                {
                    await haDAO.Insertar(nuevoHorarioArea);
                    cargarHorariosArea();
                    await new MessageDialog("El horario fue creado correctamente.").ShowAsync();
                }
                else
                {
                    await new MessageDialog("La fecha de inicio no puede ser menor a la fecha final del horario anterior.").ShowAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
                        if (await obtenerDatosVistaHorarioArea())
                        {
                            await haDAO.Actualizar(nuevoHorarioArea);
                            cargarHorariosArea();
                            DataGrid.SelectedItem = horarioSeleccionado;
                            await new MessageDialog("Horario actualizado correctamente!", "").ShowAsync();
                        }
                        break;
                    case (int)ACCION.INSERTAR:
                        if (await obtenerDatosVistaHorarioArea())
                        {
                            asignarHorarioArea();
                            //cargarHorariosArea();
                        }
                        break;
                    case (int)ACCION.DEFAULT:
                        if (await obtenerDatosVistaHorarioArea())
                        {
                            asignarHorarioArea();
                            //cargarHorariosArea();
                        }
                        break;
                }
                limpiarCampos();
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }
        private async void cargarHorariosArea()
        {
            dgHorarios.ItemsSource = await haDAO.getHorariosByAreaId(areaSeleccionada.AreaId);
        }

        private void dgHorarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyToolkit.Controls.DataGrid dg = (MyToolkit.Controls.DataGrid)sender;
            horarioAreaSeleccionado = (HorarioArea)dg.SelectedItem;
        }
    }
}
