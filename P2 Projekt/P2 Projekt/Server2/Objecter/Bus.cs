using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bus : MysqlObject
{
    public List<StoppestedMTid> busPassagerDataListe = new List<StoppestedMTid>();

    public string busID;
    public GPS placering;
    public int passengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute Rute;
    public int besøgteStop = 0;

}
