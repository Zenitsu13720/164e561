//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetShop.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderProduct = new HashSet<OrderProduct>();
        }
    
        public int OrderID { get; set; }
        public Nullable<int> UserID { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime DeliveryDate { get; set; }
        public Nullable<int> PickPoint { get; set; }
        public Nullable<int> GetCode { get; set; }
        public Nullable<int> StatusID { get; set; }
    
        public virtual PickPoint PickPoint1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual User User { get; set; }
    }
}