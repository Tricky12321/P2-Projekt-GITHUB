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
        public BusResultsCell(Bus bus, Tidspunkt tidspunkt, string stoppested)
        {
            BusNavn = bus.busName;
            Tidspunkt = tidspunkt.ToString();
            PasNu = $"Nuværende passagerer: {bus.PassengersTotal} af {bus.CapacitySitting + bus.CapacityStanding}";
            PasForv = $"Forventede passagerer: {bus.StoppeStederMTid.Where(x => x.Stop.StoppestedName == stoppested).First().AfPåTidComb.First().ForventetPassagere} af {bus.CapacitySitting + bus.CapacityStanding}";
            Stoppested = stoppested;

            if (bus.PassengersTotal < bus.CapacitySitting * 0.8)
                KapacitetStatusBillede = Resource.Drawable.KapacitetStatusMasserafplads;
            else if ((bus.PassengersTotal >= bus.CapacitySitting * 0.8) && (bus.PassengersTotal < bus.CapacitySitting))
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