using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class Extra
    {
        public Extra()
        {
            RoomExtras = new HashSet<RoomExtra>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<RoomExtra> RoomExtras { get; set; }
    }
}
