//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shtat.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Uchastok
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Uchastok()
        {
            this.vacancy = new HashSet<vacancy>();
        }
    
        public int IDuch { get; set; }
        public string Uchastok1 { get; set; }
        public Nullable<int> IDGrup { get; set; }
    
        public virtual Gruppa Gruppa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<vacancy> vacancy { get; set; }
    }
}
