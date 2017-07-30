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
                c.ApellidoPaterno.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 ||
                c.ApellidoMaterno.IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1 || 
                c.EmpleadoId.ToString().IndexOf(query, StringComparison.CurrentCultureIgnoreCase) > -1)
                .OrderByDescending(c => c.Nombre.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c. ApellidoPaterno.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.ApellidoMaterno.StartsWith(query, StringComparison.CurrentCultureIgnoreCase))
                .ThenByDescending(c => c.EmpleadoId.ToString().IndexOf(query, StringComparison.CurrentCultureIgnoreCase));
        }
        public static int calcularEdad(DateTime birthdate)
        {
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - birthdate.Year;
            // Go back to the year the person was born in case of a leap year
            if (birthdate > today.AddYears(-age)) age--;

            return age;
        }
        public static string formatearHoras(TimeSpan hora)
        {
            var hours = hora.Hours;
            var minutes = hora.Minutes;
            var amPmDesignator = "AM";
            if (hours == 0)
                hours = 12;
            else if (hours == 12)
                amPmDesignator = "PM";
            else if (hours > 12)
            {
                hours -= 12;
                amPmDesignator = "PM";
            }
            return string.Format("{0}:{1:00} {2}", hours, minutes, amPmDesignator);
        }
    }
}
