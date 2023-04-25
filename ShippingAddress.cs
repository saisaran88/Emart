using System;
using System.Collections.Generic;

namespace ShopDAL.ViewModels
{
    public partial class ShippingAddress
    {
        public int Sno { get; set; }
        public string EmailId { get; set; }
        public long? MobileNumber { get; set; }
        public string Area { get; set; }
        public string LandMark { get; set; }
        public string Town { get; set; }
        public long? Pincode { get; set; }
        public string State { get; set; }
    }
}
