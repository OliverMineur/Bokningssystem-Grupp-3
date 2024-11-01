using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Bokningssystem
{
    internal class Grupprum : Lokal
    {
        public bool Projector { get; set; }

        public Grupprum(string roomType, byte roomNumber, int numberOfChairs, bool projector)
            : base(roomType, roomNumber, numberOfChairs)
        {
            Projector = projector;
        }

        public override bool Boka(DateTime startTid, TimeSpan duration, string bokadAv)
        {
            if (IsBooked)
            {
                Console.WriteLine("Grupprummet är redan bokat.");
                return false;
            }

            // Kontrollera kapacitet
            if (NumberOfChairs > 10)
            {
                Console.WriteLine("Bokning misslyckades: Kapacitetsgräns överskriden för Grupprum.");
                return false;
            }

            // Sätt bokningsinformation
            IsBooked = true;
            BookingStartTime = startTid;
            BookingDuration = duration;
            ClientName = bokadAv;
            BookingID = new Random().Next(1000, 9999);

            Console.WriteLine($"Grupprummet är bokat av {bokadAv} från {startTid} i {duration.TotalHours} timmar.");
            return true;
        }

        public override void Avboka()
        {
            if (!IsBooked)
            {
                Console.WriteLine("Grupprummet är inte bokat och kan därför inte avbokas.");
                return;
            }

            IsBooked = false;
            Console.WriteLine("Grupprummet har avbokats.");
        }
    }
}