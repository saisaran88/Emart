using System;
using System.Collections.Generic;

namespace ShopDAL.ViewModels
{
    public partial class CartDetails
    {
        public int CartId { get; set; }
        public string EmailId { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public byte? CategoryId { get; set; }
        public decimal Price { get; set; }
        public int QuantityRequired { get; set; }

        public Users Email { get; set; }
        public Products Product { get; set; }
    }
}
