using NewYearPresents.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.DTOs
{
    public class ParsedDataDTO
    {
        public List<Product>? Products { get; set; }
        public List<ProductType>? ProductTypes { get; set; }
        public List<Manufacturer>? Manufacturers { get; set; }
    }
}
