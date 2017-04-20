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
    public class BusResults : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Results);

            BusResultsCell [] busser = new BusResultsCell[]{ new BusResultsCell(0), new BusResultsCell(1), new BusResultsCell(2), new BusResultsCell(4)};

            //IList<string> busses = Intent.Extras.GetStringArrayList("busses") ?? new string[0];
            this.ListAdapter = new BusResultsAdapter(this, busser);
            
            
            
            /*Button backButton = FindViewById<Button>(Resource.Id.BackButton);
            backButton.Click += (object sender, EventArgs e) =>
            {
                this.OnBackPressed();
            };*/
        }
    }
}