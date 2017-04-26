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

            /* Gør stoppested-inputtet klar og sætter det over i en string */
            AutoCompleteTextView StoppestedInputTextView = FindViewById<AutoCompleteTextView>(Resource.Id.autocomplete_stoppested);
            var adapter = new ArrayAdapter<String>(this, Resource.Layout.StoppestedLayout, COUNTRIES);
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


        string StoppestedInputString;

        static string[] COUNTRIES = new string[] {
  "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra",
  "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina",
  "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan",
  "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium",
  "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia",
  "Bosnia and Herzegovina", "Botswana", "Bouvet Island", "Brazil", "British Indian Ocean Territory",
  "British Virgin Islands", "Brunei", "Bulgaria", "Burkina Faso", "Burundi",
  "Cote d'Ivoire", "Cambodia", "Cameroon", "Canada", "Cape Verde",
  "Cayman Islands", "Central African Republic", "Chad", "Chile", "China",
  "Christmas Island", "Cocos (Keeling) Islands", "Colombia", "Comoros", "Congo",
  "Cook Islands", "Costa Rica", "Croatia", "Cuba", "Cyprus", "Czech Republic",
  "Democratic Republic of the Congo", "Denmark", "Djibouti", "Dominica", "Dominican Republic",
  "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea",
  "Estonia", "Ethiopia", "Faeroe Islands", "Falkland Islands", "Fiji", "Finland",
  "Former Yugoslav Republic of Macedonia", "France", "French Guiana", "French Polynesia",
  "French Southern Territories", "Gabon", "Georgia", "Germany", "Ghana", "Gibraltar",
  "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau",
  "Guyana", "Haiti", "Heard Island and McDonald Islands", "Honduras", "Hong Kong", "Hungary",
  "Iceland", "India", "Indonesia", "Iran", "Iraq", "Ireland", "Israel", "Italy", "Jamaica",
  "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Kuwait", "Kyrgyzstan", "Laos",
  "Latvia", "Lebanon", "Lesotho", "Liberia", "Libya", "Liechtenstein", "Lithuania", "Luxembourg",
  "Macau", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands",
  "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia", "Moldova",
  "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia",
  "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand",
  "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "North Korea", "Northern Marianas",
  "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru",
  "Philippines", "Pitcairn Islands", "Poland", "Portugal", "Puerto Rico", "Qatar",
  "Reunion", "Romania", "Russia", "Rwanda", "Sqo Tome and Principe", "Saint Helena",
  "Saint Kitts and Nevis", "Saint Lucia", "Saint Pierre and Miquelon",
  "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Saudi Arabia", "Senegal",
  "Seychelles", "Sierra Leone", "Singapore", "Slovakia", "Slovenia", "Solomon Islands",
  "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "South Korea",
  "Spain", "Sri Lanka", "Sudan", "Suriname", "Svalbard and Jan Mayen", "Swaziland", "Sweden",
  "Switzerland", "Syria", "Taiwan", "Tajikistan", "Tanzania", "Thailand", "The Bahamas",
  "The Gambia", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey",
  "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Virgin Islands", "Uganda",
  "Ukraine", "United Arab Emirates", "United Kingdom",
  "United States", "United States Minor Outlying Islands", "Uruguay", "Uzbekistan",
  "Vanuatu", "Vatican City", "Venezuela", "Vietnam", "Wallis and Futuna", "Western Sahara",
  "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"
};

        /* Rejsetidspunkt-variable */
        TextView ny_tid;
        int hours;
        int minutes;
        DateTime CurrentTime;
        
        /* Rejsetidspunkt-metoder */
        void ShowTimePickerDialog()
        {
            var dialog = new TimePicker(this, hours, minutes, this);
            dialog.Show(FragmentManager, null);
        }

        void ShowResults()
        {
            List<string> stopOgTid = new List<string>() { StoppestedInputString, hours.ToString(), minutes.ToString() };
            var intent = new Intent(this, typeof(BusResults));
            intent.PutStringArrayListExtra("stopOgTid", stopOgTid);
            StartActivity(intent);
        }

        public void OnTimeSet(Android.Widget.TimePicker view, int hourOfDay, int minute)
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

