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
    
    public partial class HorarioArea
    {
        public int HorarioId { get; set; }
        public int AreaId { get; set; }
        public System.DateTime FechaInicio { get; set; }
        public System.DateTime FechaFinal { get; set; }
        public string UsuarioRegistro { get; set; }
        public System.DateTime FechaHoraRegistro { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Horario Horario { get; set; }
    }
}
