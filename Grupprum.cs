using System;

namespace Bokningssystem
{
    // Representerar ett grupprum som kan bokas och ärver från klassen Lokal
    internal class Grupprum : Lokal
    {
        // Indikerar om grupprummet har en projektor
        public bool Projector { get; set; }

        // Konstruktor för att skapa ett Grupprum med specifik typ, rumsnummer, antal stolar och projektortillgänglighet
        // "roomType"> Typ av rum (t.ex. grupprum)
        // "roomNumber">Rummets nummer
        // "numberOfChairs">Antal stolar i grupprummet
        // "projector">Anger om grupprummet har en projektor
        public Grupprum(string roomType, byte roomNumber, int numberOfChairs, bool projector)
            : base(roomType, roomNumber, numberOfChairs)
        {
            Projector = projector;
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
            return true;
        }
        // Avbokar grupprummet om det är bokat
        public void UnBook()
        {
            // Kontrollera om grupprummet inte är bokat
            if (!IsBooked)
            {
                Console.WriteLine("Grupprummet är inte bokat och kan därför inte avbokas.");
                return;
            }
            // Avboka grupprummet
            IsBooked = false;
            Console.WriteLine("Grupprummet har avbokats.");
        }
    }
}
