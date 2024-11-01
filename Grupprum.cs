using System;

namespace Bokningssystem
{
    internal class Grupprum : Lokal
    {
        public bool Projector { get; set; }

        public Grupprum(byte roomNumber, int numberOfChairs, bool projector)
            : base("Grupprum", roomNumber, numberOfChairs) // Ge korrekt rumstyp
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
