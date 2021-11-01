using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfEx.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string Customer_id { get; set; }
        public int Employee_id { get; set; }
        public DateTime Date { get; set; } 
        public DateTime Required { get; set; } 
        public DateTime? Shipped_date { get; set; } 
        public int Freight { get; set; }
        public string ShipName { get; set; }
        public string Ship_address{ get; set; }
        public string ShipCity { get; set; }
        public string Ship_postal { get; set; }
        public string Ship_country { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }


        public override string ToString()
        {
            return $"Order_id = {Id}, Customer_id= {Customer_id}, Employee_id = {Employee_id}, " +
                $"Order_date = {Date}, Required_date = {Required}, Shipped_date = {Shipped_date}," +
                $" Freight = {Freight}, Ship_name = {ShipName}, Ship_address = {Ship_address}, Ship_city = {ShipCity}," +
                $"Ship_postal={Ship_postal}, Ship_country={Ship_country}, Order_detail={OrderDetails}";
        }
    }
}
