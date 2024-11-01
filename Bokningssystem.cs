namespace Bokningssystem
{
    internal class Bokningssystem
    {
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

                                }
                            }
                            break;
                        case 2:
                            Console.WriteLine("Hantera lokaler:\n1:Visa alla salar\n2:Skapa ny sal");
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
                                }
                            }
                            break;
                    }
                }
            }
        }
    }
}
