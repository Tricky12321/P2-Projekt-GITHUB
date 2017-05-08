﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTilBusselskab
{
    public class Rute
    {
        public List<Stoppested> AfPåRuteList = new List<Stoppested>();
        public string RuteName;
        public int RuteID;

        public Rute(string ruteName, int ruteID, params Stoppested[] stoppested)
        {
            RuteName = ruteName;
            RuteID = ruteID;
            foreach (Stoppested stop in stoppested)
            {
                AfPåRuteList.Add(stop);
            }
        }

        public override string ToString()
        {
            return RuteName;
        }
    }
}