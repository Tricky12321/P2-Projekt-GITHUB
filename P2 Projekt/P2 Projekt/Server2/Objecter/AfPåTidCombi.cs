using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class AfPåTidCombi : MysqlObject
{
    public Tidspunkt Tidspunkt;
    public int afstigninger;
    public int påstigninger;

    public override string ToString()
    {
        return Tidspunkt.ToString();
    }
}
