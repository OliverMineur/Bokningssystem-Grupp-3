using Bokningssystem;
using System;

internal class Sal : Lokal
{
    public bool Projector { get; set; }

    public Sal(string roomType, byte roomNumber, int numberOfChairs, bool projector)
        : base(roomType, roomNumber, numberOfChairs)
    {
        Projector = projector;
    }

    //Override av book metoden från lokal (Emilia)
    public override bool Book(DateTime startTid, TimeSpan duration, string clientName)
    {
        // Kontrollera om salen redan är bokad
        if (IsBooked)
        {
            Console.WriteLine("Salen är redan bokad.");
            return false;
        }

        // Kontrollera om antalet stolar överstiger kapacitetsgränsen på 40
        if (NumberOfChairs > 40)
        {
            Console.WriteLine("Bokning misslyckades: Kapacitetsgräns överskriden för Sal.");
            return false;
        }

        // Sätt bokningsinformation
        IsBooked = true;
        BookingStartTime = startTid;
        BookingDuration = duration;
        ClientName = clientName;
        BookingID = GenerateBookingID();
        // Bekräfta bokningen till användaren
        Console.WriteLine($"Salen är bokad av {clientName} från {startTid} i {duration.TotalHours} timmar.");
        Console.WriteLine($"Bokning lyckades! Boknings-ID: {BookingID}");
        return true;
    }
}
