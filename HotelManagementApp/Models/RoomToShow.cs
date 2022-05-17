using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagementApp.Models
{
    internal class RoomToShow
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Features { get; set; }

        public float Price { get; set; }

        public byte[] Image { get; set; }
    }
}
