using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp.Models
{
    public class OfferToShow
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public float Price { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public string RoomType { get; set; }

    }
}
