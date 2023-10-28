using DAL.Models;

namespace webApi.DTOs
{
    public class BookindDTO
    {
        public int Id { get; set; }
        public string BookingName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDiscounted { get; set; }
        public string BranchName { get; set; }  
        public string CustomerName { get; set; }

    }
}
