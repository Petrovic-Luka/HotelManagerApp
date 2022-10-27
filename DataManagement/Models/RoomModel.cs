using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataManagement
{
    public class RoomModel
    {
        public string Room_Number { get; set; }

        public int Beds_Number { get; set; }

         public string Display { get { return $"Room: {Room_Number} Beds: {Beds_Number}"; } }

    }
}
