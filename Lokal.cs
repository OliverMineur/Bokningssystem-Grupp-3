using System;

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
                    if (allRooms.FindIndex(x => x.RoomNumber == roomNumber) == -1)
                    {
                        Console.WriteLine("Finns det en projektor i salen? Svara ja/nej");
                        String? projectorOrNot = Console.ReadLine();
                        if (String.IsNullOrEmpty(projectorOrNot))
                        {
                            while (String.IsNullOrEmpty(projectorOrNot))
                            {
                                Console.WriteLine("Svara ja/nej");
                                Console.WriteLine("Finns det en projektor i salen?");
                                projectorOrNot = Console.ReadLine();
                            }
                        }
                        else if (projectorOrNot.ToLower() == "Ja".ToLower())
                        {
                            Sal newSal = new Sal("Sal", roomNumber, 40, true);
                            allRooms.Add(newSal);
                            Console.WriteLine($"Sal med nummer {roomNumber} har lagts till!");
                        }
                        else if (projectorOrNot.ToLower() == "Nej".ToLower())
                        {
                            Sal newSal = new Sal("Sal", roomNumber, 40, false);
                            allRooms.Add(newSal);
                            Console.WriteLine($"Sal med nummer {roomNumber} har lagts till!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Rumsnummret finns redan");
                    }
                }
                else if (roomNumberCheck == false)
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
                    if (allRooms.FindIndex(x => x.RoomNumber == roomNumber) == -1)
                    {
                        Console.WriteLine("Finns det eluttag i rummet? Svara ja/nej");
                        String? socketOrNot = Console.ReadLine();
                        if (String.IsNullOrEmpty(socketOrNot))
                        {
                            while (String.IsNullOrEmpty(socketOrNot))
                            {
                                Console.WriteLine("Svara ja/nej");
                                Console.WriteLine("Finns det eluttag i salen?");
                                socketOrNot = Console.ReadLine();
                            }
                        }
                        else if (socketOrNot.ToLower() == "Ja".ToLower())
                        {
                            Grupprum newRoom = new Grupprum("Grupprum", roomNumber, 10, true);
                            allRooms.Add(newRoom);
                            Console.WriteLine($"Grupprum med nummer {roomNumber} har lagts till!");
                        }
                        else if (socketOrNot.ToLower() == "Nej".ToLower())
                        {
                            Grupprum newRoom = new Grupprum("Grupprum", roomNumber, 10, false);
                            allRooms.Add(newRoom);
                            Console.WriteLine($"Grupprum med nummer {roomNumber} har lagts till!");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Rumsnummret finns redan: {roomNumber}");
                    }
                }
                else if (roomNumberCheck == false)
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
