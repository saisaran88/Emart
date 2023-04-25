using ShopDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ShopDAL
{
    public class ShopMallRepository
    {
        ShopMallDBContext context;
        private readonly ShopMallDBContext _context;

        public static long orderid = 100;
        public ShopMallRepository()
        {
            context = new ShopMallDBContext();
        }


        public ShopMallRepository(ShopMallDBContext context)
        {
            _context = context;
        }

        public bool ValidateEmailId(string Email)
        {
            bool status = false;
            int count = 0;
            List<String> emailList = _context.Users.Select(m => m.EmailId).ToList();
            foreach (var emailId in emailList)
            {
                if (emailId == Email)
                {

                    count++;
                }

            }
            if (count > 0)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }
        public bool GetUserNamebyEmail(String UserName)
        {
            bool status = false;
            try
            {

                var username = _context.Users.Where(p => p.EmailId.Contains(UserName)).FirstOrDefault();
                if (username != null)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return status;
        }
        public string GetNextProductId()
        {
            var row = _context.Products.LastOrDefault();
            string lid = row.ProductId;
            string num = lid.Split("P")[1];
            int nextid = Convert.ToInt32(num) + 1;
            return "P" + Convert.ToString(nextid);
        }

        public List<Products> GetProducts()
        {
            List<Products> prodlist = new List<Products>();
            List<Products> reslist = new List<Products>();
            try
            {
                prodlist = _context.Products.Where(p => p.QuantityAvailable > 0  ).ToList();
                foreach (var item in prodlist)
                {
                    string id = item.ProductId.Split("P")[1];
                    if(Convert.ToInt32(id) > 157)
                    {
                        reslist.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                reslist = null;
                // throw;
            }
            return reslist;
        }

        public bool ValidateCategory(String categoryName)
        {
            bool status = false;
            try
            {
                var obj = _context.Categories.Where(c => c.CategoryName == categoryName).FirstOrDefault();
                if (obj != null)
                {
                    status = false;
                }
                else
                    status = true;
            }
            catch (Exception e)
            {
                status = false;
            }
            return status;
        }
        public List<Products> GetProductsByCategory(byte categoryid)
        {
            List<Products> plist = new List<Products>();
            try
            {
                plist = _context.Products.Where(p => p.CategoryId == categoryid).ToList();
            }
            catch (Exception)
            {
                plist = null;
            }
            return plist;
        }

        public bool AddProduct(Products product)
        {
            try
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool UpdateProduct(Products product)
        {
            bool status = false;
            try
            {
                var prod = _context.Products.Find(product.ProductId);
                if (prod != null)
                {
                    prod.ProductName = product.ProductName;
                    prod.Price = product.Price;
                    prod.QuantityAvailable = product.QuantityAvailable;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteProduct(string productid)
        {
            bool status = false;
            try
            {
                var prod = _context.Products.Find(productid);
                if (prod != null)
                {
                    _context.Products.Remove(prod);
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;

            }
            return status;
        }

        public List<Categories> GetCategories()
        {
            List<Categories> catlist = new List<Categories>();
            try
            {
                catlist = _context.Categories.OrderBy(c => c.CategoryId).ToList();
            }
            catch (Exception)
            {
                catlist = null;
            }
            return catlist;
        }

        public bool AddCategory(Categories category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }
        }

        public bool UpdateCategory(Categories cat)
        {
            bool status = false;
            try
            {
                var prod = _context.Categories.Find(cat.CategoryId);
                if (prod != null)
                {
                    prod.CategoryName = cat.CategoryName;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        public bool DeleteCategory(byte categoryId)
        {
            bool status = false;
            try
            {
                var prod = _context.Categories.Find(categoryId);
                if (prod != null)
                {
                    _context.Categories.Remove(prod);
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public List<PurchaseDetails> GetOrders()
        {
            var prodlist = new List<PurchaseDetails>();
            try
            {
                prodlist = _context.PurchaseDetails.OrderBy(p => p.PurchaseId).ToList();
            }
            catch (Exception)
            {
                prodlist = null;
            }
            return prodlist;
        }



        public byte? ValidateCredentials(string userid, string paswd)
        {
            var obj = _context.Users.Find(userid);
            if (obj.UserPassword == paswd)
            {
                return obj.RoleId;
            }
            else
            {
                return null;
            }
        }


        public bool RegisterUser(Users user)
        {
            bool status = false;
            try
            {
                user.RoleId = 2;
                _context.Users.Add(user);
                _context.SaveChanges();
                status = true;

            }
            catch (Exception)
            {
                status = false;
                // throw;
            }
            return status;
        }

        public int GetQuantity(string productid)
        {
            try
            {
                var obj = _context.Products.Find(productid);
                if (obj != null)
                    return obj.QuantityAvailable;
                else
                    return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UpdateQuantity(string productid, int quantavailable)
        {
            bool status = false;
            try
            {
                var obj = _context.Products.Find(productid);
                obj.QuantityAvailable -= quantavailable;
                _context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        public bool AddProductToCart(CartDetails product)
        {
            try
            {
                var list = GetProductsFromCart(product.EmailId);
                int flag = 0;
                var prodobj = _context.Products.Find(product.ProductId);
                foreach (var item in list)
                {
                    if (item.ProductId == product.ProductId)
                    {
                        if (item.QuantityRequired + product.QuantityRequired <= prodobj.QuantityAvailable)
                        {

                            item.QuantityRequired += product.QuantityRequired;
                            item.Price = item.QuantityRequired * prodobj.Price;
                            flag = 1;
                            _context.SaveChanges();
                        }
                        else
                        {
                            return false;
                        }

                    }
                }
                if (flag == 0)
                {
                    _context.CartDetails.Add(product);
                    _context.SaveChanges();
                    return true;
                }
                else
                    return true;
            }
            catch (Exception)
            {
                return false;
                //throw;
            }

        }

        public List<CartDetails> GetProductsFromCart(string username)
        {
            List<CartDetails> prodlist = new List<CartDetails>();
            try
            {
                prodlist = _context.CartDetails.Where(p => p.EmailId == username).ToList();
            }
            catch (Exception)
            {
                prodlist = null;
                // throw;
            }
            return prodlist;
        }

        public bool UpdateCartProduct(CartDetails cart)
        {
            bool status = false;
            try
            {
                var prod = _context.CartDetails.Find(cart.CartId);
                if (prod != null)
                {
                    prod.CategoryId = cart.CategoryId;
                    prod.EmailId = cart.EmailId;
                    prod.ProductId = cart.ProductId;
                    prod.ProductName = cart.ProductName;
                    var obj = _context.Products.Find(cart.ProductId);
                    prod.Price = obj.Price * cart.QuantityRequired;
                    prod.QuantityRequired = cart.QuantityRequired;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public bool DeleteCartProduct(int id)
        {
            bool status = false;
            try
            {
                var obj = _context.CartDetails.Find(id);
                if (obj != null)
                {
                    _context.Remove(obj);
                    _context.SaveChanges();
                    status = true;
                }
                else
                    status = false;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }


        public decimal GetTotalBill(string name)
        {
            decimal sum = 0;
            try
            {
                var list = _context.CartDetails.Where(c => c.EmailId == name).ToList();
                foreach (var item in list)
                {
                    sum += item.Price;
                }
            }
            catch (Exception)
            {
                return 0;
                //throw;
            }
            return sum;
        }

        public bool UpdateDataBase(string name, CardDetail carddetails)
        {
            bool status = false;
            try
            {
                if (carddetails.ExpiryDate >= DateTime.Now)
                {
                    var obj = GetProductsFromCart(name);
                    orderid = GetOrderid();
                    foreach (var item in obj)
                    {
                        // var temp = item;
                        NewPurchaseDetails purchasedetails = new NewPurchaseDetails();
                        purchasedetails.OrderId = orderid;
                        purchasedetails.DateOfPurchase = DateTime.Now;
                        var cartobj = _context.CartDetails.Where(c => c.EmailId == name).FirstOrDefault();
                        purchasedetails.QuantityPurchased = Convert.ToSByte(cartobj.QuantityRequired);
                        purchasedetails.Product = item.ProductId;
                        purchasedetails.EmailId = name;
                        var prodobj = _context.Products.Find(item.ProductId);
                        prodobj.QuantityAvailable -= item.QuantityRequired;
                        _context.NewPurchaseDetails.Add(purchasedetails);
                        _context.CartDetails.Remove(cartobj);
                        _context.SaveChanges();
                    }
                    _context.CardDetail.Add(carddetails);
                    _context.SaveChanges();
                    status = true;
                }
                else
                    status = false;

            }
            catch (Exception)
            {
                status = false;
                //throw;
            }
            return status;
        }


        public List<NewPurchaseDetails> GetCustomerPurchasedproducts(string name)
        {
            var list = new List<NewPurchaseDetails>();
            var reslist = new List<NewPurchaseDetails>();
            int flag = 0;
            long id = 0;
            try
            {
                list = _context.NewPurchaseDetails.Where(p => p.EmailId == name).ToList();
                foreach (var item in list)
                {
                    flag = 0;
                    id = item.OrderId;
                    foreach (var res in reslist)
                    {
                        if (id == res.OrderId)
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        reslist.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                reslist = null;
                //throw;
            }
            return reslist;
        }

        public List<PurchaseDetails> GetAllPurchasedDetails()
        {
            var list = new List<PurchaseDetails>();
            try
            {
                list = _context.PurchaseDetails.OrderBy(c => c.PurchaseId).ToList();
            }
            catch (Exception)
            {
                list = null;
                //throw;
            }
            return list;
        }

        public bool UpdatePassword(Users user)
        {
            bool status = false;
            try
            {
                var obj = _context.Users.Find(user.EmailId);
                if (obj != null && obj.UserPassword != user.UserPassword)
                {
                    obj.UserPassword = user.UserPassword;
                    _context.SaveChanges();
                    status = true;
                }
                else
                    status = false;
            }
            catch (Exception)
            {
                status = false;
                //throw;
            }
            return status;
        }

        public bool updateDatabases(string name)
        {
            bool status = false;
            try
            {
                var obj = GetProductsFromCart(name);
                orderid = GetOrderid();
                foreach (var item in obj)
                {
                    var temp = item;
                    NewPurchaseDetails purchasedetails = new NewPurchaseDetails();
                    purchasedetails.OrderId = orderid;
                    purchasedetails.DateOfPurchase = DateTime.Now;
                    var cartobj = _context.CartDetails.Where(c => c.EmailId == name).FirstOrDefault();
                    purchasedetails.QuantityPurchased = Convert.ToSByte(cartobj.QuantityRequired);
                    purchasedetails.Product = item.ProductId;
                    purchasedetails.EmailId = name;
                    var prodobj = _context.Products.Find(item.ProductId);
                    prodobj.QuantityAvailable -= item.QuantityRequired;
                    // _context.CardDetails.Add(carddetails);
                    _context.NewPurchaseDetails.Add(purchasedetails);
                    _context.CartDetails.Remove(cartobj);

                    _context.SaveChanges();
                }
                status = true;
            }
            catch (Exception)
            {
                status = false;
                //throw;
            }
            return status;
        }

        public string GetPhoneNumber(string name)
        {
            var obj = _context.Users.Find(name);
            if (obj != null)
            {
                return obj.Phone;
            }
            else
                return null;
        }

        public List<Products> GetProductsByName(string prodname)
        {
            List<Products> prodlist = null;
            List<Products> reslist = null;
            try
            {
                prodlist = new List<Products>();
                prodlist = _context.Products.Where(p => p.ProductName.Contains(prodname)).ToList();

                
            }
            catch (Exception)
            {
                prodlist = null;
            }
            return prodlist;

        }

        public List<NewPurchaseDetails> GetOrderList(long id)
        {
            var list = new List<NewPurchaseDetails>();
            try
            {
                list = _context.NewPurchaseDetails.Where(p => p.OrderId == id).ToList();

            }
            catch (Exception)
            {
                return null;
                // throw;
            }
            return list;
        }

        public List<NewPurchaseDetails> GetAdminOrderDetails(long id, string name)
        {
            var list = new List<NewPurchaseDetails>();
            try
            {
                list = _context.NewPurchaseDetails.Where(p => p.OrderId == id && p.EmailId == name).ToList();
            }
            catch (Exception)
            {
                return null;
                //throw;
            }
            return list;
        }
        public long GetOrderid()
        {
            long id = 0;
            try
            {
                var obj = _context.NewPurchaseDetails.LastOrDefault();
                if (obj != null)
                {
                    id = obj.OrderId + 1;
                }
                else
                    id = 100;
            }
            catch (Exception)
            {
                id = 1000;
                //throw;
            }
            return id;
        }

        public List<NewPurchaseDetails> GetAdminPurchasedproducts()
        {

            var list = new List<NewPurchaseDetails>();
            var reslist = new List<NewPurchaseDetails>();
            int flag = 0;
            long id = 0;
            try
            {
                list = _context.NewPurchaseDetails.ToList();
                foreach (var item in list)
                {
                    flag = 0;
                    id = item.OrderId;
                    foreach (var res in reslist)
                    {
                        if (id == res.OrderId)
                        {
                            flag = 1;
                        }
                    }
                    if (flag == 0)
                    {
                        reslist.Add(item);
                    }
                }
            }
            catch (Exception)
            {
                reslist = null;
                //throw;
            }
            return reslist;
        }

        public List<ShippingAddress> GetAddressList(string name)
        {
            var list = new List<ShippingAddress>();
            try
            {
                list = _context.ShippingAddress.Where(p =>p.EmailId == name ).ToList();
            }
            catch (Exception)
            {
                list = null;
                //throw;
            }
            return list;
        }


        public bool AddAddress(ShippingAddress obj)
        {
            try
            {
                _context.ShippingAddress.Add(obj);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateAddress(ShippingAddress obj,string name,string area)
        {
            bool status = false;
            try
            {
                var prod = _context.ShippingAddress.Where(p => p.EmailId == name && p.Area == area).FirstOrDefault();
                if (prod != null)
                {
                    prod.LandMark = obj.LandMark;
                    prod.Pincode = obj.Pincode;
                    prod.State = obj.State;
                    prod.Town = obj.Town;
                    prod.Area = obj.Area;
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }    
            
        public bool DeleteAddress(int obj,string name)
        {
            bool status = false;
            try
            {
                var prod = _context.ShippingAddress.Where(p => p.EmailId == name).FirstOrDefault();
                if (prod != null)
                {
                    _context.ShippingAddress.Remove(prod);
                    _context.SaveChanges();
                    status = true;
                }
            }
            catch (Exception)
            {
                status = false;

            }
            return status;
        }
    }
}
