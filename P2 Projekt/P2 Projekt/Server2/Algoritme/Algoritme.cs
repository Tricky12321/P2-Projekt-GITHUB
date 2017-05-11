using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Algoritme
{
    int travlhedsFaktor = 1;
    int forventedePassagerer;
    int forventedeAfvigelse;
    List<int> travlSidsteStops = new List<int>();
    List<AfPåTidCombi> DataAlgorithm = new List<AfPåTidCombi>();

    //day >= 1 AND day <= 5 AND busID = 30
    public Algoritme(string whereCondition)
    {
        List<NetworkObject> DatabaseAlgorithm;
        RealClient AlgorithmDataClient = new RealClient();
        DatabaseAlgorithm = AlgorithmDataClient.RequestAllWhere(ObjectTypes.AfPaaTidCombi, whereCondition);

        foreach (var item in DatabaseAlgorithm)
        {
            DataAlgorithm.Add(item as AfPåTidCombi);
        }
        Algoritmen();
    }

    private int Algoritmen()
    {
        List<AfPåTidCombi> lastFiveStops = DataAlgorithm.OrderByDescending(x => x.ID).Take(5).ToList();

        List<List<AfPåTidCombi>> lastFiveHistory = new List<List<AfPåTidCombi>>();
        List<List<AfPåTidCombi>> futureHistory = new List<List<AfPåTidCombi>>();

        int[] lastFiveAverages = new int[lastFiveStops.Count];
        for (int i = 0; i < lastFiveStops.Count; i++)
        {
            lastFiveHistory.Add(DataAlgorithm.Where(x => x.Stop.StoppestedID == lastFiveStops[i].Stop.StoppestedID).ToList());
            lastFiveAverages[i] = FindAverage(lastFiveHistory[i]);
        }

        int indexForStop = DataAlgorithm.First().Bus.Rute.StoppeSteder.FindIndex(x => x.StoppestedID == DataAlgorithm.Last().Stop.StoppestedID);
        Stoppested nextStop = DataAlgorithm.First().Bus.Rute.StoppeSteder[indexForStop + 1];

        int[] futureFiveAverages = new int[futureHistory.Count];
        for (int i = 0; i < DataAlgorithm.First().Bus.Rute.StoppeSteder.Count - indexForStop + 1; i++)
        {
            futureHistory.Add(DataAlgorithm.Where(x => x.Stop.StoppestedID == DataAlgorithm.First().Bus.Rute.StoppeSteder[indexForStop + 1].StoppestedID).ToList());
            futureFiveAverages[i] = FindAverage(futureHistory[i]);
        }

        forventedeAfvigelse = (int)Math.Round(travlSidsteStops.Average());
        DataAlgorithm.First().Bus.StoppeStederMTid.








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
        return (int)Resultat;

    }

    private int FindAverage(List<AfPåTidCombi> combiList)
    {
        decimal average = 0;
        foreach (AfPåTidCombi combi in combiList)
        {
            average += -combi.Afstigninger + combi.Påstigninger;
        }
        average /= combiList.Count();

        return (int)Math.Round(average);
    }



    private void UpdateTravlhed(int afvigelse)
    {
        if (travlSidsteStops.Count == 5)
        {
            travlSidsteStops.RemoveAt(0);
            travlSidsteStops.Add(afvigelse);
        }
        else
        {
            travlSidsteStops.Add(afvigelse);
        }
    }

}

