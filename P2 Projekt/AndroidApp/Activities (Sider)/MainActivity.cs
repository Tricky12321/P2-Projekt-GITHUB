using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS; 

namespace AndroidApp
{
    [Activity(Label = "SmartBus", MainLauncher = true, Icon = "@drawable/SmartBusIcon1")]
    public class MainActivity : Activity, TimePickerDialog.IOnTimeSetListener
    {
        string StoppestedInputString;

        /* Rejsetidspunkt-variable */
        TextView ny_tid;
        int hours;
        int minutes;
        DateTime CurrentTime;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);

            /* Gør knapper til henholdsvis resultat-aktiviteten og favorit-aktiviteten klar */
            Button resultButton = FindViewById<Button>(Resource.Id.ResultButton);
            resultButton.Click += delegate { ShowResultsActivity(); };

            Button FavoriteButton = FindViewById<Button>(Resource.Id.FavButton);
            FavoriteButton.Click += delegate { ShowFavoritesActivity(); };

            /* Gør rejsetidspunkt-knapperne klar */
            ny_tid = FindViewById<TextView>(Resource.Id.nytid);
            ny_tid.SetTextSize(Android.Util.ComplexUnitType.Sp ,30);
            ny_tid.Click += delegate { ShowTimePickerDialog(); };

            /* Gør stoppested-inputtet klar og sætter det over i en string */
            AutoCompleteTextView StoppestedInputTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_stoppested);
            ArrayAdapter adapter = new ArrayAdapter(this, Resource.Layout.StoppestedLayout, GetStoppesteder());
            StoppestedInputTextView.Adapter = adapter;

            StoppestedInputTextView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) =>
            {
                StoppestedInputString = StoppestedInputTextView.Text;
            };

            /* Sætter rejsetidspunktet til nuværende tidspunkt */
            CurrentTime = DateTime.Now;
            hours = CurrentTime.Hour;
            minutes = CurrentTime.Minute;
            UpdateDisplay(hours, minutes);
        }

        private string[] GetStoppesteder()
        {
            RealClient NewRealClient = new RealClient();
            List<NetworkObject> StoppestederFraServer = NewRealClient.RequestAllWhere(ObjectTypes.BusStop,"");
            List<string> StoppeSteder = new List<string>();

            foreach (NetworkObject obj in StoppestederFraServer)
            {
                StoppeSteder.Add((obj as Stoppested).ToString());
            }
            return StoppeSteder.ToArray();
        }

        /* Rejsetidspunkt-metoder */
        private void ShowTimePickerDialog()
        {
            TimePicker dialog = new TimePicker(this, hours, minutes, this);
            dialog.Show(FragmentManager, null);
        }

        private void ShowResultsActivity()
        {
            List<string> stopOgTid = new List<string>() { StoppestedInputString, hours.ToString(), minutes.ToString() };
            Intent intent = new Intent(this, typeof(BusResults));
            intent.PutStringArrayListExtra("stopOgTid", stopOgTid);
            StartActivity(intent);
        }

        private void ShowFavoritesActivity()
        {
            Intent intent = new Intent(this, typeof(FavoriteBusses));
            StartActivity(intent);
        }

        public void OnTimeSet(Android.Widget.TimePicker view, int hourOfDay, int minute)
        {
            hours = hourOfDay;
            minutes = minute;
            UpdateDisplay(hourOfDay, minute);
        }

        private void UpdateDisplay(int selectedHours, int selectedMinutes)
        {
            // Sørger for at tid vises korrekt (00:00) (10:00) (12:54)
            ny_tid.Text = $"{selectedHours.ToString().PadLeft(2, '0')}:{selectedMinutes.ToString().PadLeft(2, '0')}";
        }
    }
}

