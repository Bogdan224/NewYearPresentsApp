using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewYearPresents.Models.Entities
{
    public class ProductsBoxInStorage
    {
        public int Id { get; set; }

        public int Count { get;set; }

        public int ProductsBoxId { get; set; }
        [NotNull] public ProductsBox ProductsBox { get; set; }
    }
}
