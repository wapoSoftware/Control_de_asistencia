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

        public async void Insertar(Empleado user)
        {
            try
            {
                //using (AssistanceControlEntities entidad = new AssistanceControlEntities())
                //{
                //    entidad.EmpleadoPermisoes.Add(entEmpleadoPermiso);
                //    entidad.SaveChanges();
                //}
                await base.insert(_uriServicio, user);
            }
            catch (Exception)
            {
                throw new Exception("Error al insertar empleadoPermiso.");
            }
        }

    }
}
