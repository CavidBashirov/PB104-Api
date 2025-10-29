namespace Domain.Entities
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public int SeriesNumber { get; set; }
    }
}
