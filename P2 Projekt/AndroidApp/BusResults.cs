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
    [Activity(Label = "BusResults")]
    public class BusResults : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Results);

            Button backButton = FindViewById<Button>(Resource.Id.BackButton);

            backButton.Click += (object sender, EventArgs e) =>
            {
                StartActivity(typeof(MainActivity));
                
            };
        }
    }
}