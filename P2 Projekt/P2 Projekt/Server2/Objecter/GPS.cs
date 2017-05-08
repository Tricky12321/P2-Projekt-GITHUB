using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GPS
{
    public GPS(double x, double y)
    {
        xCoordinate = x;
        yCoordinate = y;
    }

    public GPS() { }

    public double xCoordinate;
    public double yCoordinate;
}
