using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class Room
    {
        public Room()
        {
            Reservations = new HashSet<Reservation>();
            RoomExtras = new HashSet<RoomExtra>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public int RoomTypeId { get; set; }
        public bool Deleted { get; set; }

        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<RoomExtra> RoomExtras { get; set; }
    }
}
