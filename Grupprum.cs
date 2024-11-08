using System;

namespace Bokningssystem
{
    // Representerar ett grupprum som kan bokas och ärver från klassen Lokal
    internal class Grupprum : Lokal
    {
        // Indikerar om grupprummet har en projektor
        public bool Socket { get; set; }

        // Konstruktor för att skapa ett Grupprum med specifik typ, rumsnummer, antal stolar och projektortillgänglighet
        // "roomType"> Typ av rum (t.ex. grupprum)
        // "roomNumber">Rummets nummer
        // "numberOfChairs">Antal stolar i grupprummet
        // "projector">Anger om grupprummet har en projektor
        public Grupprum(string roomType, byte roomNumber, int numberOfChairs, bool socket)
            : base(roomType, roomNumber, numberOfChairs)
        {
            Socket = socket;
        }
        // Bokar grupprummet för en specifik klient under en given tidsperiod
        // "startTid">Starttid för bokningen
        // "duration">Varaktighet för bokningen
        // "bokadAv">Namn på klienten som bokar grupprummet
        // Returnerar true om bokningen lyckades, annars false
        public override bool Book(DateTime startTid, TimeSpan duration, string bokadAv)
        {
            // Kontrollera om grupprummet redan är bokat
            if (IsBooked)
            {
                Console.WriteLine("Grupprummet är redan bokat.");
                return false;
            }

            // Kontrollera om antalet stolar överstiger kapacitetsgränsen på 10
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
            BookingID = GenerateBookingID();

            // Bekräfta bokningen till användaren
            Console.WriteLine($"Grupprummet är bokat av {bokadAv} från {startTid} i {duration.TotalHours} timmar.");
            Console.WriteLine($"Bokning lyckades! Boknings-ID: {BookingID}");
            return true;
        }
    }
}
