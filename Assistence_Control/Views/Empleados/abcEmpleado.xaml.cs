using Assistence_Control.Utilerias.Items;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Assistence_Control.Views.Empleados
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class abcEmpleado : Page
    {
        public abcEmpleado()
        {
            this.InitializeComponent();
            cargarEmpleados();
        }
        private void cargarEmpleados()
        {
            List<EmpleadoItem> empleados = new List<EmpleadoItem>();
            int length = 100;
            for (int i = 0; i < length; i++)
            {
                empleados.Add(new EmpleadoItem {
                    Nombre = "Luis Alonso",
                    NoEmpleado = "12490092",
                    ApellidoPaterno = "Duenas",
                    ApellidoMaterno = "Manroquez",
                    Edad = i,
                    FechaAlta = DateTime.Today
                });
            }
            DataGrid.ItemsSource = empleados;
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            if (gridAgregar.Visibility == Visibility.Collapsed)
            {
                gridAgregar.Visibility = Visibility.Visible;
            }
            else
            {
                gridAgregar.Visibility = Visibility.Collapsed;
            }
        }
    }
}
