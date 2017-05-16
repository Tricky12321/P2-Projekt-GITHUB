using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum day { mandag = 1, tirsdag = 2, onsdag = 3, torsdag = 4, fredag = 5, lørdag = 6, søndag = 7 }

public class AfPåTidCombi
{
    public Tidspunkt Tidspunkt;
    public int ID;
    public int Afstigninger;
    public int Påstigninger;
    public day UgeDag;
    public Stoppested Stop;
    public Bus Bus;
    public int TotalPassagere;
    public int MaxCapa;
    public int Week;
    public int ForventetPassagere;
    /*
    public int ID;
    public Tidspunkt Tidspunkt;
    public int afstigninger;
    public int påstigninger;
    public int ForventetPassagere;
    */

    public override string ToString()
    {
        return Tidspunkt.ToString();
    }

}
