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
    public struct Favorit
    {
        public string Bus;
        public string Stoppested;
        public string Tid;

        public Favorit(string bus, string stoppested, string tid)
        {
            Bus = bus;
            Stoppested = stoppested;
            Tid = tid;
        }
    }

    [Activity(Label = "FavoriteBusses")]
    public class FavoriteBusses : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Favorite);

            Button HomeButton = FindViewById<Button>(Resource.Id.HomeButton);
            HomeButton.Click += (object sender, EventArgs e) =>
            {
                this.OnBackPressed();
            };

            var favoritListe = new List<Favorit>();
            favoritListe.Add(new Favorit("25A", "Ferslev", "13:00"));
            favoritListe.Add(new Favorit("2A", "Jomfru Ane Gade", "14:45"));

            var favoritArray = new string[favoritListe.Count];

            for (int i = 0; i < favoritListe.Count; i++)
            {
                 favoritArray[i] = $"Bus: {favoritListe[i].Bus} \nAnkommer ved: {favoritListe[i].Stoppested} \nTid: {favoritListe[i].Tid}";
            }

            ListView FavoritListe = FindViewById<ListView>(Resource.Id.FavoriteList);
            FavoritListe.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, favoritArray);
        }
    }
}