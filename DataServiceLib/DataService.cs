using EfEx;
using EfEx.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataServiceLib
{
    public class DataService : IDataService
    {
        private static readonly NorthwindContext ctx = new();
        public bool CreateCategory(Category cat)
        {
            cat.Id = ctx.Categories.Max(x => x.Id) + 1;
            ctx.Add(cat);
            
            return ctx.SaveChanges() > 0;
        }

        public List<Order> GetOrders()
        {
            List<Order> returns = new();
            ctx.Orders.ForEachAsync(order => {
                returns.Add(new Order { Id = order.Id, Date = order.Date, ShipName = order.ShipName, ShipCity = order.ShipCity });
                Console.WriteLine(order.Id);
            });
            return returns;
        }

        public IList<Product> GetProducts()
        {
            return ctx.Products.ToList();
        }

        public Order GetOrder(int id)
        {
            Order returns = ctx.Orders.Find(id);
            returns.OrderDetails = ctx.OrderDetails.Where(x => x.OrderId == id).ToListAsync().Result;
            returns.OrderDetails.ForEach(o => {
                o.Product = ctx.Products.Find(o.ProductId);
                o.Product.Category = ctx.Categories.Find(o.Product.CategoryId);
            });
            return returns;
        }
        //Task 4
        public List<OrderDetails> GetOrderDetailsByOrderId(int id)
        {
            Order returns = ctx.Orders.Find(id);
            returns.OrderDetails = ctx.OrderDetails.Where(x => x.OrderId == id).ToListAsync().Result;
            returns.OrderDetails.ForEach(o => {
                o.Product = ctx.Products.Find(o.ProductId);
                o.Product.Category = ctx.Categories.Find(o.Product.CategoryId);
            });

            return returns.OrderDetails;

        }

        //Task 5
        public List<OrderDetails> GetOrderDetailsByProductId(int id)
        {
            List<OrderDetails> returns = ctx.OrderDetails.Where(od => od.ProductId == id).ToListAsync().Result;

            returns.ForEach(orderdetail => {
                Console.WriteLine(orderdetail.OrderId);
                //orderdetail.Order = ctx.Orders.Find(orderdetail.OrderId);
            });
            return returns;
        }
        //Task 9
        public Category GetCategory(int id)
        {
            Category c = ctx.Categories.FirstOrDefault(x => x.Id == id);


            if (c != null)
            {
                return c;
            }
            else
            {
                return null;
            }

        }

        //Task 10
        public IList<Category> GetCategories()
        {
            return ctx.Categories.ToList();
        }

        //Task 11
        public bool CreateCategory(int id, string name, string description)
        {
            Category c = new Category
            {
                Id = id,
                Name = name,
                Description = description

            };

            c.Id = ctx.Categories.Max(x => x.Id) + 1;
            ctx.Add(c);
            return ctx.SaveChanges() > 0;
        }

        //Task 12
        public bool UpdateCategory(Category cat)
        {
            Category c = ctx.Categories.FirstOrDefault(x => x.Id == cat.Id);

            //find original, and update with the modified category from controller
            if (c != null)
            {
                c.Name = cat.Name;
                c.Description = cat.Description;

                return ctx.SaveChanges() > 0;
            }
            else
            {
                return false;
            }

        }





        //Task 13
        public bool DeleteCategory(int id)
        {
            Category c = ctx.Categories.FirstOrDefault(x => x.Id == id);
            if (c != null)
            {
                ctx.Remove(c);
                ctx.SaveChanges();
                return true;
            }
            else { return false; }
        }





        public Product GetProduct(int id)
        {
            Product found = ctx.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            return new Product() { Name = found.Name, UnitPrice = found.UnitPrice, Category = found.Category };
        }


        public List<Product> GetProductByName(string sub)
        {
            List<Product> returns = new();
            ctx.Products.Include(x => x.Category).Where(x => x.Name.ToLower().Contains(sub.ToLower())).ForEachAsync(x => {
                returns.Add(new Product { Name = x.Name, Category = x.Category });
                Console.WriteLine($"{x.Name} + {x.Category}");
            });
            return returns;
        }

        public List<Product> GetProductByCategory(int category_id)
        {
            List<Product> returns = new();
            ctx.Products.Include(x => x.Category).Where(x => x.Category.Id == category_id).ForEachAsync(x =>
            {
                returns.Add(new Product { Name = x.Name, UnitPrice = x.UnitPrice, CategoryName = x.Category.Name });
            });
            return returns;
        }

        //Task 2
        public void Get_full_order_by_Shipping_name(string name)
        {
            var result = ctx.Orders
                .Where(x => x.ShipName == name);

            foreach (var r in result)
            {
                Console.WriteLine(r);
            }
        }

    }
}
