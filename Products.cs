using System;
using System.Collections.Generic;

namespace ShopDAL.ViewModels
{
    public partial class Products
    {
        public Products()
        {
            CartDetails = new HashSet<CartDetails>();
            NewPurchaseDetails = new HashSet<NewPurchaseDetails>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
        }

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public byte? CategoryId { get; set; }
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }

        public Categories Category { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<NewPurchaseDetails> NewPurchaseDetails { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; }
    }
}
