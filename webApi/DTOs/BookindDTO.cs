using DAL.Models;

namespace webApi.DTOs
{
    public class BookindDTO
    {
        
        public string BookingName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDiscounted { get; set; }
       // public int BranchId { get; set; }
        public string BranchName { get; set; }
        //public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        //public List<int> RoomIds { get; set; }

        public List<RoomDto> Rooms { get; set; }

    }



}
