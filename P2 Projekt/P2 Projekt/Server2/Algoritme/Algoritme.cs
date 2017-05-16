using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

public static class Algoritme
{
    private static Stoppested GetCurrentStop(Bus EnEllerAndenBus)
    {
        Stoppested SidsteStop = new Stoppested();
        Row SidsteLinje = MysqlControls.GetLastLineFrom(new AfPåTidCombi().GetTableName(), $"WHERE busID = {EnEllerAndenBus.BusID}");
        SidsteStop.StoppestedID = Convert.ToInt32(SidsteLinje.Values[9]);
        SidsteStop.GetUpdate();
        return SidsteStop;
    }

    private static bool IsFirstStop(Stoppested stoppested, Bus BusInQuestion)
    {
        return AntalBesøgteStop(stoppested, BusInQuestion) == 0;
    }

    private static int AntalBesøgteStop(Stoppested Stoppested, Bus BusInQuestion)
    {
        int i = 1;

        foreach (var item in BusInQuestion.Rute.StoppeSteder)
        {
            if (item.StoppestedID != Stoppested.StoppestedID)
            {
                i++;
            }
            else
            {
                break;
            }
        }
        return i;
    }

    public static Bus StartAlgoritmen(Bus placeholderBus)
    {
        Stoppested NuværendeStop = GetCurrentStop(placeholderBus);
        int AntalBesøgteStoppesteder = AntalBesøgteStop(NuværendeStop, placeholderBus);
        return Algoritmen(placeholderBus, GetSidsteMånedData(placeholderBus), AntalBesøgteStoppesteder);
    }

    public static List<AfPåTidCombi> GetSidsteMånedData(Bus placeholderBus)
    {
        Stoppested NuværendeStop = GetCurrentStop(placeholderBus);
        int AntalBesøgteStoppesteder = AntalBesøgteStop(NuværendeStop, placeholderBus);
        int AntalTotaleStopPåRute = placeholderBus.Rute.StoppeSteder.Count();
        // Finder ud om der er tale om en weekend eller en hverdag
        // Hvis det er en hverdag: Så vælg alle dage som er hverdage
        // Hvis der er en weekend: Så vælg alle dage som er weekend
        string whereCondition = (int)placeholderBus.StoppeStederMTid[AntalBesøgteStoppesteder].AfPåTidComb.First().UgeDag <= 5 ? $"day <= 5 AND busID = {placeholderBus.BusID}" : $"day >= 6 AND busID = {placeholderBus.BusID}";
        int WeekDaysInHistory = (int)placeholderBus.StoppeStederMTid[AntalBesøgteStoppesteder].AfPåTidComb.First().UgeDag <= 5 ? 5 : 2;
        // Henter en måneds historik, ikke nødvendigvis 4 uger, men den sidste måned af relevante dage.
        var MånedHistorik = MysqlControls.SelectAllWhere(new AfPåTidCombi().GetTableName(), $"{whereCondition} ORDER BY ID DESC LIMIT {AntalTotaleStopPåRute * WeekDaysInHistory * 4}");
        List<AfPåTidCombi> SidsteMånedAfPåTid = new List<AfPåTidCombi>();
        foreach (var item in MånedHistorik.RowData)
        {
            AfPåTidCombi NewAfPåTid = new AfPåTidCombi();
            NewAfPåTid.Update(item);
            SidsteMånedAfPåTid.Add(NewAfPåTid);
        }
        return SidsteMånedAfPåTid;
    }

    public static Bus Algoritmen(Bus placeholderBus, List<AfPåTidCombi> Data, int AntalBesøgteStoppesteder)
    {
        List<AfPåTidCombi> StoppeStederTid = new List<AfPåTidCombi>();
        int AntalTotaleStopPåRute = placeholderBus.Rute.StoppeSteder.Count();
        List<AfPåTidCombi> SidsteMånedAfPåTid = new List<AfPåTidCombi>();
        SidsteMånedAfPåTid = Data;
        // Henter alle ID's på de stoppesteder der er besøgt
        List<int> BesøgteStopIDs = new List<int>();
        for (int i = 0; i < AntalBesøgteStoppesteder; i++)
        {
            BesøgteStopIDs.Add(placeholderBus.Rute.StoppeSteder[i].StoppestedID);
        }

        List<double> AfstigningerAverage = new List<double>();
        List<double> PåstigningerAverage = new List<double>();
        List<double> MånedAverage = new List<double>();
        int h = 0;
        BesøgteStopIDs.Reverse();
        foreach (var item in BesøgteStopIDs.Take(5))
        {
            AfstigningerAverage.Add(SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == item).Average(x => x.Afstigninger));
            PåstigningerAverage.Add(SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == item).Average(x => x.Påstigninger));
            MånedAverage.Add(-AfstigningerAverage[h] + PåstigningerAverage[h]);
            h++;
        }

        // Hvis bussen har besøgt mere end 1 stoppested.
        List<double> LastStopsData = new List<double>();
        //List<double> LastStopsDataHis = new List<double>();
        if (AntalBesøgteStoppesteder > 1)
        {
            // Sikre at der ikke kan blive valgt mere end 5, men der må godt vælges mindre end 5 
            int antal = AntalBesøgteStoppesteder <= 5 ? AntalBesøgteStoppesteder : 5;
            // Sidste antal stops, men maks 5
            var SidsteStops = placeholderBus.StoppeStederMTid.OrderByDescending(x => x.AfPåTidComb.First().ID).Take(antal).ToList();
            // Beregner gennemsnittet af de sidste stoppesteder, maks 5 sidste.
            for (int i = 0; i < antal; i++)
            {

                LastStopsData.Add(-placeholderBus.StoppeStederMTid[i].AfPåTidComb.First().Afstigninger + placeholderBus.StoppeStederMTid[i].AfPåTidComb.First().Påstigninger);
            }
        }

        int Afvigelse = 0;
        // Hvis den har besøgt mere end 1 stoppested, så skal afvigelse beregnes
        if (AntalBesøgteStoppesteder > 1)
        {
            Afvigelse = (int)Math.Round(LastStopsData.Average() - MånedAverage.Average(), 0);
        }
        int Total = placeholderBus.PassengersTotal;
        BesøgteStopIDs.Reverse();
        // For alle stoppesteder som er besøgt, skal der beregnes forventet passagere tal
        for (int i = AntalBesøgteStoppesteder; i < AntalTotaleStopPåRute; i++)
        {
            if (!BesøgteStopIDs.Take(BesøgteStopIDs.Count - 1).Contains(placeholderBus.StoppeStederMTid[i].Stop.StoppestedID))
            {
                double _afstigningerAverage = (SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == placeholderBus.StoppeStederMTid[i].Stop.StoppestedID)).Average(x => x.Afstigninger);
                double _påstigningerAverage = (SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == placeholderBus.StoppeStederMTid[i].Stop.StoppestedID)).Average(x => x.Påstigninger);
                int SingleAfvigelse = (int)Math.Round((-_afstigningerAverage + _påstigningerAverage) + Afvigelse, 0);
                placeholderBus.StoppeStederMTid[i].AfPåTidComb[0].ForventetPassagere = Total + SingleAfvigelse;
                if (placeholderBus.StoppeStederMTid[i].AfPåTidComb[0].ForventetPassagere < 0)
                {
                    placeholderBus.StoppeStederMTid[i].AfPåTidComb[0].ForventetPassagere = 0;
                }
                Total += SingleAfvigelse;
            }
            else
            {
                placeholderBus.StoppeStederMTid[i].AfPåTidComb[0].ForventetPassagere = placeholderBus.StoppeStederMTid[i].AfPåTidComb[0].TotalPassagere;
            }
        }
        new Thread(new ThreadStart(placeholderBus.UploadToDatabase)).Start();
        return placeholderBus;
    }

    private static int FindAverage(List<AfPåTidCombi> combiList)
    {
        decimal average = 0;
        foreach (AfPåTidCombi combi in combiList)
        {
            average += -combi.Afstigninger + combi.Påstigninger;
        }
        average /= combiList.Count();

        return (int)Math.Round(average);
    }



    private static void UpdateTravlhed(int afvigelse, ref List<int> TravlSidsteStops)
    {
        if (TravlSidsteStops.Count == 5)
        {
            TravlSidsteStops.RemoveAt(0);
            TravlSidsteStops.Add(afvigelse);
        }
        else
        {
            TravlSidsteStops.Add(afvigelse);
        }
    }

}

