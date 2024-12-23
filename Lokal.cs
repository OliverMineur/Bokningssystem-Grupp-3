﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace Bokningssystem
{
    // Representerar en abstrakt lokal som kan bokas, t.ex. ett grupprum eller en sal
    public abstract class Lokal : IBookable
    {

        public string RoomType { get; set; } // "Sal" eller "Grupprum"
        public byte RoomNumber { get; set; }
        public int NumberOfChairs { get; set; }
        public bool IsBooked { get; protected set; }  // Anger om rummet är bokat eller ej
        public DateTime BookingStartTime { get; protected set; }
        public TimeSpan BookingDuration { get; protected set; }
        public string ClientName { get; protected set; } = ""; 
        public int BookingID { get; protected set; } 

        public Lokal(String roomType, byte roomNumber, int numberOfChairs)
        {
            RoomType = roomType;
            RoomNumber = roomNumber;
            NumberOfChairs = numberOfChairs;
        }

        // Visar en meny för att boka ett rum (JP)
        // + Emilia + Cecilia + Oliver
        public static bool BokningsMeny()
        {
            Console.Clear();
            Console.WriteLine("Välj rumstyp att boka:");
            Console.WriteLine("1. Sal");
            Console.WriteLine("2. Grupprum");
            string roomTypeChoice = Console.ReadLine();
            Console.Clear();
            // Hämta en lista över lediga rum av vald typ
            List<Lokal> ledigaRum = Bokningssystem.AllRooms.Where(rum => rum.RoomType == (roomTypeChoice == "1" ? "Sal" : "Fel") || rum.RoomType == (roomTypeChoice == "2" ? "Grupprum" : "Fel") && !rum.IsBooked).ToList();
            if (roomTypeChoice != "1" && roomTypeChoice != "2")
            {
                Console.WriteLine("Felaktig inmatning");
                return false;
            }
            if (!ledigaRum.Any())
            {
                Console.WriteLine("Inga lediga rum av denna typ.");
                return false;
            }
            Console.WriteLine("Välj rum nummer att boka");
            foreach (var rum in ledigaRum)
            {
                Console.WriteLine($"{rum.RoomType} nummer: {rum.RoomNumber}, Platser: {rum.NumberOfChairs}");

            }
            // Läsa in rumsnumret för bokning
            if (!byte.TryParse(Console.ReadLine(), out byte roomNumber))
            {
                Console.WriteLine("Ogiltigt inmatning");
                return false;
            }
            Console.Clear();
            if (!ledigaRum.Any(x => x.RoomNumber == roomNumber))
            //Om angivet rumsnummer inte finns i listan 
            {
                Console.WriteLine("Nummret du skrev in hittades inte");
                return false;
            }
            // Klonar valt rum för att boka det
            Lokal valtRum = (Lokal)ledigaRum.FirstOrDefault(rum => rum.RoomNumber == roomNumber).MemberwiseClone();
            if (valtRum == null)
            {
                Console.WriteLine("Rum inte hittat.");
                return false;
            }
            Console.Clear();
            // Få klientnamn för bokningen
            Console.WriteLine("Ange kundens namn:");
            string clientNamn = Console.ReadLine();

            Console.Clear();
            // Ange starttid för bokningen
            Console.WriteLine("Ange bokningsstarttid (ÅÅÅÅ-MM-DD HH:MM)");
            DateTime startTime;
            if (!DateTime.TryParse(Console.ReadLine(), out startTime))
            {
                Console.WriteLine("Ogiltigt tid");
                return false;
            }
            Console.Clear();
            // Ange varaktighet för bokningen
            Console.WriteLine("Ange varaktighet i timmar:");
            int durationHours;

            if (!int.TryParse(Console.ReadLine(), out durationHours))
            {
                Console.WriteLine("Ogiltigt varaktighet");
                return false;
            }
            // Bokar rummet om tillgängligt
            TimeSpan duration = new TimeSpan(durationHours, 0, 0);
            if (valtRum.Book(startTime, duration, clientNamn))
            {
                Bokningssystem.AllRooms.Add(valtRum);
                Console.Clear();
                Console.WriteLine("Bokningen är genomförd");
                return true;
            }
            else
            {
                Console.WriteLine("Kunde inte boka rummet");
                return false;
            }
        }

        // Implementerar metoden Boka från IBookable (Emilia)
        public virtual bool Book(DateTime startTime, TimeSpan duration, string clientName)
        {
            if (IsBooked)
            {
                Console.WriteLine("Rummet är redan bokat.");
                return false; // Rummet är redan bokat
            }

            // Kontrollera om antalet stolar överstiger kapacitetsgränsen beroende på rumstyp
            if ((RoomType == "Sal" && NumberOfChairs > 40) ||
                (RoomType == "Grupprum" && NumberOfChairs > 10))
            {
                Console.WriteLine("Rummet överstiger kapacitetsgränsen.");
                return false; // Bokning misslyckades pga kapacitet
            }

            // Genererar ett unikt boknings-ID och uppdaterar bokningsinformation
            BookingID = GenerateBookingID();
            ClientName = clientName;
            BookingStartTime = startTime;
            BookingDuration = duration;
            IsBooked = true; // Uppdatera bokningsstatus
            Bokningssystem.AllRooms.Add(this);// Lägg till i listan med bokade rum

            Console.WriteLine($"Bokning lyckades! Boknings-ID: {BookingID}");
            return true; // Bokning lyckades
        }

        // Funktion för att avboka ett rum (Emilia)
        // + JP
        public void UnBook()
        {
            IsBooked = false; // Återställ bokningsstatus
            Bokningssystem.AllRooms.Remove(this); // Ta bort från listan med bokade rum
            Console.Clear();
            Console.WriteLine("Rummet har avbokats.");
        }

        // Genererar ett unikt boknings-ID som inte redan används (JP)
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
        // Hittar ett rum baserat på ett boknings-ID (JP)
        public static Lokal FindRoomByID(int bookingID)
        {
            Console.Clear();
            // Söker efter det första rummet i listan med ett matchande boknings-ID (JP)
            Lokal room = Bokningssystem.AllRooms.FirstOrDefault(r => r.BookingID == bookingID);
            // Om ett rum med angivet boknings-ID hittas, visa dess uppgifter och returnera det
            if (room != null)
            {
                Console.WriteLine($"Rum hittades: {room.RoomType} med nummer {room.RoomNumber}");
                return room;
            }
            // Om inget rum med angivet boknings-ID hittas, visa ett meddelande och returnera null
            else
            {
                Console.WriteLine("Ingen bokning hittades med det angivna ID:t");
                return null;
            }
        }

        // Lägger till ett nytt rum i systemet (Cecilia)
        // + Oliver
        public static void AddRoom()
        {
            Console.Clear();
            Console.WriteLine("Vad vill du lägga till?");
            Console.WriteLine("1. Sal\n2. Grupprum");
            string? selection = Console.ReadLine();
            Console.Clear();
            if (selection == "1")
            {
                Console.WriteLine("Ange önskat nummer på salen");
                if (byte.TryParse(Console.ReadLine(), out byte roomNumber) && roomNumber > 0)
                //Om parse lyckas, skickar tillbaka roomNumber
                {
                    Console.Clear();
                    if (Bokningssystem.AllRooms.Any(x => x.RoomNumber == roomNumber))
                    //Kontrollerar om angivna rumsnummret redan finns i listan
                    {
                        Console.WriteLine("Rumsnumret finns redan.");
                        return;
                    }
                    Console.Clear();
                    Console.WriteLine("Finns det en projektor i salen? \n1: Ja\n2: Nej");
                    if (int.TryParse(Console.ReadLine(), out int projectorOrNot))
                    {
                        Sal newSal;
                        Console.Clear();
                        switch (projectorOrNot)
                        {
                            case 1:
                                // Logik för att lägga till en ny sal
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
                Console.Clear();
                Console.WriteLine("Ange önskat nummer på grupprummet");
                if (byte.TryParse(Console.ReadLine(), out byte roomNumber) && roomNumber > 0)
                {
                    if (Bokningssystem.AllRooms.Any(x => x.RoomNumber == roomNumber))
                    {
                        Console.WriteLine("Rumsnumret finns redan.");
                        return;
                    }
                    Console.Clear();
                    Console.WriteLine("Finns det eluttag i rummet? \n1: Ja\n2: Nej");
                    if (int.TryParse(Console.ReadLine(), out int socketOrNot))
                    {
                        Console.Clear();
                        Grupprum newRoom;
                        switch (socketOrNot)
                        {
                            case 1:
                                // Logik för att lägga till ett nytt grupprum
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
        // Listar alla rum i systemet och deras egenskaper
        public static void ListRooms() //(Emilia)
            // + Cecilia
        {
            Console.Clear();
            if (Bokningssystem.AllRooms.Count == 0)
            {
                Console.WriteLine("Finns inga skapade rum.");
                return;
            }

            // Skriver ut en lista över alla salar och deras egenskaper
            Console.WriteLine("Lista över alla salar och deras egenskaper:");
            foreach (var room in Bokningssystem.AllRooms)
            {
                if (!room.IsBooked)
                {
                    if (room is Sal sal)
                    {
                        Console.WriteLine($"Rumstyp: {sal.RoomType}, Rumsnummer: {sal.RoomNumber}, Antal stolar: {sal.NumberOfChairs}, Projektor: {(sal.Projector ? "Ja" : "Nej")}");
                    }
                    else if (room is Grupprum grupprum)
                    {
                        Console.WriteLine($"Rumstyp: {grupprum.RoomType}, Rumsnummer: {grupprum.RoomNumber}, Antal stolar: {grupprum.NumberOfChairs}, Eluttag: {(grupprum.Socket ? "Ja" : "Nej")}");
                    }
                }
            }
        }
        //Sparar data från AllRooms lokalt på hårdisken (Oliver)
        public static void SaveRoomsToFile()
        {
            List<Sal> sals = Bokningssystem.AllRooms.Where(x => x.RoomType == "Sal").Cast<Sal>().ToList();
            List<Grupprum> grupprums = Bokningssystem.AllRooms.Where(x => x.RoomType == "Grupprum").Cast<Grupprum>().ToList();
            File.WriteAllText("Sal.Json", JsonSerializer.Serialize(sals));
            File.WriteAllText("Grupprum.Json", JsonSerializer.Serialize(grupprums));
        }
        //Hämtar sparad data från hårddisken (Oliver)
        public static void LoadRoomsFromFile()
        {
            if (File.Exists("Sal.Json") && File.Exists("Grupprum.Json"))
            {
                Bokningssystem.AllRooms.AddRange(JsonSerializer.Deserialize<List<Sal>>(File.ReadAllText("Sal.Json")));
                Bokningssystem.AllRooms.AddRange(JsonSerializer.Deserialize<List<Grupprum>>(File.ReadAllText("Grupprum.Json")));
            }
        }
        //Metod för att uppdatera rum (Cecilia)
        public static void UpdateRoom()
        {
            Console.WriteLine("Ange boknings ID:");

            if (int.TryParse(Console.ReadLine(), out int userSearchId))
            {
                List<Lokal> allRoomsCopy = Bokningssystem.AllRooms.ToList();
                //Kopia av AllRooms för att i foreach loop lokalisera korrekt bookning och ta bort ur oginal listan
                foreach (Lokal booking in allRoomsCopy)
                {
                    if (booking.BookingID == userSearchId)
                    {
                        Console.WriteLine("Vad vill du göra?");
                        Console.WriteLine("1.Byta sal\n2.Ändra tid");
                        if (int.TryParse(Console.ReadLine(), out int userChoice))
                        {
                            switch (userChoice)
                            {
                                case 1:
                                    if (BokningsMeny())
                                    //Om vi får tillbaka true från BokningsMeny, en ny bokning har skapats
                                    {
                                        Bokningssystem.AllRooms.Remove(booking);
                                        //Tar bort aktuell bokning ur AllRooms
                                        return;
                                    }
                                    break;

                                case 2: //Ändra tid
                                    Console.WriteLine($"Nuvarande bokning:\nRumsnummer: {booking.RoomNumber}\n" +
                                        $"Starttid: {booking.BookingStartTime}\n" +
                                        $"Längd på bokning: {booking.BookingDuration}");

                                    Console.WriteLine("Ange ny bokningsstarttid (ÅÅÅÅ-MM-DD:MM):");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime startTime))
                                    {
                                        booking.BookingStartTime = startTime;
                                        //Väljer ny starttid 
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ogiltigt tid");
                                        return;
                                    }
                                    Console.WriteLine("Ange varaktighet i timmar:");
                                    if (int.TryParse(Console.ReadLine(), out int durationHours))
                                    {
                                        TimeSpan duration = new TimeSpan(durationHours, 0, 0);
                                        //Skapar en ny timespan med användarens val av timmar
                                        booking.BookingDuration = duration;
                                        //Väljer ny varaktighet
                                    }
                                    else
                                    {
                                        Console.WriteLine("Ogiltigt varaktighet");
                                        return;
                                    }
                                    Console.WriteLine("Ändring klar!");
                                    Console.WriteLine($"Uppdaterad bokning:\nRumsnummer: {booking.RoomNumber}\n" +
                                        $"Starttid: {booking.BookingStartTime}\n" +
                                        $"Längd på bokning: {booking.BookingDuration}");
                                    return;
                                default:
                                    Console.WriteLine("Ogiltigt menyval");
                                    return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Ogiltligt menyval");
                            return;
                        }
                    }
                }
                Console.WriteLine("Bokning hittades inte");
            }
            else
            {
                Console.WriteLine("Ogiltlig inmatning av boknings ID");
            }
        }
    }
}

