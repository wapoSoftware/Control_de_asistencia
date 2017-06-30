using AssistanceControl_BLL.AssistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcHorario : tcGenerico<Horario>
    {
        private Uri _uriServicio;
        public tcHorario(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }
        public async Task<List<Horario>> getAllHorarios()
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Horario?$filter=Estatus eq 1";
            return await getDataList(URL);
        }
        public async Task<int> getNextId()
        {
            List<Horario> areas = null;
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Horario";
            areas = await getDataList(URL);
            areas = areas.OrderBy(o => o.HorarioId).ToList();
            if (areas != null && areas.Count > 0)
            {
                return areas.LastOrDefault().HorarioId + 1;
            }
            else
            {
                return 1;
            }
        }
        public async Task Insertar(Horario entidad)
        {
            try
            {
                await base.insert(_uriServicio, entidad);
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar area.");
            }
        }
        public async Task Actualizar(Horario entidad)
        {
            try
            {
                await base.update(_uriServicio, entidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar area.");
            }
        }
        public async Task Eliminar(Horario entidad)
        {
            try
            {
                //entidad.Estatus = 0;
                await base.update(_uriServicio, entidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar horario.");
            }
        }
    }
}
