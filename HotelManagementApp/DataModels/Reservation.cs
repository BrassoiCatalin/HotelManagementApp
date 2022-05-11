using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public float TotalPrice { get; set; }
        public bool Deleted { get; set; }

        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
