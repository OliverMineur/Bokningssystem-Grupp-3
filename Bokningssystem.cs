using System.ComponentModel.Design;
using System.Net.Sockets;

namespace Bokningssystem
{
    // Klass som hanterar bokningssystemet för lokaler (t.ex. salar och grupprum)
    internal class Bokningssystem
    {
        // Lista över alla lokaler i systemet
        public static List<Lokal> AllRooms = new List<Lokal>();

        // Metod som visar alla bokningar, med möjlighet att sortera och filtrera baserat på input (Oliver)
        public static void ShowAllBookings(int whatToSort, int ascendingOrDecending)
        {
            List<Lokal> orderedList = null; // Lista för sorterade lokaler
            bool roomsBooked = false; // Indikerar om några rum är bokade
            try
            {
                Console.Clear();
                // Välj sorteringsmetod och ordning baserat på argumenten whatToSort och ascendingOrDecending
                switch ((whatToSort, ascendingOrDecending))
                {
                    case (0, 0):
                        foreach (Lokal room in AllRooms)
                        {
                            if (room.IsBooked == true)
                            {
                                Console.WriteLine("" + room.RoomNumber + " bokad av " +
                                                        room.ClientName + " från " +
                                                        room.BookingStartTime.ToString("M") + " " +
                                                        room.BookingStartTime.ToString("HH:mm") + " till " +
                                                        (room.BookingStartTime + room.BookingDuration).ToString("HH:mm"));
                                roomsBooked = true;
                            }
                        }
                        if (!roomsBooked)
                        {
                            Console.WriteLine("Inga rum är bokade.");
                        }
                        break;
                    case (1, 1):
                        orderedList = AllRooms.OrderBy(x => x.RoomType).ToList(); // Sortera efter rumstyp i stigande ordnin
                        break;
                    case (2, 1):
                        orderedList = AllRooms.OrderBy(x => x.BookingStartTime).ToList(); // Sortera efter starttid för bokningen i stigande ordning
                        break;
                    case (3, 1):
                        orderedList = AllRooms.OrderBy(x => x.RoomNumber).ToList(); // Sortera efter rumsnummer i stigande ordning
                        break;
                    case (1, 2):
                        orderedList = AllRooms.OrderByDescending(x => x.RoomType).ToList(); // Sortera efter rumstyp i fallande ordning
                        break;
                    case (2, 2):
                        orderedList = AllRooms.OrderByDescending(x => x.BookingStartTime).ToList(); // Sortera efter starttid för bokningen i fallande ordning
                        break;
                    case (3, 2):
                        orderedList = AllRooms.OrderByDescending(x => x.RoomNumber).ToList(); // Sortera efter rumsnummer i fallande ordning
                        break;
                    case (4, 1): // Filtrera bokningar efter ett specifikt år
                        Console.Clear();
                        Console.WriteLine("Vilket år vill du söka på:\nFormat exempel \"1998\"");
                        if(int.TryParse(Console.ReadLine(), out int searchForYear))
                        {
                            foreach (Lokal room in AllRooms)
                            {
                                if (searchForYear == room.BookingStartTime.Year)
                                {
                                    Console.WriteLine("" + room.RoomNumber + " bokad av " +
                                                            room.ClientName + " från " +
                                                            room.BookingStartTime.ToString("M") + " " +
                                                            room.BookingStartTime.ToString("HH:mm") + " till " +
                                                            (room.BookingStartTime + room.BookingDuration).ToString("HH:mm"));
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ogiltig inmatning.");
                        }

                        break;
                    default: // Felaktigt val
                        Console.WriteLine("Inget av alternativen valdes.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Hantera fel och visa felmeddelande
            }
            finally
            {
                // Om orderedList är inte null, visa sorterade bokningar
                if (orderedList != null)
                {
                    foreach (Lokal room in orderedList)
                    {
                        if (room.IsBooked == true)
                        {
                            Console.WriteLine("" + room.RoomNumber + " bokad av " +
                                                room.ClientName + " från " +
                                                room.BookingStartTime.ToString("M") + " " +
                                                room.BookingStartTime.ToString("HH:mm") + " till " +
                                                (room.BookingStartTime + room.BookingDuration).ToString("HH:mm"));
                        }
                    }
                }
            }
        }
        // Metod för att filtrera bokningar baserat på användarens val (Oliver)
        public static void FilterBookings()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Hur vill du filtrera listan?\n1:Sortera efter rumstyp\n2:Sortera efter datum\n3:Sortera efter rum nummer\n4:Visa bokningar på specifika år");
                int firstChoice = int.Parse(Console.ReadLine());
                Console.Clear();
                Console.WriteLine("Visa i:\n1:Stigande ordning\n2:Fallande ordning");
                int secondChoice = int.Parse(Console.ReadLine());
                ShowAllBookings(firstChoice, secondChoice); // Anropa ShowAllBookings med valda sorteringsalternativ
            }
            catch (FormatException)
            {
                Console.WriteLine("Ogiltig inskrift, ange bara ett nummer.");
            }
        }
        // Huvudmetoden som startar programmet och visar menyalternativ för användaren
        static void Main(string[] args)
        {
            Lokal.LoadRoomsFromFile();

            while (true)
            {
                // Visa huvudmenyn
                Console.WriteLine("Bokningssystem:\n1:Hantera bokningar\n2:Hantera lokaler\n3:Spara och avsluta");
                if (int.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    Console.Clear();
                    switch (menuChoice)
                    {
                        case 1: // Alternativ för att hantera bokningar
                            Console.WriteLine("Hantera bokningar:\n1:Skapa bokning\n2:Se alla bokningar\n3:Filtrera alla bokningar\n4:Ta bort bokning\n5:Uppdatera bokning");
                            if (int.TryParse(Console.ReadLine(), out int secondMenuChoice))
                            {
                                switch (secondMenuChoice)
                                {
                                    case 1:
                                        //Skapa bokning
                                        Lokal.BokningsMeny();
                                        break;
                                    case 2: // Visa alla bokningar
                                        ShowAllBookings(0, 0);
                                        break;
                                    case 3: // Filtrera alla bokningar
                                        FilterBookings();
                                        break;
                                    case 4: // Avboka en bokning (JP)
                                        Console.Clear();
                                        Console.WriteLine("Ange boknings-ID för att avboka:");
                                        if (int.TryParse(Console.ReadLine(), out int bookingID))
                                        {
                                            Lokal roomToCancel = Lokal.FindRoomByID(bookingID);
                                            if (roomToCancel != null)
                                            {
                                                roomToCancel.UnBook();
                                                Console.WriteLine("Bokningen har avbokats.");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Ingen bokning hittades med angivet ID.");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Ogiltigt boknings-ID.");
                                        }
                                        break;

                                    case 5:
                                        Lokal.UpdateRoom();
                                        //Uppdatera bokning
                                        break;
                                   
                                    default:
                                        Console.WriteLine("Ogiltigt val, försök igen.");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ogiltig inskrift");
                            }
                            break;
                        case 2: 
                            // Alternativ för att hantera lokaler
                            Console.WriteLine("Hantera lokaler:\n1:Visa alla salar och grupprum\n2:Skapa ny sal eller grupprum");
                            if ((int.TryParse(Console.ReadLine(), out secondMenuChoice)))
                            {
                                switch (secondMenuChoice)
                                {
                                    case 1:
                                        //Visa alla salar och grupprum
                                        Lokal.ListRooms();
                                        break;
                                    case 2:
                                        //Skapa ny sal eller grupprum
                                        Lokal.AddRoom();
                                        break;
                                    default:
                                        Console.WriteLine("Ogiltigt val, försök igen.");
                                        break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Ogiltig inskrift");
                            }
                            break;
                            case 3:
                            Lokal.SaveRoomsToFile();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Ogiltigt val, försök igen.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltig inskrift");
                }

            }
        }
    }
}
