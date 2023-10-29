using DAL.Models;

namespace webApi.DTOs
{
    public class BookingRoomDto
    {
        public string BookingName { get; set; }
        public string RoomNumber { get; set; }
        public int NumberOfPersons { get; set; }
    }
}
