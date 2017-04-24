using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Bus : MysqlObject
{
    public List<StoppestedMTid> busPassagerDataListe = new List<StoppestedMTid>();

    public int busID;
    public string busName;
    public GPS busLok;
    public int passengersTotal;
    public int CapacitySitting;
    public int CapacityStanding;
    public Rute rute;
    public int besøgteStop = 0;

    public override string ToString()
    {
        return busName;
    }

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override int GetID()
    {
        return this.busID;
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "Busser";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
    }

    public override void Update(TableDecode TableContent)
    {
        busID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        busLok = new GPS();
        busName = Convert.ToString(TableContent.RowData[0].Values[1]);
        busLok.xCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[2]);    // DOUBLE
        busLok.yCoordinate = Convert.ToDouble(TableContent.RowData[0].Values[3]);    // DOUBLE
        passengersTotal = Convert.ToInt32(TableContent.RowData[0].Values[4]);
        CapacitySitting = Convert.ToInt32(TableContent.RowData[0].Values[5]);
        CapacityStanding = Convert.ToInt32(TableContent.RowData[0].Values[6]);
        besøgteStop = Convert.ToInt32(TableContent.RowData[0].Values[7]);
        rute = new Rute();                                                              //Ruten her mangler at være korrekt
        rute = Convert.ToInt32(TableContent.RowData[0].Values[7]);                      //se også lige om den er korrekt i GetValues

    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(busID.ToString());
        Output.Add(busName.ToString());
        Output.Add(busLok.xCoordinate.ToString());
        Output.Add(busLok.yCoordinate.ToString());
        Output.Add(passengersTotal.ToString());
        Output.Add(CapacitySitting.ToString());
        Output.Add(CapacityStanding.ToString());
        Output.Add(besøgteStop.ToString());
        Output.Add(rute.ToString());

        return Output.ToArray();
    }

    public override string[] GetValuesDB()
    {
        return GetThisFromDB().RowData[0].Values.ToArray();
    }

    public override TableDecode GetThisFromDB()
    {
        return MysqlControls.SelectAllWhere(GetTableName(), WhereID());
    }

    public override string WhereID()
    {
        return $"`{GetIDCollumName()}`={GetID()}";
    }
}
