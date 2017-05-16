using System.Collections.Generic;
using System.Linq;

public class StoppestedMTid
{
    public Stoppested Stop;
    public List<AfPåTidCombi> AfPåTidComb;

    public StoppestedMTid(Stoppested stop)
    {
        Stop = stop;
    }

    public StoppestedMTid(Stoppested stop, params AfPåTidCombi[] afPåTidComb)
    {
        Stop = stop;
        AfPåTidComb = afPåTidComb.ToList();
    }

    public StoppestedMTid(Stoppested stop, List<AfPåTidCombi> afPåTidList)
    {
        Stop = stop;
        AfPåTidComb = afPåTidList;
    }


    public StoppestedMTid()
    {

    }
}

