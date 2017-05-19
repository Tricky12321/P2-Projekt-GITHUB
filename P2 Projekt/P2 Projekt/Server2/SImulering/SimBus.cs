using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ProgramTilBusselskab;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Diagnostics;

namespace ServerGPSSimulering
{
    class SimBus
    {
        public static Random FirstRandom = new Random();
        public static Random SecondRandom = new Random();
        public Bus SimulatedBus;
        public SimRoute SimulatedRute;
        public List<int> VisitedStops = new List<int>();
        public double RuteDistance;

        public int StartHour;
        public int StartMinut;
        public int SlutHour;
        public int SlutMinut;

        public int DrivetimeInSeconds;
        public bool NoDelay = false;
        public double busAvgSpeedMprSec;

        public day UgeDag;
        public const int Week = 5;

        private int AlgoSleeptime = 0;
        public SimBus(Bus simulatedBus, day WeekDay, bool delay = true)
        {
            UgeDag = WeekDay;
            simulatedBus.GetUpdate();
            SimulatedBus = simulatedBus;
            SimulatedRute = new SimRoute(SimulatedBus.Rute, "Simuleringsrute");
            NoDelay = !delay;
            int elementerIRute = SimulatedRute.route.Points.Count();
            for (int i = 0; i < elementerIRute; ++i)
            {
                if (i + 1 < elementerIRute)
                {
                    RuteDistance += DistanceBetweenPoints(SimulatedRute.route.Points[i].Lat, SimulatedRute.route.Points[i].Lng, SimulatedRute.route.Points[i + 1].Lat, SimulatedRute.route.Points[i + 1].Lng);
                }
            }

            StartHour = SimulatedBus.StoppeStederMTid.First().AfPåTidComb.First().Tidspunkt.hour;
            StartMinut = SimulatedBus.StoppeStederMTid.First().AfPåTidComb.First().Tidspunkt.minute;

            SlutHour = SimulatedBus.StoppeStederMTid.Last().AfPåTidComb.First().Tidspunkt.hour;
            SlutMinut = SimulatedBus.StoppeStederMTid.Last().AfPåTidComb.First().Tidspunkt.minute;

            DrivetimeInSeconds = ((SlutHour - StartHour) * 60 + SlutMinut - StartMinut) * 60;
            Print.PrintCenterColor("Det skal tage ", (DrivetimeInSeconds).ToString(), " sek at køre ruten", ConsoleColor.Green);

            busAvgSpeedMprSec = RuteDistance * 1000 / DrivetimeInSeconds;
            MoveToStart();
            Thread BusMovementThread = new Thread(new ThreadStart(BusMovement));
            BusMovementThread.Start();
            //Print.WriteLine("Simlering startet");
        }

        private void MoveToStart()
        {
            GPS Start = new GPS();
            int points = SimulatedRute.route.Points.Count();
            //Print.WriteLine($"Points: {points}");
            if (points > 0)
            {
                Start.xCoordinate = SimulatedRute.route.Points.First().Lat;
                Start.yCoordinate = SimulatedRute.route.Points.First().Lng;
                SimulatedBus.placering = Start;
                SimulatedBus.PassengersTotal = RandomPassagerer();
                Stoppested Stop = SimulatedBus.StoppeStederMTid.First().Stop;
                Tidspunkt Time = SimulatedBus.StoppeStederMTid.First().AfPåTidComb.First().Tidspunkt;
                AfPåTidCombi NewAfPåTidComb = new AfPåTidCombi(0, SimulatedBus.PassengersTotal, Stop, SimulatedBus, UgeDag, Week, Time, SimulatedBus.PassengersTotal, SimulatedBus.TotalCapacity);
                SimulatedBus.StoppeStederMTid.First().AfPåTidComb[0] = NewAfPåTidComb;
                NewAfPåTidComb.ForventetPassagere = SimulatedBus.PassengersTotal;
                NewAfPåTidComb.UploadToDatabase();
                SimulatedBus.UploadToDatabase();
            }
        }

        private void CheckIfAtStopAndUpdate(ref int j, int StoppeStederCount, bool AtStop, ref double distance, ref GPS NextStop, double DistanceToStopThreshHold, ref double TotalAlgoTime)
        {
            if (j < StoppeStederCount)
            {
                int stopnr = j;
                distance = DistanceBetweenPoints(SimulatedBus.placering, NextStop);
                AtStop = distance < DistanceToStopThreshHold;
                if (AtStop)
                {
                    Stopwatch Test = new Stopwatch();
                    Test.Start();
                    //Print.WriteLine("Reached a stop!");
                    if (SimulatedBus.StoppeStederMTid.Last().Stop.StoppestedID != SimulatedBus.StoppeStederMTid[j + 1].Stop.StoppestedID && !VisitedStops.Contains(SimulatedBus.StoppeStederMTid[j + 1].Stop.StoppestedID))
                    {
                        Print.WriteLine("Set next stop!");
                        VisitedStops.Add(SimulatedBus.StoppeStederMTid[j + 1].Stop.StoppestedID);

                        NextStop = SimulatedBus.StoppeStederMTid[j + 1].Stop.StoppestedLok;
                        Print.PrintColorLine($"Distance to next stop is {DistanceBetweenPoints(NextStop, SimulatedBus.placering)}", ConsoleColor.Green);
                        Print.PrintColorLine($"Next stop is: {SimulatedBus.StoppeStederMTid[j + 1].Stop.StoppestedName}", ConsoleColor.Green);
                    }
                    int RngPassPÅ = RandomPassagerer();
                    int RngPassAF = RandomPassagerer();
                    Stoppested Stop = SimulatedBus.StoppeStederMTid[j].Stop;
                    Tidspunkt Time = SimulatedBus.StoppeStederMTid[j].AfPåTidComb.First().Tidspunkt;
                    // Sørger for at bussen ikke kan have et negativt antal passagere
                    if (SimulatedBus.PassengersTotal - RngPassAF < 0)
                    {
                        RngPassAF = SimulatedBus.PassengersTotal;
                    }
                    SimulatedBus.PassengersTotal -= RngPassAF;
                    // Sørger for at bussen ikke kan have mere end kapaciteten
                    if (SimulatedBus.PassengersTotal + RngPassPÅ > SimulatedBus.TotalCapacity)
                    {
                        RngPassPÅ = SimulatedBus.PassengersTotal - SimulatedBus.TotalCapacity;
                    }
                    SimulatedBus.PassengersTotal += RngPassPÅ;
                    AfPåTidCombi NewAfPåTidComb = new AfPåTidCombi(RngPassAF, RngPassPÅ, Stop, SimulatedBus, UgeDag, Week, Time, SimulatedBus.PassengersTotal, SimulatedBus.TotalCapacity);
                    // Sørger for at besøgte stoppesteder har det rigtige antal forventede passagere.
                    NewAfPåTidComb.ForventetPassagere = SimulatedBus.PassengersTotal;
                    NewAfPåTidComb.UploadToDatabase();
                    // Starter Algoritmen
                    SimulatedBus = Algoritme.StartAlgoritmen(SimulatedBus);
                    SimulatedBus.StoppeStederMTid[j].AfPåTidComb[0] = NewAfPåTidComb;
                    SendToServer();
                    Test.Stop();
                    TotalAlgoTime += (Test.ElapsedMilliseconds / 1000);
                    AlgoSleeptime += (int)Test.ElapsedMilliseconds;
                    j++;
                }

            }
        }

        private void DoSteps(ref int j, ref int steps, ref bool AtStop, ref double distance, double DistanceToStopThreshHold, ref GPS SinglePoint, ref double DistanceToNextStop, ref GPS NextPoint, int timeBetweenPointsMilSec)
        {
            for (int k = 0; k < steps && !AtStop; k++)
            {
                Stopwatch Timer2 = new Stopwatch();
                Timer2.Start();
                distance = DistanceBetweenPoints(SimulatedBus.placering.xCoordinate, SimulatedBus.placering.yCoordinate, SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate, SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate);
                AtStop = distance < DistanceToStopThreshHold;
                GPS NyBusLok = SimulatedBus.placering;
                NyBusLok.xCoordinate -= SinglePoint.xCoordinate;
                NyBusLok.yCoordinate -= SinglePoint.yCoordinate;
                DistanceToNextStop = DistanceBetweenPoints(NyBusLok, NextPoint);
                SimulatedBus.placering = NyBusLok;
                int Sleeptime = (timeBetweenPointsMilSec / steps);
                Timer2.Stop();
                AlgoSleeptime += (int)Timer2.ElapsedMilliseconds;
                if (Sleeptime > 0)
                {
                    if ((Sleeptime - AlgoSleeptime) > 0)
                    {
                        Thread.Sleep(Sleeptime - AlgoSleeptime);
                        AlgoSleeptime = 0;
                    }
                    else
                    {
                        AlgoSleeptime -= Sleeptime;
                    }
                }
                SendToServer();
            }
        }

        private void BusMovement()
        {
            int ElementerIRute = SimulatedRute.route.Points.Count();
            int steps = 3;
            double distanceBetweenPoints;
            double timeBetweenPoints;
            int timeBetweenPointsMilSec;
            int j = 1;
            double DistanceToStopThreshHold = 0.05;
            GPS NextStop = SimulatedBus.StoppeStederMTid[1].Stop.StoppestedLok;
            Stopwatch Timer = new Stopwatch();
            double TotalAlgoTime = 0;
            Timer.Start();
            // Starter med at sætte bussen til at være ved det første punkt.
            int StoppeStederCount = SimulatedBus.StoppeStederMTid.Count;
            for (int i = 0; i < ElementerIRute; i++)
            {
                if (i + 1 < ElementerIRute)
                {
                    distanceBetweenPoints = DistanceBetweenPoints(SimulatedRute.route.Points[i].Lat, SimulatedRute.route.Points[i].Lng, SimulatedRute.route.Points[i + 1].Lat, SimulatedRute.route.Points[i + 1].Lng) * 1000;
                    timeBetweenPoints = distanceBetweenPoints / busAvgSpeedMprSec;
                    timeBetweenPointsMilSec = (int)(timeBetweenPoints * 1000);
                    GPS FirstPoint = new GPS(SimulatedRute.route.Points[i].Lat, SimulatedRute.route.Points[i].Lng);
                    GPS NextPoint = new GPS(SimulatedRute.route.Points[i + 1].Lat, SimulatedRute.route.Points[i + 1].Lng);
                    // for steps = 10, vil kordinaterne for single point, være 1/10 af afstanden mellem 2 punkter
                    double DistanceBetweenNextStops = DistanceBetweenPoints(NextPoint.xCoordinate, NextPoint.yCoordinate, FirstPoint.xCoordinate, FirstPoint.yCoordinate);
                    GPS SinglePoint = new GPS((FirstPoint.xCoordinate - NextPoint.xCoordinate) / steps, (FirstPoint.yCoordinate - NextPoint.yCoordinate) / steps);
                    double DistanceToNextStop = DistanceBetweenPoints(SimulatedBus.placering, NextPoint);
                    double distance = DistanceBetweenPoints(SimulatedBus.placering, NextStop);
                    bool AtStop = distance < DistanceToStopThreshHold;
                    DoSteps(ref j, ref steps, ref AtStop, ref distance, DistanceToStopThreshHold, ref SinglePoint, ref DistanceToNextStop, ref NextPoint, timeBetweenPointsMilSec);
                    CheckIfAtStopAndUpdate(ref j, StoppeStederCount, AtStop, ref distance, ref NextStop, DistanceToStopThreshHold, ref TotalAlgoTime);
                }
                else
                {
                    SimulatedBus.placering.xCoordinate = SimulatedRute.route.Points[i].Lat;
                    SimulatedBus.placering.yCoordinate = SimulatedRute.route.Points[i].Lng;
                    SendToServer();
                }

            }
            Timer.Stop();
            Print.PrintCenterColor("Simulation took: ", ((double)Timer.ElapsedMilliseconds / 1000).ToString(), " sec", ConsoleColor.Green);
        }

        private int RandomPassagerer()
        {
            int negPosPas = FirstRandom.Next(1, 10);

            switch (negPosPas)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    {
                        return SecondRandom.Next(0, 3);
                    }
                case 5:
                case 6:
                case 7:
                    {
                        return SecondRandom.Next(0, 7);
                    }
                case 8:
                case 9:
                    {
                        return SecondRandom.Next(0, 10);
                    }
                case 10:
                    {
                        return SecondRandom.Next(0, 15);
                    }
            }
            return 0;
        }

        private void SendToServer()
        {
            Debug.WriteLine(SimulatedBus.busName + " : " + SimulatedBus.placering.xCoordinate + " : " + SimulatedBus.placering.yCoordinate + " : " + SimulatedBus.PassengersTotal);
            new Thread(new ThreadStart(SimulatedBus.UploadToDatabase)).Start();
        }

        private double DistanceBetweenPoints(GPS First, GPS Second)
        {
            return DistanceBetweenPoints(First.xCoordinate, First.yCoordinate, Second.xCoordinate, Second.yCoordinate);
        }

        private double DistanceBetweenPoints(double firstxCoordinate, double firstyCoordinate, double lastxCoordinate, double lastyCoordinate)
        {
            List<PointLatLng> placeholderPoints = new List<PointLatLng>();
            placeholderPoints.Add(new PointLatLng(firstyCoordinate, firstxCoordinate));
            placeholderPoints.Add(new PointLatLng(lastyCoordinate, lastxCoordinate));
            GMapRoute distanceRoute = new GMapRoute(placeholderPoints, "Placeholder");
            return distanceRoute.Distance;
        }
    }
}
