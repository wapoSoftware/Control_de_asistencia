//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Assistance_ControlBLL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Horario
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Horario()
        {
            this.HorarioAreas = new HashSet<HorarioArea>();
        }
    
        public int HorarioId { get; set; }
        public string Nombre { get; set; }
        public System.DateTime HoraEntrada { get; set; }
        public System.DateTime HoraSalida { get; set; }
        public int UsuarioRegistro { get; set; }
        public System.DateTime FechaHoraRegistro { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HorarioArea> HorarioAreas { get; set; }
    }
}
