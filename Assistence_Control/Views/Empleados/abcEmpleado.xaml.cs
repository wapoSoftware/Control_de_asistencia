using Assistence_Control.Utilerias.Items;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Assistence_Control.Views.Empleados
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class abcEmpleado : Page
    {
        //Variables Globales
        EmpleadoItem empleadoSeleccionado = null;
        List<EmpleadoItem> empleados = null;
        //Constructor
        public abcEmpleado()
        {
            this.InitializeComponent();
            cargarEmpleados();
        }
        //Metodos de carga
        private void cargarEmpleados()
        {
             empleados = new List<EmpleadoItem>();
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
        //Eventos
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
        private async void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            empleadoSeleccionado = (EmpleadoItem)DataGrid.SelectedItem;
            if (empleadoSeleccionado != null)
            {
                gridAgregar.Visibility = Visibility.Visible;
                tbNumeroEmpleado.Text = empleadoSeleccionado.NoEmpleado;
                tbNombre.Text = empleadoSeleccionado.Nombre;
                tbApellidoPaterno.Text = empleadoSeleccionado.ApellidoPaterno;
                tbApellidoMaterno.Text = empleadoSeleccionado.ApellidoMaterno;
            }
            else
            {
                await new MessageDialog("Seleccione un empleado para editar", "Atención").ShowAsync();
            }
        }
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            if (empleadoSeleccionado != null)//Actualizar
            {
                int selectedIndex = empleados.IndexOf(empleadoSeleccionado);
                empleados[selectedIndex].NoEmpleado = tbNumeroEmpleado.Text;
                empleados[selectedIndex].Nombre = tbNombre.Text;
                empleados[selectedIndex].ApellidoMaterno = tbApellidoMaterno.Text;
                empleados[selectedIndex].ApellidoPaterno = tbApellidoPaterno.Text;
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = empleados;
                DataGrid.SelectedItem = empleadoSeleccionado; 
            }
            else
            {
                //InsertarNuevo
            }
            limpiarCampos();
            await new MessageDialog("Empleado actualizado correctamente!", "").ShowAsync();
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            gridAgregar.Visibility = Visibility.Collapsed;
        }
        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing,
            // otherwise assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                //sender.ItemsSource = dataset;
                var matchingEmployees = Assistance_Control.Utilerias.Utils.obtenerEmpleadosBusqueda(empleados,sender.Text);
                sender.ItemsSource = matchingEmployees.ToList();
                DataGrid.ItemsSource = null;
                DataGrid.ItemsSource = matchingEmployees.ToList();
            }
        }


        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            EmpleadoItem empleado = (EmpleadoItem)args.SelectedItem;
            DataGrid.SelectedItem = empleado;
            sender.Text = empleado.Nombre;
        }


        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {
                // Use args.QueryText to determine what to do.
            }
        }
        //Administracion
        private void limpiarCampos()
        {
            tbNumeroEmpleado.Text = string.Empty;
            tbNombre.Text = string.Empty;
            tbApellidoPaterno.Text = string.Empty;
            tbApellidoMaterno.Text = string.Empty;
            empleadoSeleccionado = null;
        }
    }
}
