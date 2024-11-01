using System.Security.Cryptography.X509Certificates;

namespace Bokningssystem
{
    public class Bokningssystem
    {
        private int ID;

        public List<Bokningssystem> Bokningar { get; set; }

        public Bokningssystem()
        {
            Bokningar = new List<Bokningssystem>();
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
    }
}


