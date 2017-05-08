using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AndroidApp
{
    [Activity(Label = "SmartBus")]
    public class BusResults : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Results);
            

            IList<string> _stopOgTid = Intent.Extras.GetStringArrayList("stopOgTid") ?? new string[0];


            int IntervalStart, IntervalSlut, Tidspunkt;
            Interval(_stopOgTid[1], _stopOgTid[2], out IntervalStart, out IntervalSlut, out Tidspunkt);

            bool FiltrerEfterBusNavn;
            var busCelleListe = new List<BusResultsCell>();
            var ServerBusListe = new List<Bus>();
            var sorteretBusListe = new List<Bus>();


            // noget magi fra serveren med _stopOgTid
            // ServerBusListe = magi;


            if (_stopOgTid.Count == 4)
                FiltrerEfterBusNavn = true;
            else
                FiltrerEfterBusNavn = false;

            

            /* Først sorterer vi de busser fra, som ikke indeholder det stoppested vi søger efter.
             * Derefter sorterer vi de busser fra, som ikke holder ved stoppestedet inden for en halv time af det indtastede tidspunkt */
            sorteretBusListe = ServerBusListe.
                Where(bus => bus.busPassagerDataListe.
                Any(StopMTid => StopMTid.Stop.StoppestedName == _stopOgTid[0])).ToList();
            sorteretBusListe = sorteretBusListe.
                Where(bus => bus.busPassagerDataListe.
                Any(StopMTid => StopMTid.AfPåTidComb.
                Any(afpåtidcombi => AnkomstInterval(afpåtidcombi.Tidspunkt, IntervalStart, IntervalSlut)))).ToList();

            foreach (Bus bus in sorteretBusListe)
            {
                if (FiltrerEfterBusNavn)
                {
                    if (bus.busName == _stopOgTid[3])
                    {
                        busCelleListe.Add(new BusResultsCell(bus, Tidspunkt));
                    }
                }
                else
                {
                    busCelleListe.Add(new BusResultsCell(bus, Tidspunkt));
                }
            }


            //BusResultsCell[]busliste = new BusResultsCell[] { new BusResultsCell(0), new BusResultsCell(1), new BusResultsCell(2), new BusResultsCell(4) };
            
            Button HomeButton = FindViewById<Button>(Resource.Id.HomeButton);
            HomeButton.Click += (object sender, EventArgs e) =>
            {
                this.OnBackPressed();
            };

            ListView BusList = FindViewById<ListView>(Resource.Id.BusList);
            BusList.Adapter = new BusResultsAdapter(this, busCelleListe.ToArray<BusResultsCell>());

        }

        private bool AnkomstInterval(Tidspunkt tidspunkt, int IntervalStart, int IntervalSlut)
        {
            int Tidspunkt = (tidspunkt.hour * 60 * 60 + tidspunkt.minute * 60) - (30 * 60);
            if (Tidspunkt > IntervalSlut && Tidspunkt < IntervalSlut)
            {
                return true;
            }
            else return false;
        }

        private void Interval (string timers, string minutters,  out int IntervalStart, out int IntervalSlut, out int Tidspunkt)
        {
            int timer = Convert.ToInt32(timers);
            int minutter = Convert.ToInt32(minutters);
            Tidspunkt = (timer * 60 * 60 + minutter * 60);
            IntervalStart = Tidspunkt - (30 * 60);
            IntervalSlut = Tidspunkt + (30 * 60);
        }
    }
}