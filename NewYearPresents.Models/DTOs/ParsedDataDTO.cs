using NewYearPresents.Models.Entities;

namespace NewYearPresents.Models.DTOs
{
    public class ParsedDataDTO
    {
        public List<Product>? Products { get; set; }
        public List<ProductType>? ProductTypes { get; set; }
        public List<Manufacturer>? Manufacturers { get; set; }
    }
}