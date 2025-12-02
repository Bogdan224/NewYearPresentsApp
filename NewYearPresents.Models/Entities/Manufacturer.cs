namespace NewYearPresents.Models.Entities
{
    public class Manufacturer : BaseEntity
    {
        public ICollection<ProductsBox>? Products { get; set; }
    }
}