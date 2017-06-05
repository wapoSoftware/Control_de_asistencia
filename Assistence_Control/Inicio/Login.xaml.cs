using Assistence_Control.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace Assistence_Control.Inicio
{
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
            
        }

        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Frame rootFrame = (Window.Current.Content as Frame);
                rootFrame.Navigate(typeof(pMasterPage),this);
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                messageDialog.Commands.Add(new UICommand("Aceptar"));
                messageDialog.DefaultCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
