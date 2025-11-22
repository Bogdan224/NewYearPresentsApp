namespace NewYearPresents.Models.Entities
{
    public class Manufacturer : BaseEntity
    {
        public ICollection<Product>? Products { get; set; }
    }
}