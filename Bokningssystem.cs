using System.ComponentModel.Design;
using System.Net.Sockets;

namespace Bokningssystem
{
    internal class Bokningssystem
    {
        public static List<Lokal> AllRooms = new List<Lokal>();

        public Bokningssystem()
        {

        }

        //public Lokal GetBookingWithId(int ID)
        //{
        //    return AllRooms.FirstOrDefault(b => b.ID == ID);
        //}

        //public bool RemoveBookingWithId(int id)
        //{
        //    var bokning = GetBookingWithId(id);
        //    if (bokning != null)
        //    {
        //        return AllRooms.Remove(bokning);
        //    }
        //    return false;
        //}

        public static void ShowAllBookings(int whatToSort, int ascendingOrDecending)
        {
            List<Lokal> orderedList = null;
            try
            {
                switch ((whatToSort, ascendingOrDecending))
                {
                    case (0, 0):
                        foreach (Lokal room in AllRooms)
                        {
                            if (room.IsBooked == true)
                            {
                                Console.WriteLine("" + room.RoomNumber + " is booked by " +
                                                        room.ClientName + " from " +
                                                        room.BookingStartTime.ToString("M") + " " +
                                                        room.BookingStartTime.ToString("HH:mm") + " to " +
                                                        (room.BookingStartTime + room.BookingDuration).ToString("HH:mm"));
                            }
                        }
                        break;
                    case (1, 1):
                        orderedList = AllRooms.OrderBy(x => x.RoomType).ToList();
                        break;
                    case (2, 1):
                        orderedList = AllRooms.OrderBy(x => x.BookingStartTime).ToList();
                        break;
                    case (3, 1):
                        orderedList = AllRooms.OrderBy(x => x.RoomNumber).ToList();
                        break;
                    case (1, 2):
                        orderedList = AllRooms.OrderByDescending(x => x.RoomType).ToList();
                        break;
                    case (2, 2):
                        orderedList = AllRooms.OrderByDescending(x => x.BookingStartTime).ToList();
                        break;
                    case (3, 2):
                        orderedList = AllRooms.OrderByDescending(x => x.RoomNumber).ToList();
                        break;
                    case (4, 1):
                        Console.WriteLine("Vilket år vill du söka på:\nFormat exempel \"1998\"");
                        int searchForyear = int.Parse(Console.ReadLine());
                        foreach (Lokal room in AllRooms)
                        {
                            if (searchForyear == room.BookingStartTime.Year)
                            {
                                Console.WriteLine("" + room.RoomNumber + " is booked by " +
                                                        room.ClientName + " from " +
                                                        room.BookingStartTime.ToString("M") + " " +
                                                        room.BookingStartTime.ToString("HH:mm") + " to " +
                                                        (room.BookingStartTime + room.BookingDuration).ToString("HH:mm"));
                            }
                        }
                        break;
                    default:
                        Console.WriteLine("Inget av alternativen valdes.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (orderedList != null)
                {
                    foreach (Lokal room in orderedList)
                    {
                        if (room.IsBooked == true)
                        {
                            Console.WriteLine("" + room.RoomNumber + " is booked by " +
                                                room.ClientName + " from " +
                                                room.BookingStartTime.ToString("M") + " " +
                                                room.BookingStartTime.ToString("HH:mm") + " to " +
                                                (room.BookingStartTime + room.BookingDuration).ToString("HH:mm"));
                        }
                    }
                }
            }
        }
        public static void FilterBookings()
        {
            try
            {
                Console.WriteLine("Hur vill du filtrera listan?\n1:Sortera efter rumstyp\n2:Sortera efter datum\n3:Sortera efter rum nummer\n4:Visa bokningar på specifika år");
                int firstChoice = int.Parse(Console.ReadLine());
                Console.WriteLine("Visa i:\n1:Stigande ordning\n2:Fallande ordning");
                int secondChoice = int.Parse(Console.ReadLine());
                ShowAllBookings(firstChoice, secondChoice);
            }
            catch (FormatException)
            {
                Console.WriteLine("Ogiltig inskrift, ange bara ett nummer.");
            }
        }

        static void Main(string[] args)
        {



            //foreach (Lokal item in AllRooms)
            //{
            //    if(item is Sal a)
            //    {
            //        Console.WriteLine(a.RoomType + a.Socket + a.NumberOfChairs);
            //    }
            //    else if (item is Grupprum b)
            //    {
            //        Console.WriteLine(b.RoomType + b.RoomNumber);
            //    }
            //}

            while (true)
            {

                Console.WriteLine("Bokningssystem:\n1:Hantera bokningar\n2:Hantera lokaler");
                if (int.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Console.WriteLine("Hantera bokningar:\n1:Skapa bokning\n2:Se alla bokningar\n3:Filtrera alla bokningar\n4:Ta bort bokning\n5:Uppdatera bokning\n6:Avsluta");
                            if (int.TryParse(Console.ReadLine(), out int secondMenuChoice))
                            {
                                switch (secondMenuChoice)
                                {
                                    case 1:
                                        //Skapa bokning
                                        Lokal.BokningsMeny();
                                        break;
                                    case 2:
                                        ShowAllBookings(0, 0);
                                        break;
                                    case 3:
                                        FilterBookings();
                                        break;
                                    case 4:
                                        //Ta bort alla bokningar
                                        break;
                                    case 5:
                                        //Uppdatera bokning
                                        break;
                                    case 6:
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
                            break;
                        case 2:
                            Console.WriteLine("Hantera lokaler:\n1:Visa alla salar och grupprum\n2:Skapa ny sal eller grupprum");
                            if ((int.TryParse(Console.ReadLine(), out secondMenuChoice)))
                            {
                                switch (secondMenuChoice)
                                {
                                    case 1:
                                        //Visa alla salar
                                        break;
                                    case 2:
                                        //Skapa ny sal
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
