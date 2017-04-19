using System;
using System.Collections.Generic;
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
            Button resultButton = FindViewById<Button>(Resource.Id.ResultButton);
            resultButton.Click += delegate { ShowResults(); };
            
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
            ny_tid.SetTextSize(Android.Util.ComplexUnitType.Sp ,30);
            ny_tid.Click += delegate { ShowTimePickerDialog(); };

            EditText StoppestedInput = FindViewById<EditText>(Resource.Id.StoppestedInput);
            StoppestedInput.ClearFocus();
            StoppestedInput.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) =>
            {
                StoppestedInputString = StoppestedInput.Text;
            };
            
            /* Sætter rejsetidspunktet til nuværende tidspunkt */
            CurrentTime = DateTime.Now;
            hours = CurrentTime.Hour;
            minutes = CurrentTime.Minute;

            UpdateDisplay(hours, minutes);
        }
        /* Rejsetidspunkt-variable */
        TextView ny_tid;
        int hours;
        int minutes;
        string StoppestedInputString;
        DateTime CurrentTime;
        static List<string> busses = new List<string>() { "Hej", "med", "dig" };

        /* Rejsetidspunkt-metoder */
        void ShowTimePickerDialog()
        {
            var dialog = new TimePickerFragment(this, hours, minutes, this);
            dialog.Show(FragmentManager, null);
        }

        void ShowResults()
        {
            var intent = new Intent(this, typeof(BusResults));
            intent.PutStringArrayListExtra("busses", busses);
            StartActivity(intent);
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

