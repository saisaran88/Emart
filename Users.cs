using System;
using System.Collections.Generic;

namespace ShopDAL.ViewModels
{
    public partial class Users
    {
        public Users()
        {
            CartDetails = new HashSet<CartDetails>();
            NewPurchaseDetails = new HashSet<NewPurchaseDetails>();
            PurchaseDetails = new HashSet<PurchaseDetails>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string UserPassword { get; set; }
        public byte? RoleId { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        public Roles Role { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }
        public ICollection<NewPurchaseDetails> NewPurchaseDetails { get; set; }
        public ICollection<PurchaseDetails> PurchaseDetails { get; set; }
    }
}
