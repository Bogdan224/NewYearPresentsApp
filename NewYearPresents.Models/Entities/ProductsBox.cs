using System.Diagnostics.CodeAnalysis;

namespace NewYearPresents.Models.Entities
{
    public class ProductsBox
    {
        public int Id { get; set; }
        public float Price30K { get; set; }
        public float Price60K { get; set; }
        public float Price100K { get; set; }
        public float Price150K { get; set; }

        [NotNull]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public float TotalWeight { get; set; }
    }
}