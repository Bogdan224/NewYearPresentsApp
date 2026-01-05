using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class Packaging : BaseEntity
    {
        public string? ImageName { get; set; } = null;
        public float MaxWeight { get; set; }
    }
}
