using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritme
{
    class DataAfPåstigning
    {
        public DateTime tid;
        public int afstigninger;
        public int påstigninger;
    }

    class StoppestedDataAfPåstigning : DataAfPåstigning
    {
        public Bus bus;
    }
}
