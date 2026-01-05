using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Order : BaseEntity
    {
        public string? ClientName { get; set; }
        public int TotalPrice { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
