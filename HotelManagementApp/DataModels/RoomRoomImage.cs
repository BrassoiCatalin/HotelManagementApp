using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class RoomRoomImage
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomImageId { get; set; }
        public bool Deleted { get; set; }

        public virtual RoomImage RoomImage { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
