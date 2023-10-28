namespace webApi.DTOs
{
    public class CustomerDto
    {
        public string Name { get; set; }
        public bool HasBookedPreviously { get; set; } = false;
    }
}
