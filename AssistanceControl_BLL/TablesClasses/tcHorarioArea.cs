using Assistance_ControlBLL.TablesClasses;
using AssistanceControl_BLL.AssistanceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL.TablesClasses
{
    public class tcHorarioArea : tcGenerico<HorarioArea>
    {
        private Uri _uriServicio;
        private tcArea areaDAO;
        private tcHorario horarioDAO;   
        public tcHorarioArea(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }
        public async Task<List<HorarioArea>> getAllHorarios()
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/HorarioArea";
            return await getDataList(URL);
        }
        public async Task<List<HorarioArea>> getHorariosByAreaId(int areaId)
        {
            List<HorarioArea> respuesta = null;
            areaDAO = new tcArea(_uriServicio);
            horarioDAO = new tcHorario(_uriServicio);
            String URL = _uriServicio.AbsoluteUri;
            URL += "/HorarioArea?$filter=AreaId eq " + areaId + "";
            respuesta = await getDataList(URL);
            foreach (HorarioArea item in respuesta)
            {
                item.Area = await areaDAO.getAreaById(item.AreaId);
                item.Horario = await horarioDAO.getHorarioByHorarioId(item.HorarioId);
            }
            return respuesta;
        }
        public async Task Insertar(HorarioArea entidad)
        {
            try
            {
                await base.insert(_uriServicio, entidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar horario.");
            }
        }
        public async Task Actualizar(HorarioArea entidad)
        {
            try
            {
                await base.update(_uriServicio, entidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar horario.");
            }
        }
        public async Task Eliminar(HorarioArea entidad)
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
