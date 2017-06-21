using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistance_Control.Utilerias.Items
{
    public class AreaItem
    {
        int _clave;
        string _nombre;

        public int Clave
        {
            get { return _clave; }
            set { _clave = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
    }
}
