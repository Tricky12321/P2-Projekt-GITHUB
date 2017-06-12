using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

public static class Algoritme
{
    //finder bussens sidst besøgte stoppested
    private static Stoppested GetCurrentStop(Bus EnEllerAndenBus)
    {
        Stoppested SidsteStop = new Stoppested();
        Row SidsteLinje = MysqlControls.GetLastLineFrom(new AfPåTidCombi().GetTableName(), $"WHERE busID = {EnEllerAndenBus.BusID}");
        SidsteStop.StoppestedID = Convert.ToInt32(SidsteLinje.Values[9]);
        SidsteStop.GetUpdate();
        return SidsteStop;
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
        string whereCondition = (int)placeholderBus.StoppeStederMTid[AntalBesøgteStoppesteder].AfPåTidComb.First().UgeDag <= 5 
            ? $"day <= 5 AND busID = {placeholderBus.BusID}" 
            : $"day >= 6 AND busID = {placeholderBus.BusID}";
        int WeekDaysInHistory = (int)placeholderBus.StoppeStederMTid[AntalBesøgteStoppesteder].AfPåTidComb.First().UgeDag <= 5 ? 5 : 2;
        // Henter en måneds historik, ikke nødvendigvis 4 uger, men den sidste måned af relevante dage.
        TableDecode MånedHistorik = MysqlControls.SelectAllWhere(new AfPåTidCombi().GetTableName(), $"{whereCondition} ORDER BY ID DESC LIMIT {AntalTotaleStopPåRute * WeekDaysInHistory * 4}");
        List<AfPåTidCombi> SidsteMånedAfPåTid = new List<AfPåTidCombi>();
        foreach (Row item in MånedHistorik.RowData)
        {
            AfPåTidCombi NewAfPåTid = new AfPåTidCombi();
            NewAfPåTid.Update(item);
            SidsteMånedAfPåTid.Add(NewAfPåTid);
        }
        return SidsteMånedAfPåTid;
    }

    public static Bus Algoritmen(Bus placeholderBus, List<AfPåTidCombi> SidsteMånedAfPåTid, int AntalBesøgteStoppesteder)
    {
        // Henter alle ID's på de stoppesteder der er besøgt, og ligger dem i en liste for at tjekke hvilkestoppesteder der er besøgt. 
        List<int> BesøgteStopIDs = new List<int>();
        for (int i = 0; i < AntalBesøgteStoppesteder; i++)
        {
            BesøgteStopIDs.Add(placeholderBus.Rute.StoppeSteder[i].StoppestedID);
        }

        List<double> MånedAverage = new List<double>();
        BesøgteStopIDs.Reverse();
        // Vælger de seneste 5 stoppesteder fra ruten som der bliver kørt på lige nu. 
        foreach (int item in BesøgteStopIDs.Take(5))
        {
            double AfstigningerAverage = SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == item).Average(x => x.Afstigninger);
            double PåstigningerAverage = SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == item).Average(x => x.Påstigninger);

            MånedAverage.Add(-AfstigningerAverage + PåstigningerAverage);
        }

        // Hvis bussen har besøgt mere end 1 stoppested.
        List<double> LastStopsData = new List<double>();
        //List<double> LastStopsDataHis = new List<double>();
        int TotaleAntalStops = placeholderBus.Rute.StoppeSteder.Count();
        if (AntalBesøgteStoppesteder > 1)
        {
            // Sikre at der ikke kan blive valgt mere end 5, men der må godt vælges mindre end 5 
            int antal = AntalBesøgteStoppesteder <= 5 ? AntalBesøgteStoppesteder : 5;
            // Sidste antal stops, men maks 5
            // Beregner gennemsnittet af de sidste stoppesteder, maks 5 sidste.
            int StartStop = (AntalBesøgteStoppesteder);
            int SlutStop = StartStop - antal;
            for (int i = StartStop - 1; i > SlutStop - 1; i--)
            {
                AfPåTidCombi BusAfPåTidCombo = placeholderBus.StoppeStederMTid[i].AfPåTidComb.First();
                LastStopsData.Add(-BusAfPåTidCombo.Afstigninger + BusAfPåTidCombo.Påstigninger);
            }
        }

        double Afvigelse = 0;
        double LastStopsAverage = LastStopsData.Average();
        double MånedAverageSingle = MånedAverage.Average();
        // Hvis den har besøgt mere end 1 stoppested, så skal afvigelse beregnes
        if (AntalBesøgteStoppesteder > 1)
        {
            Afvigelse = LastStopsAverage - MånedAverageSingle;
        }
        // Total er bussen Totale passageretal før algoritmen
        int Total = 0;
        BesøgteStopIDs.Reverse();
        // For alle stoppesteder som ikke er besøgt, skal der beregnes forventet passagere tal
        for (int i = 0; i < TotaleAntalStops; i++)
        {
            // Sætter nogle hjælpe variabler for at gøre udtryk kortere
            int StoppeStedsID = placeholderBus.StoppeStederMTid[i].Stop.StoppestedID;
            AfPåTidCombi StoppeStedsAfPåTid = placeholderBus.StoppeStederMTid[i].AfPåTidComb[0];
            if (!BesøgteStopIDs.Contains(StoppeStedsID))
            {
                // Beregner gennemsnittet af af og påstigninger den sidste måned

                double _afstigningerAverage = (SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == StoppeStedsID)).Average(x => x.Afstigninger);
                double _påstigningerAverage = (SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == StoppeStedsID)).Average(x => x.Påstigninger);
                // Beregner den gennemsnitlige afvigelse som denne dag har haft indtil videre. 

                double SingleAfvigelse = (-_afstigningerAverage + _påstigningerAverage) + Afvigelse;
                // Sætter bussens forventede passagertal, ved kommende stop, til bussens forventede total, og afvigelsen for dagen sammen
                StoppeStedsAfPåTid.ForventetPassagere = (int)Math.Round(Total + SingleAfvigelse, 0);
                // Sikrer at bussen ikke kan have negative passagerantal
                if (StoppeStedsAfPåTid.ForventetPassagere < 0)
                {
                    StoppeStedsAfPåTid.ForventetPassagere = 0;
                }
                Debug.Print($"Afvigelse er: {SingleAfvigelse} | AF: {_afstigningerAverage} | PÅ: {_påstigningerAverage}");
                Debug.Print($"Total er: {Total}");
                // Total bliver talt op fordi den næste bus skal have data fra det foregående stoppested
                Total += (int)Math.Round(SingleAfvigelse, 0);
            }
            else
            {
                Total = StoppeStedsAfPåTid.TotalPassagere;
                // Hvis dette er et stoppested som allerede er besøgt, skal det ForventedePassagere antal, bare sættes til TotalPassagere
                StoppeStedsAfPåTid.ForventetPassagere = StoppeStedsAfPåTid.TotalPassagere;
            }
        }
        // Starter en ny tråd, som har til opgave at uploade den opdaterede bus til databasen. 
        // new Thread(new ThreadStart(placeholderBus.UploadToDatabase)).Start();
        // Returnere bussen, så simuleringen kan modtage den opdaterede bus. 
        return placeholderBus;
    }
}

