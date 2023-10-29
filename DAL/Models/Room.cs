using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }

        public RoomType Type { get; set; }

        
        
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<BookingRoom> BookingRooms { get; set; }
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }

    public static class RoomTypeMaxOccupancy
    {
        public const int Standard = 1; // Maximum occupancy for standard rooms
        public const int Double = 4;   // Maximum occupancy for double rooms
        public const int Suite = 6;    // Maximum occupancy for suites
    }

    

}
