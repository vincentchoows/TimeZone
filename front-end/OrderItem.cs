using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeZone_Assign.front_end
{
    public class OrderItem
    {
        public string imagePath { get; set; }
        public string name { get; set; }
        public int qty { get; set; }
        public string price { get; set; }
        public string watchId { get; set; }
        public string orderId { get; set; }
        public Boolean isRated { get; set; }
        public string isRatedStr { get; set; }
        public string isRatedCss { get; set; }
    }
}