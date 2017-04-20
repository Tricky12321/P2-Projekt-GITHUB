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
    class BusResultsCell
    {
        /* Skal tage imod et bus objekt + hvad den ellers har brug for, og omdanne det til strings,
         * som BusResultsAdapter kan vise */
        public BusResultsCell (int Vælger)
        {
            BusID = "5A - Ferslev ";
            Tidspunkt = "Kl 12:45";
            PasNu = "Pas. nu: 13/40";
            PasForv = "Pas. Forv.: 26/40";
            if (Vælger == 0)
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusMasserafplads;
            else if (Vælger == 1)
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusNaesteningensiddepladser;
            else if (Vælger == 2)
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusIngenSiddepladser;
            else
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusOverfyldt;
        }
        public string BusID;
        public string Tidspunkt;
        public string PasNu;
        public string PasForv;
        public int KapacitetStatusBillede;
}
}