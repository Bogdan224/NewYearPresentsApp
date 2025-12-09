using NewYearPresents.Models.Entities;

namespace NewYearPresents.Models.DTOs
{
    public class ParsedProductsBoxFileDTO
    {
        public List<Product>? Products { get; set; }
        public List<ProductType>? ProductTypes { get; set; }
        public List<Manufacturer>? Manufacturers { get; set; }
        public List<ProductsBox>? ProductsBoxes { get; set; }
    }
}