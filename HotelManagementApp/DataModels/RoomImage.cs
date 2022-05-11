using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class RoomImage
    {
        public RoomImage()
        {
            RoomRoomImages = new HashSet<RoomRoomImage>();
        }

        public int Id { get; set; }
        public byte[] Picture { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<RoomRoomImage> RoomRoomImages { get; set; }
    }
}
