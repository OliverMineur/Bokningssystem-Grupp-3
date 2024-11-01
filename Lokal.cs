using System;
using System.Collections.Generic;
using System.Linq;

namespace Bokningssystem
{
    internal class Lokal
    {
        public string RoomType { get; set; } = ""; // "Sal" eller "Grupprum"
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; private set; } = false; // Endast ändras via metoder
        public DateTime BookingStartTime { get; set; }
        public TimeSpan BookingDuration { get; set; }
        public string ClientName { get; set; }
        public int BookingID { get; private set; } // Boknings-ID, sätts vid bokning

        // Lista för alla rum
        public static List<Lokal> AllRooms = new List<Lokal>();

        // Funktion för att boka ett rum
        public bool BookRoom(string clientName, DateTime startTime, TimeSpan duration)
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
                Console.WriteLine("Rummet överstiger kapacitetsgränsen.");
                return false; // Bokning misslyckades pga kapacitet
            }

            // Generera ett unikt bokningsnummer
            BookingID = GenerateBookingID();
            ClientName = clientName;
            BookingStartTime = startTime;
            BookingDuration = duration;
            IsBooked = true; // Uppdatera bokningsstatus
            AllRooms.Add(this); // Lägg till i listan med bokade rum

            Console.WriteLine($"Bokning lyckades! Boknings-ID: {BookingID}");
            return true; // Bokning lyckades
        }

        // Funktion för att avboka ett rum
        public void Unbook()
        {
            IsBooked = false; // Återställ bokningsstatus
            AllRooms.Remove(this); // Ta bort från listan med bokade rum
            Console.WriteLine("Rummet har avbokats.");
        }

        // Metod för att generera ett unikt boknings-ID
        private int GenerateBookingID()
        {
            int newBookingID;
            Random rnd = new Random();

            do
            {
                newBookingID = 1 * 1000 + rnd.Next(1, 1000);
            } while (AllRooms.Any(x => x.BookingID == newBookingID));

            return newBookingID;
        }
    }
}
