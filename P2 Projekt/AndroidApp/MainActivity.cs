using System;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS; 

namespace AndroidApp
{
    [Activity(Label = "SmartBus", MainLauncher = true, Icon = "@drawable/icon")]
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
            Button resultButton = FindViewById<Button>(Resource.Id.ResultButton);

            ny_tid_knap.Click += delegate { ShowTimePickerDialog(); };

            resultButton.Click += (object sender, EventArgs e) =>
            {
                var intent = new Intent(this, typeof(BusResults));
                StartActivity(intent);
            };
            
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

            /* Gør rejsetidspunkt-knapperne klar */
            ny_tid = FindViewById<TextView>(Resource.Id.nytid);
            Button ny_tid_knap = FindViewById<Button>(Resource.Id.nytid_knap);
            ny_tid_knap.Click += delegate { ShowTimePickerDialog(); };



            /* Sætter rejsetidspunktet til nuværende tidspunkt */
            CurrentTime = DateTime.Now;
            hour = CurrentTime.Hour;
            minutes = CurrentTime.Minute;

            UpdateDisplay(hour, minutes);
        }
        /* Rejsetidspunkt-variable */
        TextView ny_tid;
        int hour;
        int minutes;
        DateTime CurrentTime;

        /* Rejsetidspunkt-metoder */
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
            if (selectedHours < 10 || selectedMinutes < 10)
            {
                if (selectedHours < 10 && selectedMinutes < 10)
                    ny_tid.Text = "0" + selectedHours + ":" + "0" + selectedMinutes;
                else if (selectedHours < 10)
                    ny_tid.Text = "0" + selectedHours + ":" + selectedMinutes;
                else
                    ny_tid.Text = selectedHours + ":" + "0" + selectedMinutes;
            }
            else
                ny_tid.Text = selectedHours + ":" + selectedMinutes;
        }
    }
}

