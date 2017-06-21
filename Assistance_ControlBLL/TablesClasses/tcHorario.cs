using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_ControlBLL.TablesClasses
{
    public class tcHorario
    {
        public void Insertar(Horario entHorario)
        {
            try
            {
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    entidad.Horarios.Add(entHorario);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar Horario.");
            }
        }
        public void Eliminar(Horario entHorario)
        {
            try
            {
                List<Horario> horarios = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Horarios
                                   where c.HorarioId == entHorario.HorarioId
                                   select c;
                    horarios = consulta.ToList();
                    if (horarios.Count > 0)
                    {
                        foreach (Horario horario in horarios)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar Horario.");
            }
        }
        public void Actualizar(Horario entHorario)
        {
            try
            {
                List<Horario> horarios = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Horarios
                                   where c.HorarioId == entHorario.HorarioId
                                   select c;
                    horarios = consulta.ToList();
                    if (horarios.Count > 0)
                    {
                        foreach (Horario horario in horarios)
                        {
                            //
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar Horario.");
            }
        }
    }
}
