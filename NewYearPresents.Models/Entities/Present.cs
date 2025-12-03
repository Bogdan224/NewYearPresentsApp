using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Present
    {
        public int Id { get; set; }
        public Packaging? Packaging { get; set; }
        public float TotalWeight { get; set; }
        public float TotalPrice { get; set; }
    }
}
