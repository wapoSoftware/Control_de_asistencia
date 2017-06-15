using Assistance_Control.Utilerias.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        AreaItem areaSeleccionada = null;
        List<AreaItem> areas = null;
        //Constructor
        public abcAreas()
        {
            this.InitializeComponent();
            cargarAreas();
        }
        //Metodos de carga
        private void cargarAreas()
        {
            areas = new List<AreaItem>();
            int length = 100;
            for (int i = 0; i < length; i++)
            {
                areas.Add(new AreaItem
                {
                    Nombre = "Luis_Alonso",
                    Clave = i * 3
                });
            }
            DataGrid.ItemsSource = areas;
        }
        //Eventos
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.SelectedItem = null;
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
            areaSeleccionada = (AreaItem)DataGrid.SelectedItem;
            if (areaSeleccionada != null)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbClave.Text = areaSeleccionada.Clave.ToString();
                tbNombreArea.Text = areaSeleccionada.Nombre;
            }
            else
            {
                await new MessageDialog("Seleccione un area para editar", "Atención").ShowAsync();
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            string message = string.Empty;
            if (areaSeleccionada != null)//Actualizar
            {
                int selectedIndex = areas.IndexOf(areaSeleccionada);
                areas[selectedIndex].Nombre = tbNombreArea.Text;
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = areas;
                DataGrid.SelectedItem = areaSeleccionada;
                message = "Area actualizada correctamente!";
            }
            else
            {
                //Insertar nuevo
                message = "Error al insertar entidad";
            }
            limpiarCampos();
            await new MessageDialog(message, "").ShowAsync();
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            gridAgregar.Visibility = Visibility.Collapsed;
        }
        //Administracion
        private void limpiarCampos()
        {
            tbClave.Text = string.Empty;
            tbNombreArea.Text = string.Empty;
            areaSeleccionada = null;
        }
        private void obtenerDatos()
        {

        }
    }
}
