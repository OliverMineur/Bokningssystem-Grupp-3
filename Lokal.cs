using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bokningssystem
{
    internal class Lokal
    {
        public String RoomType { get; set; } = "";
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; private set; } = false; // Endast ändras av metoderna i klassen

        public bool Booked()
        {
            if (!IsBooked)
            {
                IsBooked = true; // Uppdaterar klassens variabel
                return true; // Bokning lyckad
            }
            return false; // Lokalen är redan bokad
        }

        public void Unbook()
        {
            IsBooked = false; // Återställer bokningsstatus
        }
    }
}
