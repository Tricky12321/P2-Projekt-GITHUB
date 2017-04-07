using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algoritme
{
    public struct coordinate
    {
        public coordinate (double x, double y)
        {
            xCoordinate = x;
            yCoordinate = y;
        }

        public double xCoordinate;
        public double yCoordinate;
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateTime[] tidspunkterID2 = {new DateTime(2017, 04, 07, 10, 18, 00),
                                         new DateTime(2017, 04, 07, 11, 18, 00),
                                         new DateTime(2017, 04, 07, 12, 18, 00),
                                         new DateTime(2017, 04, 07, 13, 18, 00),
                                         new DateTime(2017, 04, 07, 14, 18, 00)};



            Stoppested ID2 = new Stoppested(2, new coordinate(3.333, 4.4444), tidspunkterID2);

            Stoppested ID12 = new Stoppested(12, new coordinate(5.5555, 6.6666), new DateTime(2017, 04, 07, 10, 21, 00),
                                                                                 new DateTime(2017, 04, 07, 11, 21, 00),
                                                                                 new DateTime(2017, 04, 07, 12, 21, 00),
                                                                                 new DateTime(2017, 04, 07, 13, 21, 00),
                                                                                 new DateTime(2017, 04, 07, 14, 21, 00));

            Stoppested ID17 = new Stoppested(17, new coordinate(7.7777, 8.8888), new DateTime(2017, 04, 07, 10, 28, 00),
                                                                                 new DateTime(2017, 04, 07, 11, 28, 00),
                                                                                 new DateTime(2017, 04, 07, 12, 28, 00),
                                                                                 new DateTime(2017, 04, 07, 13, 28, 00),
                                                                                 new DateTime(2017, 04, 07, 14, 28, 00));

            Rute rute2 = new Rute(2, ID2, ID12, ID17);

            ID2.UpdatePassengers(5, 10);
            ID2.UpdatePassengers(43, 123);
            ID2.UpdatePassengers(56, 1123);

            ID12.UpdatePassengers(5, 10);
            ID12.UpdatePassengers(43, 123);
            ID12.UpdatePassengers(56, 1123);



            foreach (Stoppested stop in rute2.AfPåRuteList)
            {
                Console.WriteLine("\n" + stop.stoppestedID);

                foreach (DataAfPåstigning tid in stop.AfPåstigningerList)
                {
                    Console.WriteLine(tid.tid.ToString());
                    Console.WriteLine(tid.afstigninger);
                    Console.WriteLine(tid.påstigninger);
                }
            }

            Console.ReadKey();

        }
    }
}
