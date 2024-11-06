using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bokningssystem
{
    public abstract class Lokal : IBookable
    {

        public string RoomType { get; set; } // "Sal" eller "Grupprum"
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; protected set; }
        public DateTime BookingStartTime { get; protected set; }
        public TimeSpan BookingDuration { get; protected set; }
        public string ClientName { get; protected set; } = "";
        public int BookingID { get; protected set; }

        // Funktion för att boka ett rum
        public Lokal(String roomType, byte roomNumber, int numberOfChairs)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
            NumberOfChairs = numberOfChairs;
        }

        public static void BokningsMeny()
        {
            Console.WriteLine("Välj rumstyp att boka:");
            Console.WriteLine("1. Sal");
            Console.WriteLine("2. Grupprum");
            string roomTypeChoice = Console.ReadLine();

            // Hämta en lista över lediga rum av vald typ
            List<Lokal> ledigaRum = Bokningssystem.AllRooms.Where(rum => rum.RoomType == (roomTypeChoice == "1" ? "Sal" : "Grupprum") && !rum.IsBooked).ToList();

            if (!ledigaRum.Any())
            {
                Console.WriteLine("Inga lediga rum av denna typ.");
                return;
            }
            Console.WriteLine("Välj rum nummer att boka");
            foreach (var rum in ledigaRum)
            {
                Console.WriteLine($"Rum nummer: {rum.RoomNumber}, Platser: {rum.NumberOfChairs}");

            }
            byte roomNumber;
            if (!byte.TryParse(Console.ReadLine(), out roomNumber))
            {
                Console.WriteLine("Ogiltigt inmatning");
                return;
            }
            Lokal valtRum = (Lokal)ledigaRum.FirstOrDefault(rum => rum.RoomNumber == roomNumber).MemberwiseClone();
            if (valtRum == null)
            {
                Console.WriteLine("Rum inte hittat.");
                return;
            }
            Console.WriteLine("Ange kundens namn:");
            string clientNamn = Console.ReadLine();

            Console.WriteLine("Ange bokningsstarttid (ÅÅÅÅ-MM-DD HH:MM)");
            DateTime startTime;
            if (!DateTime.TryParse(Console.ReadLine(), out startTime))
            {
                Console.WriteLine("Ogiltigt tid");
                return;
            }
            Console.WriteLine("Ange varaktighet i timmar:");
            int durationHours;

            if (!int.TryParse(Console.ReadLine(), out durationHours))
            {
                Console.WriteLine("Ogiltigt varaktighet");
                return;
            }
            TimeSpan duration = new TimeSpan(durationHours, 0, 0);
            if (valtRum.Book(startTime, duration, clientNamn))
            {
                Bokningssystem.AllRooms.Add(valtRum);
                Console.WriteLine("Bokningen är genomförd");
            }
            else
            {
                Console.WriteLine("Kunde inte boka rummet");
            }
        }

        // Implementerar metoden Boka från IBookable
        public virtual bool Book(DateTime startTime, TimeSpan duration, string clientName)
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
            Bokningssystem.AllRooms.Add(this);// Lägg till i listan med bokade rum

            Console.WriteLine($"Bokning lyckades! Boknings-ID: {BookingID}");
            return true; // Bokning lyckades
        }

        public void BookFromSavedFile(DateTime startTime, TimeSpan duration, string clientName, int bookingID, bool isBooked)
        {
            BookingStartTime = startTime;
            BookingDuration = duration;
            ClientName = clientName;
            BookingID = bookingID;
            IsBooked = isBooked;
            Bokningssystem.AllRooms.Add(this);
        }

        // Funktion för att avboka ett rum
        public void UnBook()
        {
            IsBooked = false; // Återställ bokningsstatus
            Bokningssystem.AllRooms.Remove(this); // Ta bort från listan med bokade rum
            Console.WriteLine("Rummet har avbokats.");
        }

        // Metod för att generera ett unikt boknings-ID
        public static int GenerateBookingID()
        {
            int newBookingID;
            Random rnd = new Random();

            do
            {
                newBookingID = 1 * 1000 + rnd.Next(1, 1000);
            } while (Bokningssystem.AllRooms.Any(x => x.BookingID == newBookingID));

            return newBookingID;
        }
        public static Lokal FindRoomByID(int bookingID)
        {
            // Searches for the first room in the list with a matching booking ID (JP)
            Lokal room = Bokningssystem.AllRooms.FirstOrDefault(r => r.BookingID == bookingID);
            // If a room with the specified booking ID is found, display its details and return it
            if (room != null)
            {
                Console.WriteLine($"Rum hittades: {room.RoomType} med nummer {room.RoomNumber}");
                return room;
            }
            // If no room with the specified booking ID is found, display a message and return null
            else
            {
                Console.WriteLine("Ingen bokning hittades med det angivna ID:t");
                return null;
            }

        }


        public static void AddRoom()
        {
            Console.WriteLine("Vad vill du lägga till?");
            Console.WriteLine("1. Sal\n2. Grupprum");
            string? selection = Console.ReadLine();
            if (selection == "1")
            {
                Console.WriteLine("Ange önskat nummer på salen");
                if (byte.TryParse(Console.ReadLine(), out byte roomNumber) && roomNumber > 0)
                {
                    if (Bokningssystem.AllRooms.Any(x => x.RoomNumber == roomNumber))
                    {
                        Console.WriteLine("Rumsnumret finns redan.");
                        return;
                    }

                    Console.WriteLine("Finns det en projektor i salen? \n1: Ja\n2: Nej");
                    if (int.TryParse(Console.ReadLine(), out int projectorOrNot))
                    {
                        Sal newSal;
                        switch (projectorOrNot)
                        {
                            case 1:
                                newSal = new Sal("Sal", roomNumber, 40, true);
                                Bokningssystem.AllRooms.Add(newSal);
                                Console.WriteLine($"Sal med nummer {roomNumber} har lagts till!");
                                break;
                            case 2:
                                newSal = new Sal("Sal", roomNumber, 40, false);
                                Bokningssystem.AllRooms.Add(newSal);
                                Console.WriteLine($"Sal med nummer {roomNumber} har lagts till!");
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
                else
                {
                    Console.WriteLine("Ogiltlig inmatning");
                }
            }
            else if (selection == "2")
            {
                Console.WriteLine("Ange önskat nummer på grupprummet");
                if (byte.TryParse(Console.ReadLine(), out byte roomNumber) && roomNumber > 0)
                {
                    if (Bokningssystem.AllRooms.Any(x => x.RoomNumber == roomNumber))
                    {
                        Console.WriteLine("Rumsnumret finns redan.");
                        return;
                    }

                    Console.WriteLine("Finns det eluttag i rummet? \n1: Ja\n2: Nej");
                    if (int.TryParse(Console.ReadLine(), out int socketOrNot))
                    {
                        Grupprum newRoom;
                        switch (socketOrNot)
                        {
                            case 1:
                                newRoom = new Grupprum("Grupprum", roomNumber, 10, true);
                                Bokningssystem.AllRooms.Add(newRoom);
                                Console.WriteLine($"Grupprum med nummer {roomNumber} har lagts till!");
                                break;
                            case 2:
                                newRoom = new Grupprum("Grupprum", roomNumber, 10, false);
                                Bokningssystem.AllRooms.Add(newRoom);
                                Console.WriteLine($"Grupprum med nummer {roomNumber} har lagts till!");
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
                else
                {
                    Console.WriteLine("Ogiltlig inmatning");
                }
            }
            else
            {
                Console.WriteLine("Ogiltligt val");
            }
        }

        public static void ListRooms()
        {
            Console.WriteLine("Lista över alla salar och deras egenskaper:");
            foreach (var room in Bokningssystem.AllRooms)
            {
                Console.WriteLine($"Rumstyp: {room.RoomType}, Rumsnummer: {room.RoomNumber}, Antal stolar: {room.NumberOfChairs}, Bokad: {room.IsBooked}");
            }

        }
        public static void SaveRoomsToFile()
        {
            using (StreamWriter writer = new StreamWriter("BookingsAndRooms.txt", append: false))
            {
                foreach (Lokal room in Bokningssystem.AllRooms)
                {
                    if (room is Sal sal)
                    {
                        foreach (PropertyInfo roomSpecs in typeof(Sal).GetProperties())
                        {
                            writer.WriteLine(roomSpecs.GetValue(room, null));
                        }
                    }
                    if (room is Grupprum grupprum)
                    {
                        foreach (PropertyInfo roomSpecs in typeof(Grupprum).GetProperties())
                        {
                            writer.WriteLine(roomSpecs.GetValue(room, null));
                        }
                    }
                    writer.WriteLine();
                }
            }
        }

        public static void LoadRoomsFromFile()
        {
            if (File.Exists("BookingsAndRooms.txt"))
            {
                using (StreamReader reader = new StreamReader("BookingsAndRooms.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        bool socketOrProjector = bool.Parse(reader.ReadLine());
                        string roomType = reader.ReadLine();
                        byte roomNumber = byte.Parse(reader.ReadLine());
                        int numberOfChairs = int.Parse(reader.ReadLine());
                        bool isBooked = bool.Parse(reader.ReadLine());
                        DateTime startTime = DateTime.Parse(reader.ReadLine());
                        TimeSpan duration = TimeSpan.Parse(reader.ReadLine());
                        string clientName = reader.ReadLine();
                        int bookingID = int.Parse(reader.ReadLine());
                        reader.ReadLine();

                        if (roomType == "Sal")
                        {
                            Lokal newObject = new Sal(roomType, roomNumber, numberOfChairs, socketOrProjector);
                            newObject.BookFromSavedFile(startTime, duration, clientName, bookingID, isBooked);
                        }
                        else if (roomType == "Grupprum")
                        {
                            Lokal newObject = new Grupprum(roomType, roomNumber, numberOfChairs, socketOrProjector);
                            newObject.BookFromSavedFile(startTime, duration, clientName, bookingID, isBooked);
                        }
                    }
                }
            }
        }
    }
}

