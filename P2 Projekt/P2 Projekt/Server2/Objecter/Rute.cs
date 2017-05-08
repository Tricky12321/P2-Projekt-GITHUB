﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rute : MysqlObject
{
    public string RuteName;
    public int RuteID;
    public List<StoppestedMTid> AfPåRuteListMTid = new List<StoppestedMTid>();

    public Rute(string ruteName, int ruteID, params StoppestedMTid[] stoppested)
    {
        RuteName = ruteName;
        RuteID = ruteID;
        foreach (StoppestedMTid stop in stoppested)
        {
            AfPåRuteListMTid.Add(stop);
        }
    }

    public Rute() { }

    public override string ToString()
    {
        return RuteName;
    }

    public override void Start()
    {
        throw new NotImplementedException();
    }

    public override int GetID()
    {
        return this.RuteID;
    }

    public override string GetIDCollumName()
    {
        return "ID";
    }

    public override string GetTableName()
    {
        return "Ruter";
    }

    public override void GetUpdate()
    {
        Update(MysqlControls.SelectAllWhere(GetTableName(), WhereID()));
    }

    public override void Update(TableDecode TableContent)
    {
        RuteID = Convert.ToInt32(TableContent.RowData[0].Values[0]);                            // INT 32 ID
        RuteName = TableContent.RowData[0].Values[1];                                           // VARHCAR 50 
        string[] stoppesteder = TableContent.RowData[0].Values[2].Split(',');                   // 1,2,3,4,5,6,7,8,9,10
        string[] stoppestedertider = TableContent.RowData[0].Values[3].Split(',');
        // stoppesteds id'er:
        // 1,2,3,4,5,6,7,8,9,10
        foreach (string stop in stoppesteder)
        {
            Stoppested NewStop = new Stoppested(Convert.ToInt32(stop));
            // stoppesteds tider:
            // {11:30;12:30;13:30},{11:30;12:30;13:30},{11:30;12:30;13:30}
            foreach (string tidarr in stoppestedertider)
            {
                // {11:30;12:30;13:30}
                string times = tidarr.Replace("}", "").Replace("{","");
                // 11:30;12:30;13:30
                string[] tider = times.Split(';');
                List<AfPåTidCombi> AfTidList = new List<AfPåTidCombi>();
                foreach (var singleTid in tider)
                {
                    // 11:30
                    AfTidList.Add(new AfPåTidCombi(new Tidspunkt(singleTid)));
                }
                AfPåRuteListMTid.Add(new StoppestedMTid(NewStop, AfTidList));
            }

        }
    }

    public override string[] GetValues()
    {
        List<string> Output = new List<string>();
        Output.Add(RuteID.ToString());
        Output.Add(RuteName.ToString());
        StringBuilder StoppeStederID = new StringBuilder();
        StringBuilder StoppeStederTID = new StringBuilder();
        foreach (StoppestedMTid SingleStoppeSted in AfPåRuteListMTid)
        {
            List<string> StoppeStederTIDLOC = new List<string>();
            foreach (var item in SingleStoppeSted.AfPåTidComb)
            {
                StoppeStederTID.Append("{"+item.Tidspunkt.SinpleString()+"},");
            }
            StoppeStederID.Append(SingleStoppeSted.Stop.StoppestedID.ToString()+",");
        }
        Output.Add(StoppeStederID.ToString());
        Output.Add(StoppeStederTID.ToString());
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

    public override TableDecode GetThisFromDB(string WhereCondition)
    {
        return MysqlControls.SelectAllWhere(GetTableName(), WhereCondition);

    }

    public override string WhereID()
    {
        return $"`{GetIDCollumName()}`={GetID()}";
    }
}
