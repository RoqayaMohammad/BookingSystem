using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public string BookingName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsDiscounted { get; set; }
  
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
