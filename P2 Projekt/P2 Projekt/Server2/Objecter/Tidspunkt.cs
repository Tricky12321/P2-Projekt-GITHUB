﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tidspunkt
{
    public int hour;
    public int minute;

    public Tidspunkt(int hour, int minute)
    {
        this.hour = hour;
        this.minute = minute;
    }

    public Tidspunkt() { }

    public override string ToString()
    {
        return hour.ToString().PadLeft(2, '0') + " : " + minute.ToString().PadLeft(2, '0');
    }

    public string SinpleString()
    {
        return hour.ToString().PadLeft(2, '0') + ":" + minute.ToString().PadLeft(2, '0');
    }

    public Tidspunkt(string Tid)
    {
        string[] tidarr = Tid.Split(':');
        hour = Convert.ToInt32(tidarr[0]);
        minute = Convert.ToInt32(tidarr[1]);
    }
}

