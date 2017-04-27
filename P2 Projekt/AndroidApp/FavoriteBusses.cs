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
    public struct Favorite
    {
        public string Bus;
        public string Stoppested;
        public string Tid;

        public Favorite(string bus, string stoppested, string tid)
        {
            Bus = bus;
            Stoppested = stoppested;
            Tid = tid;
        }
    }
    [Activity(Label = "FavoriteBusses")]
    public class FavoriteBusses : Activity
    {
        public static List<Favorite> favoritListe = new List<Favorite>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Favorite);

            Button HomeButton = FindViewById<Button>(Resource.Id.HomeButton);
            HomeButton.Click += (object sender, EventArgs e) =>
            {
                this.OnBackPressed();
            };


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