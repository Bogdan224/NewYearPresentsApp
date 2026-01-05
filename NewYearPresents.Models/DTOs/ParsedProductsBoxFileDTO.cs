using NewYearPresents.Models.Entities;

namespace NewYearPresents.Models.DTOs
{
    public class ParsedProductsBoxFileDTO(List<Product>? products,
        List<ProductType>? productTypes,
        List<Manufacturer>? manufacturers,
        List<ProductsBox>? productsBoxes)
    {
        public List<Product>? Products { get; } = products;
        public List<ProductType>? ProductTypes { get; } = productTypes;
        public List<Manufacturer>? Manufacturers { get; } = manufacturers;
        public List<ProductsBox>? ProductsBoxes { get; } = productsBoxes;
    }
}