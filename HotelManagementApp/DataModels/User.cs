using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class User
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
