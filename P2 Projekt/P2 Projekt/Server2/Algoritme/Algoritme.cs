using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

public static class Algoritme
{
    //day >= 1 AND day <= 5 AND busID = 30
    public static void GetAlgoritmeData(string whereCondition, ref Bus PlaceholderBus, List<AfPåTidCombi> DataAlgorithm)
    {
        AfPåTidCombi PlaceholderTest = new AfPåTidCombi();
        var AlleBusserFraDatabase = MysqlControls.SelectAllWhere(PlaceholderTest.GetTableName(), whereCondition);
        foreach (var item in AlleBusserFraDatabase.RowData)
        {
            AfPåTidCombi AfPåToAdd = new AfPåTidCombi();
            AfPåToAdd.Update(item);
            DataAlgorithm.Add(AfPåToAdd);
        }

        PlaceholderBus = DataAlgorithm.First().Bus;
        PlaceholderBus.GetUpdate();
    }

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

    public static void Algoritmen(Bus placeholderBus)
    {
        
        Stoppested NuværendeStop = GetCurrentStop(placeholderBus);
        int AntalBesøgteStoppesteder = AntalBesøgteStop(NuværendeStop, placeholderBus);
        List<AfPåTidCombi> StoppeStederTid = new List<AfPåTidCombi>();
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
        if (AntalBesøgteStoppesteder >= 3)
        {
            Afvigelse = (int)Math.Round(LastStopsData.Average() - MånedAverage.Average(),0);

        }
        int Total = placeholderBus.PassengersTotal;
        int AntalStopPåRute = placeholderBus.Rute.StoppeSteder.Count();
        for (int i = AntalBesøgteStoppesteder; i < AntalStopPåRute; i++)
        {
            double _afstigningerAverage = (SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == placeholderBus.StoppeStederMTid[i - 1].Stop.StoppestedID)).Average(x => x.Afstigninger);
            double _påstigningerAverage = (SidsteMånedAfPåTid.Where(x => x.Stop.StoppestedID == placeholderBus.StoppeStederMTid[i - 1].Stop.StoppestedID)).Average(x => x.Påstigninger);
            int SingleAfvigelse = (int)Math.Round((-_afstigningerAverage + _påstigningerAverage) + Afvigelse, 0);
            placeholderBus.StoppeStederMTid[i].AfPåTidComb.First().ForventetPassagere = Total + SingleAfvigelse;
            Total += SingleAfvigelse;
        }

        Debug.Print("");

        #region comment
        /*
        int forventedeAfvigelse;
        List<int> TravlSidsteStops = new List<int>();
        List<AfPåTidCombi> DataAlgorithm = new List<AfPåTidCombi>();
        //string whereCondition = "day >= 1 AND day <= 5 AND busID = " + placeholderBus.BusID;
        GetAlgoritmeData(whereCondition, ref placeholderBus, DataAlgorithm);


        List<List<AfPåTidCombi>> lastFiveHistory = new List<List<AfPåTidCombi>>();
        List<List<AfPåTidCombi>> futureHistory = new List<List<AfPåTidCombi>>();


        
        int LastFiveStopsCount = lastFiveStops.Count;
        int[] lastFiveAverages = new int[LastFiveStopsCount];
        for (int i = 0; i < LastFiveStopsCount; i++)
        {
            lastFiveHistory.Add(DataAlgorithm.Where(x => x.Stop.StoppestedID == lastFiveStops[i].Stop.StoppestedID).ToList());
            lastFiveAverages[i] = FindAverage(lastFiveHistory[i]);
        }

        int indexForStop = placeholderBus.Rute.StoppeSteder.FindIndex(x => x.StoppestedID == DataAlgorithm.Last().Stop.StoppestedID);
        List<int> futureAverages = new List<int>();
        int StoppeStederCount = placeholderBus.Rute.StoppeSteder.Count - (indexForStop + 1);
        for (int i = 0; i < StoppeStederCount; i++)
        {
            futureHistory.Add(DataAlgorithm.Where(x => x.Stop.StoppestedID == placeholderBus.Rute.StoppeSteder[indexForStop + 1 + i].StoppestedID).ToList());
            futureAverages.Add(FindAverage(futureHistory[i]));
        }
        forventedeAfvigelse = (int)Math.Round(TravlSidsteStops.Average());
        int StoppeStederCount2 = placeholderBus.Rute.StoppeSteder.Count - (indexForStop + 1);
        for (int i = 0; i < StoppeStederCount2; i++)
        {

            placeholderBus.StoppeStederMTid[indexForStop + 1 + i].AfPåTidComb.First().ForventetAfvigelse = futureAverages[i] + forventedeAfvigelse;

        }

        placeholderBus.UploadToDatabase();

        currentHistory = DataAlgorithm.Where(x => x.Stop.StoppestedID == nextStop.StoppestedID).ToList();

        int currentAverage = FindAverage(currentHistory);

        decimal Travlhedsfaktor = 1;
        decimal Resultat = ForrigeDage.Average();
        int[] FemSidsteStoppesteder;

        decimal[5] Forskel;
        decimal GennemsnitligForskel;
        decimal[] FemSidsteGennemsnit;

        //Udregning-----------------------------------------
        for (int i = 0; i < 5; ++i)
        {

            if (FemSidsteGennemsnit[i] <= 3 && FemSidsteGennemsnit[i] >= -3)
            {

            }
            else
            {
                Forskel[i] = FemSidsteStoppesteder[i] - FemSidsteGennemsnit[i];
            }
        }
        //Resultat------------------------------------------
        Travlhedsfaktor = Forskel.Average();
        Resultat *= Travlhedsfaktor;
        return (int)Resultat;*/
        #endregion
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

