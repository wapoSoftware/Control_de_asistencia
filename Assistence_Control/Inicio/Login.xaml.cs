using AssistanceControl_BLL.AssistanceService;
using AssistanceControl_BLL.TablesClasses;
using Assistence_Control.Menu;
using System;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Assistence_Control.Inicio
{
    public sealed partial class Login : Page
    {
        //Constructor
        public Login()
        {
            this.InitializeComponent();
            
        }
        //Eventos
        private async void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            accesar();
        }
        private async void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
            try
            {
                Usuario user = new Usuario
                {
                    EmpleadoId = 14490224,
                    Contrasena = "potter13",
                    Nivel = 1,
                    UsuarioRegistro = 0,
                    FechaHoraRegistro = DateTime.Now,
                    UsuarioId = "2"
                };
                tcUsuario usDAO = new tcUsuario(App.uriServicio);
                usDAO.Insertar(user);
                await new MessageDialog("correcto").ShowAsync();

            }
            catch (Exception ex)
            {
                await new MessageDialog("Error al insertar usuario" + ex.Message).ShowAsync();
            }
        }
        private void tbPassword_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                accesar();
            }
        }
        private async void numeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            string val = txt.Text;
            double aux = 0.0;
            if (string.IsNullOrEmpty(val))
            {
                return;
            }
            if (!double.TryParse(val, out aux))
            {
                if (!char.IsNumber(val[val.Length - 1]))
                {
                    val = val.Remove(val.Length - 1);
                }
                else if (!char.IsNumber(val[0]))
                {
                    val = val.Remove(0, 1);
                }
                txt.Text = val;
                txt.SelectionStart = val.Length;
                //TODO: AGEGAR ETIQUETA "SOLO SE PERMITEN NUMEROS"
            }
        }
        //Administracion
        private async Task<bool> validaCampos()
        {
            if (string.IsNullOrWhiteSpace(tbUsuario.Text))
            {
                await new MessageDialog("Falta ingresar usuario").ShowAsync();
                tbUsuario.Focus(FocusState.Programmatic);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(tbPassword.Password))
            {
                await new MessageDialog("Falta ingresar contraseña.").ShowAsync();
                tbUsuario.Focus(FocusState.Programmatic);
                return false;
            }
            return true;
        }
        private async void accesar()
        {
            prLoading.IsActive = true;
            try
            {
                prLoading.IsActive = true;
                if (await validaCampos())
                {
                    tcUsuario usDAO = new tcUsuario(App.uriServicio);
                    App.usuarioAutentificado = await usDAO.getUsuarioByEmpleadoId(tbUsuario.Text);
                    if (App.usuarioAutentificado != null)
                    {
                        if (tbPassword.Password != App.usuarioAutentificado.Contrasena)
                        {
                            await new MessageDialog("La contraseña es incorrecta.").ShowAsync();
                            tbPassword.Focus(FocusState.Programmatic);
                            tbPassword.SelectAll();
                            return;
                        }
                        else
                        {
                            Frame rootFrame = (Window.Current.Content as Frame);
                            rootFrame.Navigate(typeof(pMasterPage), this);
                        }
                    }
                    else
                    {
                        await new MessageDialog("El usuario es incorrecto.").ShowAsync();
                        tbUsuario.Focus(FocusState.Programmatic);
                        tbUsuario.SelectAll();
                    }
                }
            }
            catch (Exception ex)
            {
                var messageDialog = new MessageDialog("", ex.InnerException == null ? ex.Message : ex.InnerException.Message);
                messageDialog.Commands.Add(new UICommand("Aceptar"));
                messageDialog.DefaultCommandIndex = 0;
                await messageDialog.ShowAsync();
            }
            finally
            {
                prLoading.IsActive = false;
            }
        }
        private void limpiarCampos()
        {
            tbPassword.Password = string.Empty;
            tbUsuario.Text = string.Empty;
        }
    }
}
