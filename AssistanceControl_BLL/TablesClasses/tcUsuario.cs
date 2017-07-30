using AssistanceControl_BLL.AssistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcUsuario: tcGenerico<Usuario>
    {
        private Uri _uriServicio;

        public tcUsuario(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }

        public async Task<Usuario> getUsuario(string empleadoId, string contrasena)
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Usuario?$filter=EmpleadoId eq " + empleadoId + " and Contrasena eq '" + contrasena + "'";
            return await getData(URL);
        }
        public async Task<Usuario> getUsuarioByEmpleadoId(string empleadoId)
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Usuario?$filter=EmpleadoId eq " + empleadoId;
            return await getData(URL);
        }
        public async Task<Usuario> getUsuarioByUsuarioId(string usuarioId)
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Usuario?$filter=UsuarioId eq '" + usuarioId + "'";
            return await getData(URL);
        }
        public async void Insertar(Usuario user)
        {
            try
            {
                //using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                //{
                //    entidad.EmpleadoPermisoes.Add(entEmpleadoPermiso);
                //    entidad.SaveChanges();
                //}
                await base.insert(_uriServicio, user);
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar empleadoPermiso.");
            }
        }

    }
}
