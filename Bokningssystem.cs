﻿using System.ComponentModel.Design;

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

        public void ShowAllBookings()
        {
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
        }
        public void FilterBookings()
        {
            try
            {
                Console.WriteLine("Hur vill du filtrera listan?\n1:Sortera efter rumstyp\n2:Sortera efter datum\n3:Sortera efter rum nummer");
                if (int.TryParse(Console.ReadLine(), out int menuChoice))
                {

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        static void Main(string[] args)
        {
            AllRooms.Add(new Sal("Sal", 12, 40, false));
            AllRooms.Add(new Grupprum("Grupprum", 14, 20, true));
            AllRooms.Add(new Sal("Sal", 12, 40, false));
            AllRooms.Add(new Grupprum("Grupprum", 14, 20, true));
            AllRooms.Add(new Grupprum("Grupprum", 14, 20, true));
            AllRooms.Add(new Sal("Sal", 12, 40, false));
            AllRooms[1].Booked();


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
                if(int.TryParse(Console.ReadLine(), out int menuChoice))
                {
                    switch (menuChoice)
                    {
                        case 1:
                            Console.WriteLine("Hantera bokningar:\n1:Skapa bokning\n2:Se alla bokningar\n3:Ta bort bokning\n4:Uppdatera bokning");
                            if (int.TryParse(Console.ReadLine(), out int secondMenuChoice))
                            {
                                switch (secondMenuChoice)
                                {
                                    case 1:
                                        //Skapa bokning
                                        break;
                                    case 2:
                                        ShowAllBookings();
                                        break;
                                    case 3:
                                        //Ta bort alla bokningar
                                        break;
                                    case 4:
                                        //Uppdatera bokning
                                        break;
                                    default:
                                        Console.WriteLine("Inget valdes, försök igen.");
                                        break;

                                }
                            }
                            else
                            {
                                throw new InvalidDataException();
                            }
                            break;
                        case 2:
                            Console.WriteLine("Hantera lokaler:\n1:Visa alla salar och grupprum\n2:Skapa ny sal eller grupprum");
                            if ((int.TryParse(Console.ReadLine(),out secondMenuChoice)))
                            {
                                switch (secondMenuChoice)
                                {
                                    case 1:
                                        //Visa alla salar
                                        break;
                                    case 2:
                                        //Skapa ny sal
                                        break;
                                    default:
                                        Console.WriteLine("Inget valdes, försök igen.");
                                        break;
                                }
                            }
                            else
                            {
                                throw new InvalidDataException();
                            }
                            break;
                        default:
                            Console.WriteLine("Inget valdes, försök igen.");
                            break;
                    }
                }
                else
                {
                    throw new InvalidDataException();
                }
            }
        }
    }
}
