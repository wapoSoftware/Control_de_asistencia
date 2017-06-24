using AssistanceControl_BLL.AssistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcEmpleadoPermiso:tcGenerico<EmpleadoPermiso>
    {

        private Uri _uriServicio;

        public tcEmpleadoPermiso(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }

        public async void Insertar(EmpleadoPermiso entEmpleadoPermiso)
        {
            try
            {
                //using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                //{
                //    entidad.EmpleadoPermisoes.Add(entEmpleadoPermiso);
                //    entidad.SaveChanges();
                //}
                await base.insert(_uriServicio,entEmpleadoPermiso);
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar empleadoPermiso.");
            }
        }
        public void Eliminar(EmpleadoPermiso entEmpleadoPermiso)
        {
            try
            {
                List<EmpleadoPermiso> empleadosPermisoes = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.EmpleadoPermisoes
                                   where c.EmpleadoId == entEmpleadoPermiso.EmpleadoId
                                   where c.PermisoId == entEmpleadoPermiso.PermisoId
                                   select c;
                    empleadosPermisoes = consulta.ToList();
                    if (empleadosPermisoes.Count > 0)
                    {
                        foreach (EmpleadoPermiso empleadoPermiso in empleadosPermisoes)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar EmpleadoPermiso.");
            }
        }
        public void Actualizar(EmpleadoPermiso entEmpleadoPermiso)
        {
            try
            {
                List<EmpleadoPermiso> empleadosPermisoes = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.EmpleadoPermisoes
                                   where c.EmpleadoId == entEmpleadoPermiso.EmpleadoId
                                   where c.PermisoId == entEmpleadoPermiso.PermisoId
                                   select c;
                    empleadosPermisoes = consulta.ToList();
                    if (empleadosPermisoes.Count > 0)
                    {
                        foreach (EmpleadoPermiso empleadoPermiso in empleadosPermisoes)
                        {
                            //
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar empleadoPermiso.");
            }
        }
    }
}
