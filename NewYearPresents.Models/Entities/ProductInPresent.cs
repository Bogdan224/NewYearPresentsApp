using System.Diagnostics.CodeAnalysis;

namespace NewYearPresents.Models.Entities
{
    public class ProductInPresent
    {
        public int Id { get; set; }

        public int Count { get; set; }

        public int ProductId { get; set; }
        [NotNull] public Product Product { get; set; }

        public int PresentId { get; set; }
        [NotNull] public Present Present { get; set; }
    }
}
