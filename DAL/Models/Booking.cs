using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime? CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; } = 0;
        public bool IsDiscounted { get; set; }
        public int TotalNumberOfPersons => BookingRooms.Sum(br => br.NumberOfPersons);

        public int BranchId { get; set; }
        public Branch Branch { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<BookingRoom> BookingRooms { get; set; }
        public bool HasPreviousBookings { get; set; } 


        [NotMapped] 
        public List<int> RoomIds => BookingRooms.Select(br => br.RoomId).ToList();

        //public void ApplyDiscount()
        //{
        //    if (HasPreviousBookings)
        //    {
               
        //        TotalPrice *= 0.05M; 
        //        IsDiscounted = true;
        //    }
        //}
    }

    //public void CheckAndApplyDiscount(Booking booking)
    //{
    //    var customer = booking.Customer;
    //    // Implement logic to check if the customer has previous bookings in your database
    //    bool hasPreviousBookings = CheckIfCustomerHasPreviousBookings(customer);

    //    if (hasPreviousBookings)
    //    {
    //        booking.HasPreviousBookings = true;
    //        booking.ApplyDiscount();
    //    }
    //}

    //public bool CheckIfCustomerHasPreviousBookings(Customer customer)
    //{
    //    // Implement logic to check if the customer has previous bookings in your database
    //    // You might query the database to find customer's previous bookings.
    //    // If the customer has any previous bookings, return true; otherwise, return false.
    //    // This depends on your specific database and data structure.
    //    return true; // Replace with your actual logic.
    //}

}
