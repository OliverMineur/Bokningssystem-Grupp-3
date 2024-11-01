using System;
using System.Timers;

namespace Bokningssystem
{
    internal class Lokal
    {
        public string RoomType { get; set; } = ""; // "Sal" or "Grupprum"
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; private set; } = false; // Only modified by class methods

        public bool Booked()
        {
            if (IsBooked)
            {
                return false; // Room is already booked
            }

            // Check capacity based on room type
            if ((RoomType == "Sal" && NumberOfChairs > 40) ||
                (RoomType == "Grupprum" && NumberOfChairs > 10))
            {
                return false; // Booking fails due to capacity limit
            }

            IsBooked = true; // Update booking status
            return true; // Booking successful
        }

        public void Unbook()
        {
            IsBooked = false; // Reset booking status
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
