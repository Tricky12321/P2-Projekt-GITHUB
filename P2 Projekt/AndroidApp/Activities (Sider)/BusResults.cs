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

            bool FiltrerEfterBusNavn;
            //var busliste = new List<BusResultsCell>();
            //var ServerBusListe = new List<Bus>();
            // noget magi fra serveren med _stopOgTid
            // ServerBusListe = magi;

            if (_stopOgTid.Count == 4)
                FiltrerEfterBusNavn = true;

            /* foreach (Bus bus in ServerBusListe)
             * {
             *     if (FiltrerEfterBusNavn)
             *     {
             *         if (bus.BusName == _stopOgTid[3])
             *             {
             *                 busliste.Add(new BusResultsCell(bus));
             *             }  
             *     }
             *     else
             *     {
             *     busliste.Add(new BusResultsCell(bus));
             *     }
             * } */


            BusResultsCell[]busliste = new BusResultsCell[] { new BusResultsCell(0), new BusResultsCell(1), new BusResultsCell(2), new BusResultsCell(4) };
            
            Button HomeButton = FindViewById<Button>(Resource.Id.HomeButton);
            HomeButton.Click += (object sender, EventArgs e) =>
            {
                this.OnBackPressed();
            };

            ListView BusList = FindViewById<ListView>(Resource.Id.BusList);
            BusList.Adapter = new BusResultsAdapter(this, busliste.ToArray<BusResultsCell>());

        }
    }
}