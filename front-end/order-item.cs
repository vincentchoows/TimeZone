using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace TimeZone_Assign.front_end
{
    public class order_item
    {
        public String watch_id { get; set; }
        public int qty { get; set; }
        public double price { get; set; }
        public string productName { get; set; }
        public string imagePath { get; set; }
        public double subtotal
        {
            get { return qty * price; }
        }
        
    }

}