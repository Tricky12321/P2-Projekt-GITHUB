using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AfPåTidCombi
{
    public Tidspunkt Tidspunkt;
    public int afstigninger;
    public int påstigninger;
    public int ForventetPassagere;


    public AfPåTidCombi(Tidspunkt tidspunkt)
    {
        Tidspunkt = tidspunkt;
    }

    public override string ToString()
    {
        return Tidspunkt.ToString();
    }
}
