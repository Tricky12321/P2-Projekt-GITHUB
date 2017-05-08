using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace ProgramTilBusselskab
{
    public class SimRoute
    {
        public GMapRoute route;

        public SimRoute(Rute rute, string routeName)
        {
            GDirections placeholder;
            List<PointLatLng> direction = new List<PointLatLng>();
            PointLatLng start = new PointLatLng();
            PointLatLng end = new PointLatLng();

            int listLenght = rute.AfPåRuteList.Count() - 1;

            for (int i = 0; i < listLenght; i++)
            {
                start.Lat = rute.AfPåRuteList[i].StoppestedLok.xCoordinate;
                start.Lng = rute.AfPåRuteList[i].StoppestedLok.yCoordinate;

                end.Lat = rute.AfPåRuteList[i + 1].StoppestedLok.xCoordinate;
                end.Lng = rute.AfPåRuteList[i + 1].StoppestedLok.yCoordinate;

                GMap.NET.MapProviders.GoogleMapProvider.Instance.GetDirections(out placeholder, start, end, false, true, true, false, true);

                if (placeholder != null)
                {
                    foreach (PointLatLng point in placeholder.Route)
                    {
                        direction.Add(point);
                    }
                }
                else
                {
                    throw new Exception("Ukendt punkt");
                }
            }
            route = new GMapRoute(direction, $"{routeName}");
        }
    }
}
