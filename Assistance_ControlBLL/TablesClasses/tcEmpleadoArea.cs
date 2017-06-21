using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_ControlBLL.TablesClasses
{
    public class tcEmpleadoArea
    {
        public void Insertar(Asistencia entAsistencia)
        {
            try
            {
                Asistencia asistencia = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    asistencia = new Asistencia
                    {
                        AsistenciaId = entAsistencia.AsistenciaId,
                        EmpleadoId = entAsistencia.EmpleadoId,
                        FechaHora = entAsistencia.FechaHora,
                        Estado = entAsistencia.Estado
                    };
                    entidad.Asistencias.Add(asistencia);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar asistencia.");
            }
        }
        public void Eliminar(Asistencia entAsistencia)
        {
            try
            {
                List<Asistencia> asistencias = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Asistencias
                                   where c.AsistenciaId == entAsistencia.AsistenciaId
                                   select c;
                    asistencias = consulta.ToList();
                    if (asistencias.Count > 0)
                    {
                        foreach (Asistencia asstnc in asistencias)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar asistencia.");
            }
        }
        public void Actualizar(Asistencia entArea)
        {
            try
            {
                List<Asistencia> asistencias = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Asistencias
                                   where c.AsistenciaId == entArea.AsistenciaId
                                   select c;
                    asistencias = consulta.ToList();
                    if (asistencias.Count > 0)
                    {
                        foreach (Asistencia asstnc in asistencias)
                        {

                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar asistencia.");
            }
        }
    }
}
