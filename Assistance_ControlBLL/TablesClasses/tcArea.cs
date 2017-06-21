using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_ControlBLL.TablesClasses
{
    public class tcArea
    {
        public void Insertar(Area entArea)
        {
            try
            {
                Area area = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    area = new Area
                    {
                        AreaId = entArea.AreaId,
                        Descripcion = entArea.Descripcion,
                        Nombre = entArea.Nombre,
                        UsuarioRegistro = entArea.UsuarioRegistro,
                        FechaHoraRegistro = entArea.FechaHoraRegistro                        
                    };
                    entidad.Areas.Add(area);
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar area.");
            }
        }
        public void Eliminar(Area entArea)
        {
            try
            {
                List<Area> areas = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Areas
                                   where c.AreaId == entArea.AreaId
                                   select c;
                    areas = consulta.ToList();
                    if (areas.Count > 0)
                    {
                        foreach (Area area in areas)
                        {
                            //Cambiar estatus
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar area.");
            }
        }
        public void Actualizar(Area entArea)
        {
            try
            {
                List<Area> areas = null;
                using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                {
                    var consulta = from c in entidad.Areas
                                   where c.AreaId == entArea.AreaId
                                   select c;
                    areas = consulta.ToList();
                    if (areas.Count > 0)
                    {
                        foreach (Area area in areas)
                        {
                            area.Nombre = entArea.Nombre;
                            area.Descripcion = entArea.Descripcion;
                        }
                    }
                    entidad.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw new Exception("Error al actualizar area.");
            }
        }
    }
}
