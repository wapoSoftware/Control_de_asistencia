using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcHorarioArea
    {
        public void Insertar(HorarioArea entHorarioArea)
        {
            try
            {
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    entidad.HorarioAreas.Add(entHorarioArea);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar HorarioArea.");
            }
        }
        public void Eliminar(HorarioArea entHorarioArea)
        {
            try
            {
                List<HorarioArea> horarioAreas = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.HorarioAreas
                                   where c.HorarioId == entHorarioArea.HorarioId
                                   where c.AreaId == entHorarioArea.AreaId
                                   select c;
                    horarioAreas = consulta.ToList();
                    if (horarioAreas.Count > 0)
                    {
                        foreach (HorarioArea horarioArea in horarioAreas)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar HorarioArea.");
            }
        }
        public void Actualizar(HorarioArea entHorarioArea)
        {
            try
            {
                List<HorarioArea> horarioAreas = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.HorarioAreas
                                   where c.HorarioId == entHorarioArea.HorarioId
                                   where c.AreaId == entHorarioArea.AreaId
                                   select c;
                    horarioAreas = consulta.ToList();
                    if (horarioAreas.Count > 0)
                    {
                        foreach (HorarioArea horarioArea in horarioAreas)
                        {
                            //
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar HorarioArea.");
            }
        }
    }
}
