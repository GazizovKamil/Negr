//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Negr
{
    using System;
    using System.Collections.Generic;
    
    public partial class InventItems
    {
        public int id_II { get; set; }
        public int invent_id { get; set; }
        public int item_id { get; set; }
    
        public virtual Invent Invent { get; set; }
        public virtual Items Items { get; set; }
    }
}