using System.Security.Cryptography.X509Certificates;

namespace Bokningssystem
{
    internal class Bokningssystem
    {  
                public List<Bokning> Bokningar { get; set; }

                public BokningsSystem()
                {
                    Bokningar = new List<Bokning>();
                }

                public bool TaBortBokning(Bokning bokning)
                {
                    return Bokningar.Remove(bokning);
                }
            }

        }

    


