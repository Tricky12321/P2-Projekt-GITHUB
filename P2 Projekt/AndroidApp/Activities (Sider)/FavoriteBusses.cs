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
    public class FavoriteBusses : Activity
    {
        public static List<Favorite> favoritListe = new List<Favorite>();


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Favorite);

            Button HomeButton = FindViewById<Button>(Resource.Id.HomeButton);
            HomeButton.Click += (object sender, EventArgs e) => OnBackPressed();


            string[] favoritArray = MakeFavoriteStrings();

            ListView FavoritListe = FindViewById<ListView>(Resource.Id.FavoriteList);
            FavoritListe.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, favoritArray);

            FavoritListe.ItemClick += (object sender, AdapterView.ItemClickEventArgs args) =>
            {
                string[] arrayToSearch = ArrayToSearch(args);
                Intent intent = new Intent(this, typeof(BusResults));
                intent.PutStringArrayListExtra("stopOgTid", arrayToSearch);
                Toast.MakeText(this, $"Søger på stoppested {favoritListe[args.Position].Stoppested}", ToastLength.Short).Show();
                StartActivity(intent);
            };
        }

        protected override void OnResume()
        {
            base.OnResume();
            string[] favoritArray = MakeFavoriteStrings();
            ListView FavoritListe = FindViewById<ListView>(Resource.Id.FavoriteList);
            FavoritListe.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, favoritArray);
        }

        private string[] MakeFavoriteStrings()
        {
            int favListCount = favoritListe.Count;
            string[] favoritArray = new string[favListCount];

            for (int i = 0; i < favListCount; i++)
            {
                favoritArray[i] = $"Bus: {favoritListe[i].Bus} \nAnkommer ved: {favoritListe[i].Stoppested} \nTid: {favoritListe[i].Tid}";
            }

            return favoritArray;
        }

        private string[] ArrayToSearch(AdapterView.ItemClickEventArgs args)
        {
            Favorite favorit = favoritListe[args.Position];
            string[] splitTid = favorit.Tid.Split(':');
            return new string[] { favorit.Stoppested, splitTid[0], splitTid[1], favorit.Bus};
        }
    }
}