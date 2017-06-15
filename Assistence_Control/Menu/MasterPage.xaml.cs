using Assistance_Control.Views.Horarios;
using Assistence_Control.Inicio;
using Assistence_Control.Menu;
using Assistence_Control.Views;
using Assistence_Control.Views.Areas;
using Assistence_Control.Views.Empleados;
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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Assistence_Control.Menu
{
    public sealed partial class MasterPage : UserControl
    {
        private ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "Inicio",  Title = "Inicio", Source = "ms-appx://Assistence_Control/Assets/home.png" },
             new NavLink() { Label = "Empleados", Title = "Empleados", Source = "ms-appx://Assistence_Control/Assets/employee.png" },
             new NavLink() { Label = "Areas", Title = "Areas", Source = "ms-appx://Assistence_Control/Assets/field.png" },
             new NavLink() { Label = "Reportes", Title = "Reportes", Source = "ms-appx://Assistence_Control/Assets/padnote.png" },
             new NavLink() { Label = "Horarios", Title = "Horarios", Source = "ms-appx://Assistence_Control/Assets/calendar.png" },
             new NavLink() { Label = "Usuarios", Title = "Usuarios", Source = "ms-appx://Assistence_Control/Assets/users.png" },
             new NavLink() {Label = "Salir", Title = "Salir", Source = "ms-appx://Assistence_Control/Assets/exit.png" }
             
        };
        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        public MasterPage()
        {
            this.InitializeComponent();
            fPrincipal.Navigate(typeof(Main),fPrincipal);
            splitView.IsPaneOpen = false;
            fPrincipal.Navigating += FPrincipal_Navigating;
        }

        private void FPrincipal_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavLink navLinkSeleccionado = e.ClickedItem as NavLink;

            switch (navLinkSeleccionado.Title)
            {
                case "Inicio":
                    fPrincipal.Navigate(typeof(Main), fPrincipal);
                    splitView.IsPaneOpen = false;
                    break;

                case "Empleados":
                    fPrincipal.Navigate(typeof(abcEmpleado));
                    splitView.IsPaneOpen = true;
                    break;

                case "Areas":
                    fPrincipal.Navigate(typeof(abcAreas));
                    splitView.IsPaneOpen = true;
                    break;

                case "Horarios":
                    fPrincipal.Navigate(typeof(MasterHorarios));
                    splitView.IsPaneOpen = true;
                    break;

                case "Salir":
                    Frame rootFrame = (Window.Current.Content as Frame);
                    rootFrame.Navigate(typeof(Login), this);
                    break;
            }
        }
        private void splitViewToggle_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            splitView.IsPaneOpen = !splitView.IsPaneOpen;
        }
       


    }
    public class NavLink
    {
        public string Title { get; set; }
        public string Label { get; set; }
        public string Source { get; set; }
    }

}
