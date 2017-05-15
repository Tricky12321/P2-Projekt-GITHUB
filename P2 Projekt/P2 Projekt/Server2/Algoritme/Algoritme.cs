using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class Algoritme
{

    public static Bus placeholderBus;

    //day >= 1 AND day <= 5 AND busID = 30
    public static void GetAlgoritmeData(string whereCondition, Bus PlaceholderBus, List<AfPåTidCombi> DataAlgorithm)
    {
        AfPåTidCombi PlaceholderTest = new AfPåTidCombi();
        var AlleBusserFraDatabase = MysqlControls.SelectAllWhere(PlaceholderTest.GetTableName(),whereCondition);
        foreach (var item in AlleBusserFraDatabase.RowData)
        {
            AfPåTidCombi AfPåToAdd = new AfPåTidCombi();
            AfPåToAdd.Update(item);
            DataAlgorithm.Add(AfPåToAdd);
        }

        PlaceholderBus = DataAlgorithm.First().Bus;
        PlaceholderBus.GetUpdate();
    }

    public static void Algoritmen(Bus placeholderBus)
    {
        int forventedeAfvigelse;
        List<int> TravlSidsteStops = new List<int>();
        List<AfPåTidCombi> DataAlgorithm = new List<AfPåTidCombi>();
        string whereCondition = "day >= 1 AND day <= 5 AND busID = " + placeholderBus.BusID;
        GetAlgoritmeData(whereCondition, placeholderBus, DataAlgorithm);
        List<AfPåTidCombi> lastFiveStops = DataAlgorithm.OrderByDescending(x => x.ID).Take(5).ToList();
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
        UpdateTravlhed(forventedeAfvigelse, TravlSidsteStops);
        int StoppeStederCount2 = placeholderBus.Rute.StoppeSteder.Count - (indexForStop + 1);
        for (int i = 0; i < StoppeStederCount2; i++)
        {

            placeholderBus.StoppeStederMTid[indexForStop + 1 + i].AfPåTidComb.First().ForventetAfvigelse = futureAverages[i] + forventedeAfvigelse;
            
        }
        
        placeholderBus.UploadToDatabase();

        #region comment
        /*
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



    private static void UpdateTravlhed(int afvigelse, List<int> TravlSidsteStops)
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

