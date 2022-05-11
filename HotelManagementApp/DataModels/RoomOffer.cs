using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class RoomOffer
    {
        public int Id { get; set; }
        public int RoomTypeId { get; set; }
        public int OfferId { get; set; }
        public bool Deleted { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
