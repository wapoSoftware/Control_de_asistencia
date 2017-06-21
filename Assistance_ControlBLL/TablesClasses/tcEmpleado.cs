using Assistance_ControlBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_ControlBLL
{
    public class tcEmpleado
    {
        public void Insertar(Empleado entEmpleado)
        {
            try
            {
                Empleado empleado = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    empleado = new Empleado
                    {
                        EmpleadoId = entEmpleado.EmpleadoId,
                        Nombre = entEmpleado.Nombre,
                        Apellido1 = entEmpleado.Apellido1,
                        Apellido2 = entEmpleado.Apellido2,
                        Edad = entEmpleado.Edad,
                        UsuarioRegistro = entEmpleado.UsuarioRegistro,
                        FechaHoraRegistro = entEmpleado.FechaHoraRegistro
                        
                    };
                    entidad.Empleadoes.Add(empleado);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar empleado.");
            }
        }
        public void Eliminar(Empleado entEmpleado)
        {
            try
            {
                List<Empleado> empleados = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Empleadoes
                                   where c.EmpleadoId == entEmpleado.EmpleadoId
                                   select c;
                    empleados = consulta.ToList();
                    if (empleados.Count > 0)
                    {
                        foreach (Empleado emp in empleados)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar empleado.");
            }
        }
        public void Actualizar(Empleado entEmpleado)
        {
            try
            {
                List<Empleado> empleados = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Empleadoes
                                   where c.EmpleadoId == entEmpleado.EmpleadoId
                                   select c;
                    empleados = consulta.ToList();
                    if (empleados.Count > 0)
                    {
                        foreach (Empleado emp in empleados)
                        {
                            emp.Nombre = entEmpleado.Nombre;
                            emp.Apellido1 = entEmpleado.Apellido1;
                            emp.Apellido2 = entEmpleado.Apellido2;
                            emp.Edad = entEmpleado.Edad;
                            emp.UsuarioRegistro = entEmpleado.UsuarioRegistro;
                            emp.FechaHoraRegistro = entEmpleado.FechaHoraRegistro;
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
