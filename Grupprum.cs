using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem
{
    internal class Grupprum: Lokal
    {
        public bool Socket { get; set; }
        public Grupprum(String roomType, byte roomNumber, int numberOfChairs, bool socket)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
            NumberOfChairs = numberOfChairs;
            
            Socket = socket;
        }
    }
}
