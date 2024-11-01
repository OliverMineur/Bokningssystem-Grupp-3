using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem
{
    internal class Sal: Lokal
    {
        public bool Projector { get; set; }
        public Sal(String roomType, byte roomNumber, int numberOfChairs, bool projector)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
            NumberOfChairs = numberOfChairs;
            Projector = projector;
        }
    }
}
