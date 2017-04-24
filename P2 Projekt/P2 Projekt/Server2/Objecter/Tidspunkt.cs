using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tidspunkt
{
    public int hour;
    public int minute;

    public override string ToString()
    {
        return hour.ToString().PadLeft(2, '0') + " : " + minute.ToString().PadLeft(2, '0');
    }
}

