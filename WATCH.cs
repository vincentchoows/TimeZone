//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TimeZone_Assign
{
    using System;
    using System.Collections.Generic;
    
    public partial class WATCH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WATCH()
        {
            this.ORDER_ITEM = new HashSet<ORDER_ITEM>();
        }
    
        public string WATCH_ID { get; set; }
        public string CATEGORY_ID { get; set; }
        public string GALLERY_ID { get; set; }
        public string REFERENCE_NO { get; set; }
        public string MODEL_CASE { get; set; }
        public string BEZEL { get; set; }
        public string WATER_RESISTANCE { get; set; }
        public string MOVEMENT { get; set; }
        public string CALIBRE { get; set; }
        public string POWER_RESERVE { get; set; }
        public string BRACELET { get; set; }
        public string DIAL { get; set; }
        public string CERTIFICATION { get; set; }
        public string WINDING_CROWN { get; set; }
        public Nullable<int> STOCK_QTY { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public Nullable<bool> STATUS { get; set; }
    
        public virtual CATEGORY CATEGORY { get; set; }
        public virtual GALLERY GALLERY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDER_ITEM> ORDER_ITEM { get; set; }
    }
}