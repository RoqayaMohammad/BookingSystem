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
        public ICollection<Booking> Bookings { get; set; }
    }

    public enum RoomType
    {
        Single,
        Double,
        Suite
    }
}
