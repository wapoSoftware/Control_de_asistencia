using Assistance_ControlBLL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_ControlBLL.TablesClasses
{
    public class tcUsuario
    {
        public void Insertar(Usuario entUsuario)
        {
            try
            {
                Usuario usuario = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    usuario = new Usuario
                    {
                        UsuarioId = entUsuario.UsuarioId,
                        EmpleadoId = entUsuario.EmpleadoId,
                        Contrasena = entUsuario.Contrasena,
                        Nivel = entUsuario.Nivel
                    };
                    entidad.Usuarios.Add(usuario);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar usuario.");
            }
        }
        public void Eliminar(Usuario entUsuario)
        {
            try
            {
                List<Usuario> usuarios = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Usuarios
                                   where c.UsuarioId == entUsuario.UsuarioId
                                   select c;
                    usuarios = consulta.ToList();
                    if (usuarios.Count > 0)
                    {
                        foreach (Usuario emp in usuarios)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar usuario.");
            }
        }
        public void Actualizar(Usuario entUsuario)
        {
            try
            {
                List<Usuario> usuarios = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Usuarios
                                   where c.UsuarioId == entUsuario.UsuarioId
                                   select c;
                    usuarios = consulta.ToList();
                    if (usuarios.Count > 0)
                    {
                        foreach (Usuario us in usuarios)
                        {
                            us.EmpleadoId = entUsuario.EmpleadoId;
                            us.Contrasena = entUsuario.Contrasena;
                            us.Nivel = entUsuario.Nivel;
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar empleado.");
            }
        }
    }
}
