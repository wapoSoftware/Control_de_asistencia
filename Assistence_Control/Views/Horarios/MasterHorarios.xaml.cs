using Assistance_Control.Utilerias.Items;
using Assistence_Control.TemplateSelectors;
using Assistence_Control.Utilerias.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        Frame thisFrame = Window.Current.Content as Frame;
        ObservableCollection<AreaItem> listAreas = new ObservableCollection<AreaItem>();
        ObservableCollection<HorarioItem> listHorarios = new ObservableCollection<HorarioItem>();
        public MasterHorarios()
        {
            this.InitializeComponent();
            thisFrame.SizeChanged += RootFrame_SizeChanged;
            cargarMenus();
            cargarCombos();
        }
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
                    break;
                case "Crear/Modificar":
                    mainPivot.SelectedIndex = 2;
                    break;
                case "Permisos":
                   // mainPivot.SelectedIndex = 3;
                    break;


            }
        }
        private void cargarCombos()
        {
            listAreas.Add(new AreaItem() { Clave = 1, Nombre = "Area 1" });
            listAreas.Add(new AreaItem() { Clave = 2, Nombre = "Area 2" });
            listAreas.Add(new AreaItem() { Clave = 3, Nombre = "Area 3" });
            listAreas.Add(new AreaItem() { Clave = 4, Nombre = "Area 4" });
            //listHorarios = new ObservableCollection<string> { "Horario 1", "Horario 2", "Horario 3", "Horario 4" };
            cbAreas.ItemsSource = listAreas;
            //cbHorarios.DataContext = listHorarios;
        }

        private void cbAreas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            AreaItem item = (AreaItem)cb.SelectedItem;
            //TODO: Buscar si el area seleccionada ya fue asignada y cargar datos
            lblAreaSeleccionada.Text = item.Nombre;
        }

        private void cbHorarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            AreaItem item = (AreaItem)cb.SelectedItem;
            lblHorario.Text = item.Nombre;
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (gridAgregar.Visibility == Visibility.Collapsed)
            {
                gridAgregar.Visibility = Visibility.Visible;
            }
            else
            {
                limpiarCampos();
            }
        }

        private void btnGuardarHorario_Click(object sender, RoutedEventArgs e)
        {
            //if (empleadoSeleccionado != null)//Actualizar
            //{
            //    int selectedIndex = empleados.IndexOf(empleadoSeleccionado);
            //    empleados[selectedIndex].NoEmpleado = tbNumeroEmpleado.Text;
            //    empleados[selectedIndex].Nombre = tbNombre.Text;
            //    empleados[selectedIndex].ApellidoMaterno = tbApellidoMaterno.Text;
            //    empleados[selectedIndex].ApellidoPaterno = tbApellidoPaterno.Text;
            //    DataGrid.ItemsSource = null;
            //    DataGrid.ItemsSource = empleados;
            //    DataGrid.SelectedItem = empleadoSeleccionado;
            //}
            //else
            //{
            //    //InsertarNuevo
            //}
            //limpiarCampos();
            //await new MessageDialog("Empleado actualizado correctamente!", "").ShowAsync();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            gridAgregar.Visibility = Visibility.Collapsed;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            //empleadoSeleccionado = (EmpleadoItem)DataGrid.SelectedItem;
            //if (empleadoSeleccionado != null)
            //{
            //    gridAgregar.Visibility = Visibility.Visible;
            //    tbNumeroEmpleado.Text = empleadoSeleccionado.NoEmpleado;
            //    tbNombre.Text = empleadoSeleccionado.Nombre;
            //    tbApellidoPaterno.Text = empleadoSeleccionado.ApellidoPaterno;
            //    tbApellidoMaterno.Text = empleadoSeleccionado.ApellidoMaterno;
            //}
            //else
            //{
            //    await new MessageDialog("Seleccione un empleado para editar", "Atención").ShowAsync();
            //}
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
