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
        public Bus SimulatedBus;
        public SimRoute SimulatedRute;

        public double RuteDistance;

        public int StartHour;
        public int StartMinut;
        public int SlutHour;
        public int SlutMinut;

        public int DrivetimeInSeconds;
        public bool NoDelay = false;
        public double busAvgSpeedMprSec;

        public day UgeDag;

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
            Thread BusMovementThread = new Thread(new ThreadStart(BedreBusMovement));
            BusMovementThread.Start();
            Console.WriteLine("Simlering startet");
        }

        private void MoveToStart()
        {
            GPS Start = new GPS();
            int points = SimulatedRute.route.Points.Count();
            Console.WriteLine($"Points: {points}");
            if (points > 0)
            {
                Start.xCoordinate = SimulatedRute.route.Points.First().Lat;
                Start.yCoordinate = SimulatedRute.route.Points.First().Lng;
                SimulatedBus.placering = Start;
                SimulatedBus.PassengersTotal = new Random().Next(2, 10);
                Stoppested Stop = SimulatedBus.StoppeStederMTid.First().Stop;
                Tidspunkt Time = SimulatedBus.StoppeStederMTid.First().AfPåTidComb.First().Tidspunkt;
                AfPåTidCombi NewAfPåTidComb = new AfPåTidCombi(0, SimulatedBus.PassengersTotal, Stop, SimulatedBus, UgeDag, Time, SimulatedBus.PassengersTotal, SimulatedBus.CapacitySitting+SimulatedBus.CapacityStanding);
                NewAfPåTidComb.UploadToDatabase();
            }
            SimulatedBus.UploadToDatabase();
        }

        public void BedreBusMovement()
        {
            int ElementerIRute = SimulatedRute.route.Points.Count();
            int steps = 10;
            double distanceBetweenPoints;
            double timeBetweenPoints;
            int timeBetweenPointsMilSec;
            int j = 0;
            // Starter med at sætte bussen til at være ved det første punkt.
            MoveToStart();
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
                    GPS SinglePoint = new GPS();
                    SinglePoint.xCoordinate = (FirstPoint.xCoordinate - NextPoint.xCoordinate) / steps;
                    SinglePoint.yCoordinate = (FirstPoint.yCoordinate - NextPoint.yCoordinate) / steps;
                    for (int k = 0; k < steps; k++)
                    {
                        GPS NyBusLok = SimulatedBus.placering;
                        NyBusLok.xCoordinate -= SinglePoint.xCoordinate;
                        NyBusLok.yCoordinate -= SinglePoint.yCoordinate;
                        SimulatedBus.placering = NyBusLok;
                        SimulatedBus.UploadToDatabase();
                        if (timeBetweenPointsMilSec / steps > 0f)
                        {
                            if (!NoDelay) { Thread.Sleep(timeBetweenPointsMilSec / steps); }
                        }
                    }
                    if (SimulatedBus.StoppeStederMTid.Count > j)
                    {
                        bool AtStop = SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate < SimulatedRute.route.Points[i].Lat + 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate > SimulatedRute.route.Points[i].Lat - 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate < SimulatedRute.route.Points[i].Lng + 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate > SimulatedRute.route.Points[i].Lng - 0.001;

                        if (AtStop)
                        {
                            j++;

                            int RngPassPÅ = new Random().Next(2, 10);
                            int RngPassAF = new Random().Next(0, 5);
                            
                            if (SimulatedBus.PassengersTotal + (RngPassPÅ-RngPassAF) >= 0)
                            {
                                Stoppested Stop = SimulatedBus.StoppeStederMTid[j].Stop;
                                Tidspunkt Time = SimulatedBus.StoppeStederMTid[j].AfPåTidComb.First().Tidspunkt;
                                SimulatedBus.PassengersTotal += RngPassPÅ;
                                SimulatedBus.PassengersTotal -= RngPassAF;
                                AfPåTidCombi NewAfPåTidComb = new AfPåTidCombi(0, SimulatedBus.PassengersTotal, Stop, SimulatedBus, UgeDag, Time, SimulatedBus.PassengersTotal, SimulatedBus.CapacitySitting + SimulatedBus.CapacityStanding);
                                NewAfPåTidComb.UploadToDatabase();
                                SimulatedBus.UploadToDatabase();
                            }
                            else
                            {
                                Stoppested Stop = SimulatedBus.StoppeStederMTid[j].Stop;
                                Tidspunkt Time = SimulatedBus.StoppeStederMTid[j].AfPåTidComb.First().Tidspunkt;
                                SimulatedBus.PassengersTotal += RngPassPÅ;
                                SimulatedBus.PassengersTotal -= (RngPassAF-RngPassPÅ);
                                AfPåTidCombi NewAfPåTidComb = new AfPåTidCombi(0, SimulatedBus.PassengersTotal, Stop, SimulatedBus, UgeDag, Time, SimulatedBus.PassengersTotal, SimulatedBus.CapacitySitting + SimulatedBus.CapacityStanding);
                                NewAfPåTidComb.UploadToDatabase();
                                SimulatedBus.UploadToDatabase();
                            }
                            Debug.WriteLine("Stop:" + j);
                        }
                    }
                }
                else
                {
                    SimulatedBus.placering.xCoordinate = SimulatedRute.route.Points[i].Lat;
                    SimulatedBus.placering.yCoordinate = SimulatedRute.route.Points[i].Lng;
                    SimulatedBus.UploadToDatabase();

                }

            }
        }

        public void BusMovement()
        {
            int elementerIRute = SimulatedRute.route.Points.Count();
            double distanceBetweenPoints;
            double timeBetweenPoints;
            int timeBetweenPointsMilSec;


            int j = 0;


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

                    int Sleeptimer = timeBetweenPointsMilSec - (int)Tidstagning.ElapsedMilliseconds;
                    if (Sleeptimer > 0)
                    {
                        Thread.Sleep(Sleeptimer);
                    }

                    if (SimulatedBus.StoppeStederMTid.Count > j)
                    {
                        if (SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate < SimulatedRute.route.Points[i].Lat + 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.xCoordinate > SimulatedRute.route.Points[i].Lat - 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate < SimulatedRute.route.Points[i].Lng + 0.001 &&
                            SimulatedBus.StoppeStederMTid[j].Stop.StoppestedLok.yCoordinate > SimulatedRute.route.Points[i].Lng - 0.001)
                        {
                            j++;

                            try
                            {
                                SimulatedBus.PassengersTotal += RandomPassagerer();
                            }
                            catch (BusPassengersTotalUnderZeroException)
                            {

                            }
                            Debug.WriteLine("Stop:" + j);
                        }
                    }
                    else
                    {
                        //Debug.WriteLine("Alle stoppesteder er besøgt");
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

        private int RandomPassagerer()
        {
            Random rand = new Random();
            return rand.Next(-5, 10);
        }

        private Bus SendToServer()
        {
            Debug.WriteLine(SimulatedBus.busName + " : " + SimulatedBus.placering.xCoordinate + " : " + SimulatedBus.placering.yCoordinate + " : " + SimulatedBus.PassengersTotal);
            SimulatedBus.UploadToDatabase();
            return SimulatedBus;
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
