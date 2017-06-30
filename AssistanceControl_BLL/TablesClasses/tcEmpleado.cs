using Assistance_ControlBLL.TablesClasses;
using AssistanceControl_BLL;
using AssistanceControl_BLL.AssistanceService;
using AssistanceControl_BLL.TablesClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssistanceControl_BLL
{
    public class tcEmpleado : tcGenerico<Empleado>
    {
        private Uri _uriServicio;
        private tcArea areaDAO;

        public tcEmpleado(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }
        public async Task<List<Empleado>> getAllEmpleados()
        {
            List<Empleado> respuesta = null;
            areaDAO = new tcArea(_uriServicio);
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Empleado?$filter=Estatus eq 1";
            respuesta = await getDataList(URL);
            foreach (Empleado item in respuesta)
            {
                item.Area = await areaDAO.getAreaById(item.AreaId);
            }

            return respuesta;
        }

        public async Task Insertar(Empleado entidad)
        {
            try
            {
                await base.insert(_uriServicio, entidad);
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar empleado.");
            }
        }
        public async Task Actualizar(Empleado entidad)
        {
            try
            {
                await base.update(_uriServicio, entidad);
            }
            catch(Exception ex)
            {
                throw new Exception("Error al actualizar empleado.");
            }
        }
        public async Task Eliminar(Empleado entidad)
        {
            try
            {
                entidad.Estatus = 0;
                await base.update(_uriServicio, entidad);
            }
            catch(Exception ex)
            {
                throw new Exception("Error al eliminar empleado.");
            }
        }

    }
}
