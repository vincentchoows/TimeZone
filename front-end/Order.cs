using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeZone_Assign.front_end
{
    public class Order
    {
        public string id { get; set; }
        public string paymentDate { get; set; }
        public string estEarliestDate { get; set; }
        public string estLatestDate { get; set; }
        public string arrivalDate { get; set; }
        public string status { get; set; }
        public string paymentAmount { get; set; }
        public List<OrderItem> orderItemList { get; set; }
    }
}