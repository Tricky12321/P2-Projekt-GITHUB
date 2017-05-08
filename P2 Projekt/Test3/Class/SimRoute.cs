using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;

public class SimRoute
{
    public GMapRoute route;

    public SimRoute(Rute rute, string routeName)
    {
        GDirections placeholder;
        List<PointLatLng> direction = new List<PointLatLng>();
        PointLatLng start = new PointLatLng();
        PointLatLng end = new PointLatLng();

        int listLenght = rute.AfPåRuteListMTid.Count() - 1;

        for (int i = 0; i < listLenght; i++)
        {
            start.Lat = rute.AfPåRuteListMTid[i].Stop.StoppestedLok.xCoordinate;
            start.Lng = rute.AfPåRuteListMTid[i].Stop.StoppestedLok.yCoordinate;

            end.Lat = rute.AfPåRuteListMTid[i + 1].Stop.StoppestedLok.xCoordinate;
            end.Lng = rute.AfPåRuteListMTid[i + 1].Stop.StoppestedLok.yCoordinate;

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
                throw new InvalidCastException("Ukendt punkt");
            }
        }
        route = new GMapRoute(direction, $"{routeName}");
    }
}
