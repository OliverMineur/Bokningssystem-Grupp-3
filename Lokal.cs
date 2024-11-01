using System;

namespace Bokningssystem
{
    public abstract class Lokal : IBookable
    {
        public string RoomType { get; set; }
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; protected set; }
        public DateTime BookingStartTime { get; protected set; }
        public TimeSpan BookingDuration { get; protected set; }
        public string ClientName { get; protected set; }
        public int BookingID { get; protected set; }


        // Konstruktor för att skapa en ny lokal
        public Lokal(string roomType, byte roomNumber, int numberOfChairs)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
            NumberOfChairs = numberOfChairs;
        }

        // Implementerar metoden Boka från IBookable
        public virtual bool Boka(DateTime startTid, TimeSpan duration, string bokadAv)
        {
            if (IsBooked)
            {
                Console.WriteLine("Rummet är redan bokat.");
                return false; // Rummet är redan bokat
            }

            // Kontrollera kapacitet beroende på rumstyp
            if ((RoomType == "Sal" && NumberOfChairs > 40) ||
                (RoomType == "Grupprum" && NumberOfChairs > 10))
            {
                Console.WriteLine("Bokning misslyckades på grund av kapacitetsbegränsning.");
                return false; // Bokningen misslyckades på grund av kapacitetsbegränsning
            }

            // Sätt bokningsinformation
            IsBooked = true;
            BookingStartTime = startTid;
            BookingDuration = duration;
            ClientName = bokadAv;
            BookingID = new Random().Next(1000, 9999); // Generera ett slumpmässigt boknings-ID

            Console.WriteLine($"Rummet är bokat av {bokadAv} från {startTid} i {duration.TotalHours} timmar.");
            return true; // Bokningen lyckades
        }

        // Implementerar metoden Avboka från IBookable
        public virtual void Avboka()
        {
            if (!IsBooked)
            {
                Console.WriteLine("Rummet är inte bokat och kan därför inte avbokas.");
                return;
            }

            IsBooked = false;
            Console.WriteLine("Rummet har avbokats.");
        }
    }
}
