namespace NewYearPresents.Models.Entities
{
    public class ProductType : BaseEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}