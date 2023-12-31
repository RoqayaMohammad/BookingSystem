﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        
        public ICollection<Room> Rooms { get; set; }=new List<Room>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
