﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Bokningssystem
{
    // Interface för bokningsbara objekt (Emilia)
    internal interface IBookable
    {
        // Metod för att boka ett rum. Tar starttid, varaktighet och klientnamn som parametrar
        // Returnerar ett booleskt värde som indikerar om bokningen lyckades
        bool Book(DateTime startTime, TimeSpan duration, string clientName);
    }
}