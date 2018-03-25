//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LogisticsProject.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pojazdy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pojazdy()
        {
            this.Zlecenia = new HashSet<Zlecenia>();
        }
    
        public int ID { get; set; }
        public string NrRejestracyjny { get; set; }
        public int MasaCalkowita { get; set; }
        public double Stawka { get; set; }
        public string Kierowca { get; set; }
        public string NrKomorkowyKierowcy { get; set; }
        public Nullable<int> IDPrzewoznika { get; set; }
        public Nullable<int> IDNaczepy { get; set; }
    
        public virtual Naczepy Naczepy { get; set; }
        public virtual Przewoznicy Przewoznicy { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zlecenia> Zlecenia { get; set; }
    }
}
