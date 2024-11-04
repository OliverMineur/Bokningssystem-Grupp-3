using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Bokningssystem
{
    internal interface IBookable
    {
        bool Boka(DateTime startTid, TimeSpan duration, string bokadAv);
        void Avboka();
    }
}