//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assistance_ControlDLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permiso
    {
        public Permiso()
        {
            this.EmpleadoPermiso = new HashSet<EmpleadoPermiso>();
        }
    
        public int PermisoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioRegistro { get; set; }
        public System.DateTime FechaHoraRegistro { get; set; }
    
        public virtual ICollection<EmpleadoPermiso> EmpleadoPermiso { get; set; }
    }
}
