namespace MVC.Models
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public bool HasBookedPreviously { get; set; } = false;
    }
}
