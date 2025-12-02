namespace NewYearPresents.Models.Entities
{
    public class ProductType : BaseEntity
    {
        public ICollection<ProductsBox>? Products { get; set; }
    }
}