using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcPermiso
    {
        public void Insertar(Permiso entPermiso)
        {
            try
            {
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    entidad.Permisoes.Add(entPermiso);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar Permiso.");
            }
        }
        public void Eliminar(Permiso entPermiso)
        {
            try
            {
                List<Permiso> permisos = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Permisoes
                                   where c.PermisoId == entPermiso.PermisoId
                                   select c;
                    permisos = consulta.ToList();
                    if (permisos.Count > 0)
                    {
                        foreach (Permiso permiso in permisos)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar Permiso.");
            }
        }
        public void Actualizar(Permiso entPermiso)
        {
            try
            {
                List<Permiso> Permisos = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Permisoes
                                   where c.PermisoId == entPermiso.PermisoId
                                   select c;
                    Permisos = consulta.ToList();
                    if (Permisos.Count > 0)
                    {
                        foreach (Permiso permiso in Permisos)
                        {
                            //
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar Permiso.");
            }
        }
    }
}
