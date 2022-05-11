using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class RoomType
    {
        public RoomType()
        {
            PriceHistories = new HashSet<PriceHistory>();
            RoomOffers = new HashSet<RoomOffer>();
            RoomRoomImages = new HashSet<RoomRoomImage>();
            Rooms = new HashSet<Room>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Features { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<PriceHistory> PriceHistories { get; set; }
        public virtual ICollection<RoomOffer> RoomOffers { get; set; }
        public virtual ICollection<RoomRoomImage> RoomRoomImages { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
