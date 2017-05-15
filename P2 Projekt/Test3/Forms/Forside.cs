using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProgramTilBusselskab;
using System.Diagnostics;
using System.Threading;
namespace ProgramTilBusselskab
{
    public partial class Simulation : Form
    {
        Bus valgtBus;

        public Simulation()
        {
            InitializeComponent();
        }

        private void gMapsMap_Load(object sender, EventArgs e)
        {
            gMapsMap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            gMapsMap.SetPositionByKeywords("Aalborg, Denmark");
        }

        private void btn_opretStoppested_Click(object sender, EventArgs e)
        {
            OpretStoppested stoppestedForm = new OpretStoppested();
            stoppestedForm.Show();
        }

        private void btn_opretBus_Click(object sender, EventArgs e)
        {
            OpretBus busForm = new OpretBus();
            busForm.Show();
        }

        private void btn_opretRute_Click(object sender, EventArgs e)
        {
            OpretRute ruteForm = new OpretRute();
            ruteForm.Show();
        }

        private void Simulation_Load(object sender, EventArgs e)
        {
            RealClient BusClient = new RealClient();
            int downloadedBusses = 0;
            List<NetworkObject> DatabaseBus;
            DatabaseBus = BusClient.RequestAllWhere(ObjectTypes.Bus, "None");
            foreach (var item in DatabaseBus)
            {
                Lists.listWithBusses.Add(item as Bus);
                downloadedBusses++;
            }


            int downloadedRoutes = 0;
            RealClient RuteClient = new RealClient();
            List<NetworkObject> AlleRuter = RuteClient.RequestAllWhere(ObjectTypes.Rute, "None");

            foreach (var item in AlleRuter)
            {
                Lists.listWithRoutes.Add((item as Rute));
                downloadedRoutes++;
            }

            RealClient StoppestederClient = new RealClient();
            int downloadedStops = 0;
            List<NetworkObject> Stoppesteder = StoppestederClient.RequestAllWhere(ObjectTypes.BusStop, "None");

            foreach (var item in Stoppesteder)
            {
                Lists.listWithStops.Add(item as Stoppested);
                downloadedStops++;
            }
            MessageBox.Show($"Der er blevet hentet: \nBus(ser): {downloadedBusses} \nRute(r): {downloadedRoutes} \nStoppested(er): {downloadedStops} \nHentet fra databasen");

            btn_refresh.PerformClick();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            combox_ruterTilVisning.Items.Clear();
            foreach (Rute rute in Lists.listWithRoutes)
            {
                combox_ruterTilVisning.Items.Add(rute);
            }

            combox_vælgBus.Items.Clear();
            foreach (Bus bus in Lists.listWithBusses)
            {
                combox_vælgBus.Items.Add(bus);
            }
        }

        private void btn_visPåKort_Click(object sender, EventArgs e)
        {
            if (combox_ruterTilVisning.SelectedItem != null)
            {
                GMapOverlay routesOverlay = new GMapOverlay("routeLayer");
                SimRoute placeholderRute = new SimRoute((Rute)combox_ruterTilVisning.SelectedItem, "Placeholder Rute");
                routesOverlay.Routes.Add(placeholderRute.route);
                gMapsMap.Overlays.Add(routesOverlay);

                if (chkbox_toggleStoppesteder.Checked == true)
                {
                    GMapOverlay stopLayer = new GMapOverlay("Stoplayer");

                    foreach (Stoppested stop in ((Rute)combox_ruterTilVisning.SelectedItem).StoppeSteder)
                    {
                        GMapMarker stoppested =
                        new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                        new PointLatLng(stop.StoppestedLok.xCoordinate, stop.StoppestedLok.yCoordinate),
                        Test3.Properties.Resources.busSkilt);
                        stopLayer.Markers.Add(stoppested);

                        //Indsætter tekst med antal passagerer
                        stoppested.ToolTipText = stop.StoppestedName;

                        //Bestemmer farverne på tekstbobble
                        stoppested.ToolTip.Fill = Brushes.White;
                        stoppested.ToolTip.Foreground = Brushes.Black;
                    }
                    gMapsMap.Overlays.Add(stopLayer);
                }
                gMapsMap.ZoomAndCenterRoute(placeholderRute.route);
            }
            else
            {
                MessageBox.Show("Rute ikke valgt.");
            }
        }

        private void btn_clearMap_Click(object sender, EventArgs e)
        {
            timer_UpdateMap.Enabled = false;
            ClearMap();
        }

        private void ClearMap()
        {
            gMapsMap.Overlays.Clear();
            gMapsMap.ReloadMap();
        }

        private void btn_visOprettede_Click(object sender, EventArgs e)
        {
            OprettedeObjekter oprettedeObjekter = new OprettedeObjekter();
            oprettedeObjekter.Show();
        }

        private void btn_visBusPåRute_Click(object sender, EventArgs e)
        {
            valgtBus = (Bus)combox_vælgBus.SelectedItem;
            timer_UpdateMap.Enabled = true;
            ShowBus(valgtBus);
        }

        private void timer_UpdateMap_Tick(object sender, EventArgs e)
        {
            gMapsMap.Overlays.Clear();
            RealClient Client = new RealClient();

            try
            {
                valgtBus = Client.RequestAllWhere(ObjectTypes.Bus, $"`ID`={valgtBus.BusID}").First() as Bus;
            }
            catch (Exception)
            {
                //MessageBox.Show("Bussen kunne ikke hentes fra serveren");
            }

            ShowBus(valgtBus);
        }

        private Bus GetBusFromServer(int ID)
        {
            RealClient Client = new RealClient();
            List<NetworkObject> ListOfObjs = Client.RequestAllWhere(ObjectTypes.Bus, $"`ID`={ID.ToString()}");
            return (ListOfObjs[0] as Bus);
        }

        private void ShowBus(Bus bus)
        {
            gMapsMap.Overlays.Clear();
            try
            {
                GMapOverlay busOverlay = new GMapOverlay("busLayer");
                GMapMarker busMark = new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                    new PointLatLng(bus.placering.xCoordinate, bus.placering.yCoordinate),
                    Test3.Properties.Resources.bus);

                busMark.ToolTipText = bus.PassengersTotal.ToString() + " af " + (bus.CapacitySitting + bus.CapacityStanding) + "\nStåpladser: " + bus.CapacityStanding + " og siddeplader " + bus.CapacitySitting;
                busMark.ToolTip.Foreground = Brushes.Black;

                if ((bus.CapacitySitting + bus.CapacityStanding) * 0.8 < bus.PassengersTotal)
                {
                    busMark.ToolTip.Fill = Brushes.Red;
                }
                else if ((bus.CapacitySitting + bus.CapacityStanding) * 0.6 < bus.PassengersTotal)
                {
                    busMark.ToolTip.Fill = Brushes.Yellow;
                }
                else
                {
                    busMark.ToolTip.Fill = Brushes.Green;
                }

                busOverlay.Markers.Add(busMark);

                if (chkbox_medRute.Checked)
                {
                    GMapOverlay routesOverlay = new GMapOverlay("routeLayer");
                    SimRoute placeholderRute = new SimRoute(bus.Rute, "Placeholder Rute");
                    routesOverlay.Routes.Add(placeholderRute.route);
                    gMapsMap.Overlays.Add(routesOverlay);

                    GMapOverlay stopLayer = new GMapOverlay("Stoplayer");

                    bool warningRed = false;
                    bool warningYellow = false;

                    foreach (Stoppested stop in bus.Rute.StoppeSteder)
                    {
                        GMapMarker stoppested =
                        new GMap.NET.WindowsForms.Markers.GMarkerGoogle(
                        new PointLatLng(stop.StoppestedLok.xCoordinate, stop.StoppestedLok.yCoordinate),
                        Test3.Properties.Resources.busSkilt);
                        stopLayer.Markers.Add(stoppested);



                        //Indsætter tekst med antal passagerer
                        stoppested.ToolTipText = stop.StoppestedName + " " + bus.StoppeStederMTid.Where(x => x.Stop.StoppestedID == stop.StoppestedID).First().AfPåTidComb.First().Tidspunkt.ToString() + "\nForventede passagerer " + bus.StoppeStederMTid.Where(x => x.Stop.StoppestedID == stop.StoppestedID).First().AfPåTidComb.First().ForventetPassagere;

                        if ((bus.CapacitySitting + bus.CapacityStanding) < bus.StoppeStederMTid.Where(x => x.Stop.StoppestedID == stop.StoppestedID).First().AfPåTidComb.First().ForventetPassagere)
                        {
                            stoppested.ToolTip.Fill = Brushes.Red;
                            warningRed = true;
                        }
                        else if ((bus.CapacitySitting + bus.CapacityStanding) * 0.8 < bus.StoppeStederMTid.Where(x => x.Stop.StoppestedID == stop.StoppestedID).First().AfPåTidComb.First().ForventetPassagere)
                        {
                            stoppested.ToolTip.Fill = Brushes.Yellow;
                            warningYellow = true;

                        }
                        else
                        {
                            stoppested.ToolTip.Fill = Brushes.Green;

                        }

                        //Bestemmer farverne på tekstbobble
                        stoppested.ToolTip.Foreground = Brushes.Black;
                    }

                    if (warningRed)
                    {
                        MessageBox.Show("Bussen forventes at blive fuld senere på ruten!");
                    }
                    else if (warningYellow)
                    {
                        MessageBox.Show("Bussen forventes at blive næsten fuld senere på ruten!");
                    }

                    gMapsMap.Overlays.Add(stopLayer);
                    gMapsMap.Overlays.Add(busOverlay);
                    gMapsMap.ZoomAndCenterRoute(placeholderRute.route);
                }
                else
                {
                    gMapsMap.Overlays.Add(busOverlay);
                    gMapsMap.ZoomAndCenterMarkers("busLayer");
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Bussen har endnu ikke modtaget koordinater.");
            }
        }
    }
}

