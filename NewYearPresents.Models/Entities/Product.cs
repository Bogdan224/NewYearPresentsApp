using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Product : BaseEntity
    {
        public string? ImageName { get; set; } = null;
        public int ExpirationDate { get; set; }
        public float Weight { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
    }
}
