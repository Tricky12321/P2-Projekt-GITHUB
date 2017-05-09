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

        public double busAvgSpeedMprSec;

        public SimBus(Bus simulatedBus)
        {
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
            Thread BusMovementThread = new Thread(new ThreadStart(BusMovement));
            BusMovementThread.Start();
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
