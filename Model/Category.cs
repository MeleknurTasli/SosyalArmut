namespace Armut.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Visibility { get; set; }
        public IEnumerable<SubCategory>? SubCategories {get; set;}
    }
}
