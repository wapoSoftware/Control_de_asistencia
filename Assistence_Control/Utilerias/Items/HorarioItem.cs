using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_Control.Utilerias.Items
{
    public class HorarioItem
    {
        string _nombre;
        DateTime _entrada;
        DateTime _salida;   

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public DateTime Entrada
        {
            get { return _entrada; }
            set { _entrada = value; }
        }
        public DateTime Salida
        {
            get { return _salida; }
            set { _salida = value; }
        }
    }
}
