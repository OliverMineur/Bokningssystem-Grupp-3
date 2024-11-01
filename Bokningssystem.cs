namespace Bokningssystem
{
    internal class Bokningssystem
    {
        private int ID;

        public static List<Bokningssystem> Bokningar = new List<Bokningssystem>();

        public Bokningssystem()
        {
            
        }

        public Bokningssystem HämtaBokningMedId(int ID)
        {
            return Bokningar.FirstOrDefault(b => b.ID == ID);
        }

        public bool TaBortBokningMedId(int id)
        {
            var bokning = HämtaBokningMedId(id);
            if (bokning != null)
            {
                return Bokningar.Remove(bokning);
            }
            return false;
        }
        static void Main(string[] args)
        {
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
                                        //Se alla bokningar
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
