using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace AndroidApp
{
    [Activity(Label = "LasseogAnton", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            TextView FuckDigTekst = FindViewById<TextView>(Resource.Id.Fuckdigtekst);
            Button KnapÆndrer = FindViewById<Button>(Resource.Id.KnapAendrer);
            KnapÆndrer.Click += (object sender, EventArgs e) =>
            {
                try
                {
                    Client Test = new Client();
                    Test.SendTestObject();
                    FuckDigTekst.Text = "ANTON ER IKKE MADS, SÅ DERFOR SEJ!";
                }
                catch (Exception)
                {

                    throw;
                }
                
            };

        }
    }
}

