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
    
    public partial class Organiz
    {
        public int IDOrg { get; set; }
        public Nullable<int> IDPodrUp { get; set; }
        public Nullable<int> IDPodr { get; set; }
        public Nullable<int> IDTip { get; set; }
        public Nullable<System.DateTime> Datein { get; set; }
        public Nullable<System.DateTime> Dateout { get; set; }
        public Nullable<int> Priznak { get; set; }
    }
}
