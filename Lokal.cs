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
        public static void AddRoom(List<Lokal> allRooms)
        //Behövs det en return av listan för att spara nytt rum?
        {
            Console.WriteLine("Vad vill du lägga till?");
            Console.WriteLine("1.Sal\n2.Grupprum");
            String? selection = Console.ReadLine();
            if (selection == "1")
            {
                Console.WriteLine("Ange önskat nummer på salen");
                Byte roomNumber;
                bool roomNumberCheck = byte.TryParse(Console.ReadLine(), out roomNumber);
                if (roomNumberCheck == true && roomNumber > 0)
                {
                    if (!allRooms.Any(x => x.RoomNumber == roomNumber))
                    {


                        Console.WriteLine("Finns det en projektor i salen? \n1:Ja\n2:Nej");
                        if (int.TryParse(Console.ReadLine(), out int projectorOrNot))
                        {
                            Sal newSal;
                            switch (projectorOrNot)
                            {
                                case 1:
                                    newSal = new Sal("Sal", roomNumber, 40, true);
                                    allRooms.Add(newSal);
                                    Console.WriteLine($"Sal med nummer {roomNumber} har lagts till!");
                                    break;
                                case 2:
                                    newSal = new Sal("Sal", roomNumber, 40, false);
                                    allRooms.Add(newSal);
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
                        Console.WriteLine("Rumsnummret finns redan");
                    }
                }
                else
                {
                    Console.WriteLine("Ogiltlig inmatning");
                }
            }
            else if (selection == "2")
            {
                Console.WriteLine("Ange önskat nummer på Grupprummet");
                Byte roomNumber;
                bool roomNumberCheck = byte.TryParse(Console.ReadLine(), out roomNumber);
                if (roomNumberCheck == true && roomNumber > 0)
                {
                    if (!allRooms.Any(x => x.RoomNumber == roomNumber))
                    {
                        Console.WriteLine("Finns det eluttag i rummet? \n1:Ja\n2:Nej");
                        if (int.TryParse(Console.ReadLine(), out int projectorOrNot))
                        {
                            Grupprum newRoom;
                            switch (projectorOrNot)
                            {
                                case 1:
                                    newRoom = new Grupprum("Grupprum", roomNumber, 10, true);
                                    allRooms.Add(newRoom);
                                    Console.WriteLine($"Grupprum med nummer {roomNumber} har lagts till!");
                                    break;
                                case 2:
                                    newRoom = new Grupprum("Grupprum", roomNumber, 10, false);
                                    allRooms.Add(newRoom);
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
                        Console.WriteLine($"Rumsnummret finns redan: {roomNumber}");
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
    }
}
