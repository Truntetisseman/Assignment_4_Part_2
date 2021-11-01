using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataServiceLib;
using EfEx.Domain;

namespace WebService.ViewModels
{
    public class ProductViewModel
    {

        public string Url { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
    }
}