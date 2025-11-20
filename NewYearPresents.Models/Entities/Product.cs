using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Product : BaseEntity
    {
        public float Price30K { get; set; }
        public float Price60K { get; set; }
        public float Price100K { get; set; }
        public float Price150K { get; set; }
        public string? Image { get; set; } = null;
        public int ExpirationDate { get; set; }

        public int ProductTypeId { get; set; }
        public ProductType? ProductType { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer? Manufacturer { get; set; }
    }
}
