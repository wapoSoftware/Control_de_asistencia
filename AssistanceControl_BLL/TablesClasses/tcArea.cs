using AssistanceControl_BLL.AssistanceService;
using AssistanceControl_BLL.TablesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_ControlBLL.TablesClasses
{
    public class tcArea : tcGenerico<Area>
    {
        private Uri _uriServicio;

        public tcArea(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }
        public async Task<List<Area>> getAllAreas()
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Area";
            return await getDataList(URL);
        }
        public async Task<int> getNextId()
        {
            List<Area> areas = null;
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Area";
            areas = await getDataList(URL);
            areas = areas.OrderBy(o => o.AreaId).ToList();
            if(areas != null && areas.Count > 0)
            {
                return areas.LastOrDefault().AreaId + 1;
            }
            else
            {
                return 1;
            }
        }

        public async Task Insertar(Area entidad)
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
        public async Task Actualizar(Area entidad)
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
        public async Task Eliminar(Area entidad)
        {
            try
            {
                //entidad.Estatus = 0;
                await base.update(_uriServicio, entidad);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar area.");
            }
        }
    }
}
