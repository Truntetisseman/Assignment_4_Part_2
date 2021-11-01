using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLib;
using EfEx.Domain;

namespace WebService.ViewModels
{
    public class ProductViewModel
    {

        public string Url { get; set; }
        public int Id { get; set; }
        [NotMapped]
        public string Name { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        [NotMapped]
        public string CategoryName { get; set; }
        public Category Category { get; set; }
        public int SupplierId { get; set; }
        public int UnitPrice { get; set; }
        public string QuantityPerUnit { get; set; }
        public int UnitsInStock { get; set; }
    }
}