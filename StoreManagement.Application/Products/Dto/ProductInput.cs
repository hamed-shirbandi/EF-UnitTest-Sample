using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Products.Dto
{
    public class ProductInput
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int NewPrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsTrashed { get; set; }
    }
}
