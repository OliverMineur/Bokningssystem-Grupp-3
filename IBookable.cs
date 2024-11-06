using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Bokningssystem
{
    // Interface för bokningsbara objekt (ET)
    internal interface IBookable
    {
        // Metod för att boka ett objekt
        bool Book(DateTime startTime, TimeSpan duration, string clientName);
        // Metod för att avboka
        void UnBook();
    }
}