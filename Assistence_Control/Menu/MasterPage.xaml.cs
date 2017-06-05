using Assistence_Control.Menu;
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
            new NavLink() { Label = "Inicio", Symbol = Symbol.Home, Title = "Inicio" },
             new NavLink() { Label = "Empleados", Symbol = Symbol.OtherUser, Title = "Empleados" }
        };
        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        public MasterPage()
        {
            this.InitializeComponent();
            fPrincipal.Navigate(typeof(Main));

        }
        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavLink navLinkSeleccionado = e.ClickedItem as NavLink;

            switch (navLinkSeleccionado.Title)
            {
                case "Inicio":
                    fPrincipal.Navigate(typeof(Main),this);
                    break;

                case "Empleados":
                    fPrincipal.Navigate(typeof(abcEmpleado));
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
        public Symbol Symbol { get; set; }
    }

}
