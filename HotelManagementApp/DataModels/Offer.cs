using System;
using System.Collections.Generic;

#nullable disable

namespace HotelManagementApp.DataModels
{
    public partial class Offer
    {
        public Offer()
        {
            RoomOffers = new HashSet<RoomOffer>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<RoomOffer> RoomOffers { get; set; }
    }
}
