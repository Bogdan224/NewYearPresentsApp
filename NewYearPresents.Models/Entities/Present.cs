using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Present : BaseEntity
    {
        public Packaging? Packaging { get; set; }
        public float TotalWeight { get; set; }
        public float TotalPrice { get; set; }
    }
}
