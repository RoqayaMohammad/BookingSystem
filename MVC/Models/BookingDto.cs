namespace MVC.Models
{
    public class BookingDto
    {
        public string BookingName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDiscounted { get; set; }
    }
}
