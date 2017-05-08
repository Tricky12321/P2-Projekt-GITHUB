using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StoppestedMTid
{
    public Stoppested Stop;
    public List<AfPåTidCombi> AfPåTidComb;

    public StoppestedMTid(Stoppested stop, params AfPåTidCombi[] afPåTidComb)
    {
        Stop = stop;
        AfPåTidComb = afPåTidComb.ToList();
    }

    public StoppestedMTid(StoppestedMTid stop, params AfPåTidCombi[] afPåTidComb)
    {
        Stop = stop.Stop;
        AfPåTidComb = afPåTidComb.ToList();
    }

    public StoppestedMTid()
    {

    }

    
}
