using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp.Models
{
    internal class ReservationToShow
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public float TotalPrice { get; set; }

        public int Status { get; set; }
    }
}
