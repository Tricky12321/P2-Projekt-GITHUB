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
using Xamarin.Forms;


namespace AndroidApp
{
    class BusResultsAdapter : BaseAdapter<BusResultsCell>
    {
        BusResultsCell[] Busser;
        Activity context;
        public BusResultsAdapter(Activity context, BusResultsCell[] Busser) : base() {
            this.context = context;
            this.Busser = Busser;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override BusResultsCell this[int position]
        {
            get { return Busser[position]; }
        }
        public override int Count
        {
            get { return Busser.Length; }
        }
        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Android.Views.View view = convertView; // re-use an existing view, if one is available
            if (view == null) // otherwise create a new one
                view = context.LayoutInflater.Inflate(Resource.Layout.BusResultCellLayout, null);
            view.FindViewById<TextView>(Resource.Id.text1).Text = Busser[position].ToString();
            view.FindViewById<ImageView>(Resource.Id.KapacitetIkon).SetImageResource(Busser[position].KapacitetStatusBillede);

            return view;
        }
    }
}