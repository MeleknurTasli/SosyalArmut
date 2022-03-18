namespace Armut.Model
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public bool Visibility { get; set; }
        public Category? Category { get; set; }

        public IEnumerable<Activity>? Activities {get; set;}
    }
}
