using Bokningssystem;
using System.Runtime.CompilerServices;

internal class Sal : Lokal
{
    public bool Socket { get; set; }

    public Sal(byte roomNumber, int numberOfChairs, bool socket)
        : base("Sal", roomNumber, numberOfChairs)
    {
        Socket = socket;
    }

    public override bool Boka(DateTime startTid, TimeSpan duration, string bokadAv)
    {
        if (IsBooked)
        {
            Console.WriteLine("Salen är redan bokad.");
            return false;
        }

        // Kontrollera kapacitet
        if (NumberOfChairs > 40)
        {
            Console.WriteLine("Bokning misslyckades: Kapacitetsgräns överskriden för Sal.");
            return false;
        }

        // Sätt bokningsinformation
        IsBooked = true;
        BookingStartTime = startTid;
        BookingDuration = duration;
        ClientName = bokadAv;
        BookingID = new Random().Next(1000, 9999);

        Console.WriteLine($"Salen är bokad av {bokadAv} från {startTid} i {duration.TotalHours} timmar.");
        return true;
    }

    public override void Avboka()
    {
        if (!IsBooked)
        {
            Console.WriteLine("Salen är inte bokad och kan därför inte avbokas.");
            return;
        }

        IsBooked = false;
        Console.WriteLine("Salen har avbokats.");
    }
}
