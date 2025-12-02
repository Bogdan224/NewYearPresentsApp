using System.Diagnostics.CodeAnalysis;

namespace NewYearPresents.Models.Entities
{
    public class PackagingInStorage
    {
        public int Id { get; set; }
        
        public int Count { get; set; }

        public int PackagingId { get; set; }
        [NotNull] public Packaging Packaging { get; set; }
    }
}
