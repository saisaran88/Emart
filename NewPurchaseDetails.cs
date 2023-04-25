using System;
using System.Collections.Generic;

namespace ShopDAL.ViewModels
{
    public partial class NewPurchaseDetails
    {
        public long PurchaseId { get; set; }
        public long OrderId { get; set; }
        public string EmailId { get; set; }
        public string Product { get; set; }
        public short QuantityPurchased { get; set; }
        public DateTime DateOfPurchase { get; set; }

        public Users Email { get; set; }
        public Products ProductNavigation { get; set; }
    }
}
