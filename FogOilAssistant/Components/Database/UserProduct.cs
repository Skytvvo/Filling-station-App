//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FogOilAssistant.Components.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProduct
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int UserProductsId { get; set; }
        public int Status { get; set; }
    
        public virtual Location Location { get; set; }
        public virtual OrderStatu OrderStatu { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
