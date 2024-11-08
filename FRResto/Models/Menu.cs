namespace FRResto.Models
{
    public class Menu
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public List<Option> Options { get; set; }
    }

    public class Option
    {
        public string Name { get; set; }
        public bool IsMultiple { get; set; }
        public List<OptionChoice> Choices { get; set; }
    }

    public class OptionChoice
    {
        public string Value { get; set; }
        public decimal Price { get; set; }
    }
}
