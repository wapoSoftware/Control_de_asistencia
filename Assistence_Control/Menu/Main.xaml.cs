using Assistence_Control.TemplateSelectors;
using Assistence_Control.Utilerias.Items;
using Assistence_Control.Views.Empleados;
using System;
using System.Collections.Generic;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Assistence_Control.Menu
{
    public sealed partial class Main : Page
    {
        Frame rootFrame = (Window.Current.Content as Frame);
        public Main()
        {
            this.InitializeComponent();
            rootFrame.SizeChanged += RootFrame_SizeChanged;
            cargarMenus();
        }
        private void cargarMenus()
        {
            int length = 5;
            List<DatosMenu> menus = new List<DatosMenu>();
            menus.Add(new DatosMenu()
            {
                Titulo = "Empleados",
                Descripcion = "Empleados",
                BackGroundColor = "#ffc412",
                TileSize = 1
                
            });
            menus.Add(new DatosMenu()
            {
                Titulo = "Areas",
                Descripcion = "Areas",
                BackGroundColor = "#c6690f",
                TileSize = 2

            });
            menus.Add(new DatosMenu()
            {
                Titulo = "Reportes",
                Descripcion = "Reportes",
                BackGroundColor = "#742971",
                TileSize = 1

            });
            menus.Add(new DatosMenu()
            {
                Titulo = "Horarios",
                Descripcion = "Horarios",
                BackGroundColor = "#ff2116",
                TileSize = 2

            }); menus.Add(new DatosMenu()
            {
                Titulo = "Usuarios",
                Descripcion = "Usuarios",
                BackGroundColor = "#007be8",
                TileSize = 1

            });
            gvMenu.ItemsSource = menus;
        }

        private void gvMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            DatosMenu menu = ((DatosMenu)e.ClickedItem);
            switch (menu.Titulo)
            {
                case "Empleados":
                    rootFrame.Navigate(typeof(abcEmpleado));
                    break;
                case "Areas":
                    break;
                case "Reportes":
                    break;
                case "Horarios":
                    break;
                case "Usuarios":
                    break;

            }
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

    }
}
