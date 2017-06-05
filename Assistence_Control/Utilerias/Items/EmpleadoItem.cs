using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistence_Control.Utilerias.Items
{
    public class EmpleadoItem
    {
        private string _noEmpleado;
        private string _nombre;
        private string _apellidoPaterno;
        private string _apellidoMaterno;
        private int _edad;
        private DateTime _fechaAlta;

        public string NoEmpleado
        {
            get { return _noEmpleado; }
            set { _noEmpleado = value; }
        }
        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; }
        }
        public string ApellidoPaterno
        {
            get { return _apellidoPaterno; }
            set { _apellidoPaterno = value; }
        }
        public string ApellidoMaterno
        {
            get { return _apellidoMaterno; }
            set { _apellidoMaterno = value; }
        }
        public int Edad
        {
            get { return _edad; }
            set { _edad = value; }
        }
        public DateTime FechaAlta
        {
            get { return _fechaAlta; }
            set { _fechaAlta = value; }
        }
    }
}
