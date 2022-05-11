using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class RoomExtra
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ExtraId { get; set; }
        public bool Deleted { get; set; }

        public virtual Extra Extra { get; set; }
        public virtual Room Room { get; set; }
    }
}
