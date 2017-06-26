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

        public tcEmpleado(Uri uriServicio)
        {
            _uriServicio = uriServicio;
        }
        public async Task<List<Empleado>> getAllEmpleados()
        {
            String URL = _uriServicio.AbsoluteUri;
            URL += "/Empleado";
            return await getDataList(URL);
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
                //entidad.Estatus = 0;
                entidad.Nombre = "Eliminado";
                await base.update(_uriServicio, entidad);
            }
            catch(Exception ex)
            {
                throw new Exception("Error al eliminar empleado.");
            }
        }

    }
}
