using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Manufacturer : BaseEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}
