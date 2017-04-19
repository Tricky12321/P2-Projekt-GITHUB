using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;

namespace AndroidApp
{
    [Activity(Label = "LasseogAnton", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, TimePickerDialog.IOnTimeSetListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            TextView FuckDigTekst = FindViewById<TextView>(Resource.Id.Fuckdigtekst);
            Button KnapÆndrer = FindViewById<Button>(Resource.Id.KnapAendrer);
            TimePicker timepicker = FindViewById<TimePicker>(Resource.Id.timePicker1);
            ny_tid = FindViewById<TextView>(Resource.Id.nytid);
            Button ny_tid_knap = FindViewById<Button>(Resource.Id.nytid_knap);

            ny_tid_knap.Click += delegate { ShowTimePickerDialog(); };
            
            KnapÆndrer.Click += (object sender, EventArgs e) =>
            {
                Client Test = new Client();
                string response = Test.SendTestObject();
                KnapÆndrer.Text = Test.Host;
                if (response == "1")
                {
                    FuckDigTekst.Text = $"SUCCESS: {response}";
                    Console.WriteLine("Test");
                }
                else
                {
                    FuckDigTekst.Text = $"FAILED: {response}\n";
                }

            };

            hour = 2;
            minutes = 25;

            UpdateDisplay(hour, minutes);
        }
        TextView ny_tid;
        int hour;
        int minutes;

        void ShowTimePickerDialog()
        {
            var dialog = new TimePickerFragment(this, hour, minutes, this);
            dialog.Show(FragmentManager, null);
        }

        public void OnTimeSet(TimePicker view, int hourOfDay, int minute)
        {
            UpdateDisplay(hourOfDay, minute);
        }

        void UpdateDisplay(int selectedHours, int selectedMinutes)
        {
            ny_tid.Text = selectedHours + ":" + selectedMinutes;
        }
    }
}

