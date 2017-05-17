using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using JsonSerializer;
public static class JsonCache
{
    public static string AlleBusserCache
    {
        get { lock (BusserLock) { return _alleBusserCache; } }
        set { lock (BusserLock) { _alleBusserCache = value; } }
    }

    public static string AlleStoppeStederCache
    {
        get { lock (StoppeStederLock) { return _alleStoppeStederCache; } }
        set { lock (StoppeStederLock) { _alleStoppeStederCache = value; } }
    }

    public static string AlleRuterCache
    {
        get { lock (RuterLock) { return _alleRuterCache; } }
        set { lock (RuterLock) { _alleRuterCache = value; } }
    }

    private static string _alleBusserCache;

    private static string _alleStoppeStederCache;

    private static string _alleRuterCache;

    public static object BusserLock = new object() { };

    public static object StoppeStederLock = new object() { };

    public static object RuterLock = new object() { };

    private const int _sleepTime = 2500;

    public static void StartThreads()
    {
        Thread BusThread = new Thread(new ThreadStart(UpdateBusserCache));
        BusThread.Start();
        Thread StoppeThread = new Thread(new ThreadStart(UpdateStoppeStederCache));
        StoppeThread.Start();
        Thread RuteThread = new Thread(new ThreadStart(UpdateRuterCache));
        RuteThread.Start();
    }

    public static void UpdateRuterCache()
    {
        while (true)
        {
            string OutputString;
            var RowsFromDB = MysqlControls.SelectAll(new Rute().GetTableName(),true);
            List<Rute> AlleRuter = new List<Rute>();
            foreach (var SS in RowsFromDB.RowData)
            {
                Rute NewRute = new Rute();
                NewRute.Update(SS);
                AlleRuter.Add(NewRute);
            }
            OutputString = Json.Serialize(AlleRuter);
            lock (RuterLock)
            {
                AlleRuterCache = OutputString;
            }
            Debug.Print("Updated Rute Cache");
            Thread.Sleep(_sleepTime);
        }
    }

    public static void UpdateStoppeStederCache()
    {
        while (true)
        {
            string OutputString;
            var RowsFromDB = MysqlControls.SelectAll(new Stoppested().GetTableName(), true);
            List<Stoppested> AlleStoppesteder = new List<Stoppested>();
            foreach (var SS in RowsFromDB.RowData)
            {
                Stoppested NewStoppested = new Stoppested();
                NewStoppested.Update(SS);
                AlleStoppesteder.Add(NewStoppested);
            }
            OutputString = Json.Serialize(AlleStoppesteder);
            lock (StoppeStederLock)
            {
                AlleStoppeStederCache = OutputString;
            }
            Debug.Print("Updated StoppeSteds Cache");
            Thread.Sleep(_sleepTime);
        }
    }

    public static void UpdateBusserCache()
    {
        while (true)
        {
            string OutputString;
            var RowsFromDB = MysqlControls.SelectAll(new Bus().GetTableName(), true);
            List<Bus> AlleBusser = new List<Bus>();
            foreach (var SS in RowsFromDB.RowData)
            {
                Bus NewBus = new Bus();
                NewBus.Update(SS);
                AlleBusser.Add(NewBus);
            }
            OutputString = Json.Serialize(AlleBusser);
            lock (BusserLock)
            {
                AlleBusserCache = OutputString;
            }
            Debug.Print("Updated Busserm Cache");
            Thread.Sleep(_sleepTime);
        }

    }
}

