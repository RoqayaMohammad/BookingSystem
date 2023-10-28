using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public bool HasBookedPreviously { get; set; } = false;

        public ICollection<Booking> Bookings { get; set; }

        
        
    }
}
