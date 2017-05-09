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
        public BusResultsCell (Bus bus, Tidspunkt tidspunkt, string stoppested/* int Vælger*/)
        {
            /*
            BusID = "25A - Ferslev";
            Tidspunkt = "13:01";
            PasNu = "Pas. nu: 25/60";
            PasForv = "Pas. Forv.: 40/60";
            Stoppested = "Jomfru Ane Gade";

            if (Vælger == 0)
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusMasserafplads;
            else if (Vælger == 1)
            {
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusNaesteningensiddepladser;
                BusID = "571X - En by i Rusland";
                Tidspunkt = "13:05";
            }
            else if (Vælger == 2)
            {
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusIngenSiddepladser;
                BusID = "5 - Universitetet";
                Tidspunkt = "13:10";
            }
            else
            {
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusOverfyldt;
                BusID = "12 - Gug Øst";
                Tidspunkt = "13:16";
            }
            */
            
            BusNavn = bus.busName;
            Tidspunkt = tidspunkt.hour.ToString() + ':' + tidspunkt.minute.ToString();
            PasNu = $"Nuværende passagerer: {bus.PassengersTotal} af {bus.CapacitySitting + bus.CapacityStanding}";
            PasForv = $"Forventede passagerer: {"10"} af {bus.CapacitySitting + bus.CapacityStanding}";
            Stoppested = stoppested;

            if (bus.PassengersTotal < bus.CapacitySitting*0.8)
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusMasserafplads;
            else if ((bus.PassengersTotal >= bus.CapacitySitting*0.8) && (bus.PassengersTotal < bus.CapacitySitting))
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusNaesteningensiddepladser;
            else if ((bus.PassengersTotal >= bus.CapacitySitting) && (bus.PassengersTotal < bus.CapacitySitting + bus.CapacityStanding))
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusIngenSiddepladser;
            else
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusOverfyldt;
        }
        public string BusNavn;
        public string Tidspunkt;
        public string PasNu;
        public string PasForv;
        public string Stoppested;
        public int KapacitetStatusBillede;

        public override string ToString()
        {
            return BusNavn + " " + Tidspunkt + "\n" + PasNu + "\n" + PasForv;
        }
    }
}