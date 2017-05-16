using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public double RuteDistance;

        public int StartHour;
        public int StartMinut;
        public int SlutHour;
        public int SlutMinut;

        public int DrivetimeInSeconds;
        public double busAvgSpeedMprSec;

        public day UgeDag;
        public const int Week = 5;

        public SimBus(Bus simulatedBus, day WeekDay)
        {
            UgeDag = WeekDay;
            simulatedBus.GetUpdate();
            SimulatedBus = simulatedBus;
            SimulatedRute = new SimRoute(SimulatedBus.Rute, "Simuleringsrute");
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

            busAvgSpeedMprSec = RuteDistance * 1000 / DrivetimeInSeconds;
            //MoveToStart();
            Thread BusMovementThread = new Thread(new ThreadStart(BusMovement));
            BusMovementThread.Start();
            Print.WriteLine("Simlering startet");
        }

        private void MoveToStart()
        {
            GPS Start = new GPS();
            int points = SimulatedRute.route.Points.Count();
            Print.WriteLine($"Points: {points}");
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
                Algoritme.Algoritmen(ref SimulatedBus);
            }
        }

        public void BedreBusMovement()
         {
             int ElementerIRute = SimulatedRute.route.Points.Count();
             int steps = 5;
             if (NoDelay)
             {
                 steps = 1;
             }
             double distanceBetweenPoints;
             double timeBetweenPoints;
             int timeBetweenPointsMilSec;
             int j = 1;


             // Starter med at sætte bussen til at være ved det første punkt.
             for (int i = 0; i < ElementerIRute; i++)
             {
                 if (i + 1 < ElementerIRute)
                 {
                     distanceBetweenPoints = DistanceBetweenPoints(SimulatedRute.route.Points[i].Lat, SimulatedRute.route.Points[i].Lng, SimulatedRute.route.Points[i + 1].Lat, SimulatedRute.route.Points[i + 1].Lng) * 1000;

                     timeBetweenPoints = distanceBetweenPoints / busAvgSpeedMprSec;
                     timeBetweenPointsMilSec = (int)(timeBetweenPoints * 1000);

                     GPS FirstPoint = new GPS();
                     FirstPoint.xCoordinate = SimulatedRute.route.Points[i].Lat;
                     FirstPoint.yCoordinate = SimulatedRute.route.Points[i].Lng;
                     GPS NextPoint = new GPS();
                     NextPoint.xCoordinate = SimulatedRute.route.Points[i + 1].Lat;
                     NextPoint.yCoordinate = SimulatedRute.route.Points[i + 1].Lng;
                     // for steps = 10, vil kordinaterne for single point, være 1/10 af afstanden mellem 2 punkter
                     double DistanceBetweenNextStops = DistanceBetweenPoints(NextPoint.xCoordinate, NextPoint.yCoordinate, FirstPoint.xCoordinate, FirstPoint.yCoordinate);
                     //steps = (int)Math.Round(DistanceBetweenNextStops / 0.05f, 0);
                     GPS SinglePoint = new GPS();
                     SinglePoint.xCoordinate = (FirstPoint.xCoordinate - NextPoint.xCoordinate) / steps;
                     SinglePoint.yCoordinate = (FirstPoint.yCoordinate - NextPoint.yCoordinate) / steps;
                     //Debug.Print($"Taking {steps} steps...");
                     double DistanceToNextStop = DistanceBetweenPoints(SimulatedBus.placering, NextPoint);
                     double distance = DistanceBetweenPoints(SimulatedBus.placering.xCoordinate, SimulatedBus.placering.yCoordinate, SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate, SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate);
                     bool AtStop = distance < 0.05;
                     for (int k = 0; k < steps && DistanceToNextStop > 0 && AtStop == false; k++)
                     {
                         distance = DistanceBetweenPoints(SimulatedBus.placering.xCoordinate, SimulatedBus.placering.yCoordinate, SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate, SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate);
                         AtStop = distance < 0.05;
                         GPS NyBusLok = SimulatedBus.placering;
                         NyBusLok.xCoordinate -= SinglePoint.xCoordinate;
                         NyBusLok.yCoordinate -= SinglePoint.yCoordinate;
                         DistanceToNextStop = DistanceBetweenPoints(NyBusLok, NextPoint);
                         SimulatedBus.placering = NextPoint;
                         if (timeBetweenPointsMilSec/steps > 0f)
                         {
                             Debug.Print($"Sleeping for {timeBetweenPointsMilSec / steps}");
                             Thread.Sleep((timeBetweenPointsMilSec/steps)+5);
                         }
                         new Thread(new ThreadStart(SendToServer)).Start();
                         DistanceToNextStop = DistanceBetweenPoints(NyBusLok, NextPoint);
                         Debug.Print($"Step {k}/{steps}, Distance to next point is {DistanceToNextStop}");
                     }
                     if (j < SimulatedBus.StoppeStederMTid.Count)
                     {

                         Debug.Print($"Distance was: {distance.ToString()}");
                         int stopnr = j;
                         if (AtStop)
                         {

                             int RngPassPÅ = RandomPassagerer();
                             int RngPassAF = RandomPassagerer();
                             Stoppested Stop = SimulatedBus.StoppeStederMTid[j].Stop;
                             Tidspunkt Time = SimulatedBus.StoppeStederMTid[j].AfPåTidComb.First().Tidspunkt;
                             if (SimulatedBus.PassengersTotal - RngPassAF < 0)
                             {
                                 RngPassAF = SimulatedBus.PassengersTotal;
                             }
                             SimulatedBus.PassengersTotal -= RngPassAF;
                             //Thread.Sleep(10);
                             if (SimulatedBus.PassengersTotal + RngPassPÅ > SimulatedBus.TotalCapacity)
                             {
                                 RngPassPÅ = SimulatedBus.PassengersTotal - SimulatedBus.TotalCapacity;
                             }
                             SimulatedBus.PassengersTotal += RngPassPÅ;
                             AfPåTidCombi NewAfPåTidComb = new AfPåTidCombi(RngPassAF, RngPassPÅ, Stop, SimulatedBus, UgeDag, Week, Time, SimulatedBus.PassengersTotal, SimulatedBus.TotalCapacity);
                             NewAfPåTidComb.ForventetPassagere = SimulatedBus.PassengersTotal;
                             NewAfPåTidComb.UploadToDatabase();
                             SendToServer();
                             Debug.WriteLine("Stop:" + j);
                             SimulatedBus.StoppeStederMTid[j].AfPåTidComb[0] = NewAfPåTidComb;
                             j++;
                             Algoritme.Algoritmen(ref SimulatedBus);
                         }

                     }
                 }
                 else
                 {
                     SimulatedBus.placering.xCoordinate = SimulatedRute.route.Points[i].Lat;
                     SimulatedBus.placering.yCoordinate = SimulatedRute.route.Points[i].Lng;
                     SendToServer();

                 }

             }
         }
         

        public void BusMovement()
        {
            int elementerIRute = SimulatedRute.route.Points.Count();
            double distanceBetweenPoints;
            double timeBetweenPoints;
            int timeBetweenPointsMilSec;

            int algoritmeTid = 0;

            int j = 0;

            //Algoritme.Algoritmen(ref SimulatedBus);

            Stopwatch test = new Stopwatch();
            test.Start();

            for (int i = 0; i < elementerIRute; ++i)
            {
                if (i + 1 < elementerIRute)
                {
                    Stopwatch Tidstagning = new Stopwatch();

                    Tidstagning.Start();
                    GPS placeringPlaceholder = new GPS(SimulatedRute.route.Points[i].Lat, SimulatedRute.route.Points[i].Lng);
                    SimulatedBus.placering = placeringPlaceholder;

                    SendToServer();

                    distanceBetweenPoints = DistanceBetweenPoints(SimulatedRute.route.Points[i].Lat, SimulatedRute.route.Points[i].Lng, SimulatedRute.route.Points[i + 1].Lat, SimulatedRute.route.Points[i + 1].Lng);
                    distanceBetweenPoints *= 1000;

                    timeBetweenPoints = distanceBetweenPoints / busAvgSpeedMprSec;
                    timeBetweenPointsMilSec = (int)(timeBetweenPoints * 1000);
                    Tidstagning.Stop();

                    int Sleeptimer = timeBetweenPointsMilSec - (int)Tidstagning.ElapsedMilliseconds - algoritmeTid;
                    if (Sleeptimer > 0)
                    {
                        Thread.Sleep(Sleeptimer);
                        algoritmeTid = 0;
                    }
                    else
                    {
                        algoritmeTid -= timeBetweenPointsMilSec - (int)Tidstagning.ElapsedMilliseconds;
                    }

                    if (SimulatedBus.StoppeStederMTid.Count > j)
                    {
                        if (SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate < SimulatedRute.route.Points[i].Lat + 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate > SimulatedRute.route.Points[i].Lat - 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate < SimulatedRute.route.Points[i].Lng + 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate > SimulatedRute.route.Points[i].Lng - 0.001)
                        {
                            j++;

                            Stopwatch Algoritmetid = new Stopwatch();
                            Algoritmetid.Start();

                            SimulatedBus.PassengersTotal += RandomPassagerer();

                            Algoritme.Algoritmen(ref SimulatedBus);

                            Algoritmetid.Stop();
                            algoritmeTid += (int)Algoritmetid.ElapsedMilliseconds;
                            Debug.WriteLine("Stop:" + j);
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Alle stoppesteder er besøgt");
                    }
                }

                else
                {
                    SimulatedBus.placering.xCoordinate = SimulatedRute.route.Points[i].Lat;
                    SimulatedBus.placering.yCoordinate = SimulatedRute.route.Points[i].Lng;
                }
                SendToServer();
            }
            test.Stop();
            Debug.WriteLine(test.ElapsedMilliseconds);
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
            SimulatedBus.UploadToDatabase();
            //return SimulatedBus;
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
