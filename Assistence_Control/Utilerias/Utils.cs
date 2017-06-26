using AssistanceControl_BLL.AssistanceService;
using Assistence_Control.Utilerias.Items;
using Assistence_Control.Views.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_Control.Utilerias
{
    public static class Utils
    {
        public static IEnumerable<Empleado> obtenerEmpleadosBusqueda(List<Empleado> empleados, string query)
        {
            return empleados.Where(
                c => c.Nombre.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                c.Apellido1.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                c.Apellido2.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 || 
                c.EmpleadoId.ToString().IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(c => c.Nombre.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.Apellido1.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.Apellido2.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.EmpleadoId.ToString().IndexOf(query, StringComparison.CurrentCultureIgnoreCase));
        }

    }
}
