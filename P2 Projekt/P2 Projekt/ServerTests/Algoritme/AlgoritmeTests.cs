using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture()]
    public class AlgoritmeTests
    {
        [Test()]
        public void AlgoritmenTest()
        {
            
            Assert.AreEqual(3, enfuckingfunktion());

        }

        public int enfuckingfunktion()
        {
            Rute TestRute;

            // Stoppesteder til rute 2 med Storvorde //
            Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 15)), new BusTidCombi(bus3, new Tidspunkt(12, 15))*/);
            Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441)                                      /* , new BusTidCombi(bus2, new Tidspunkt(11, 15)), new BusTidCombi(bus3, new Tidspunkt(12, 15))*/);
            Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364)                                    /* , new BusTidCombi(bus2, new Tidspunkt(11, 16)), new BusTidCombi(bus3, new Tidspunkt(12, 16))*/);
            Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831)                                             /* , new BusTidCombi(bus2, new Tidspunkt(11, 16)), new BusTidCombi(bus3, new Tidspunkt(12, 16))*/);
            Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901)                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 17)), new BusTidCombi(bus3, new Tidspunkt(12, 17))*/);
            Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311)                                               /* , new BusTidCombi(bus2, new Tidspunkt(11, 18)), new BusTidCombi(bus3, new Tidspunkt(12, 18))*/);

            TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                                stopFriluftsbadet,
                                                                                stopStenBillesGade,
                                                                                stopHaradlslund,
                                                                                stopSchleppegrellsgade,
                                                                                stopSctJoseph);

            Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute,
                (new StoppestedMTid(TestRute.StoppeSteder[0], new AfPåTidCombi())),
                (new StoppestedMTid(TestRute.StoppeSteder[1], new AfPåTidCombi())),
                (new StoppestedMTid(TestRute.StoppeSteder[2], new AfPåTidCombi())),
                (new StoppestedMTid(TestRute.StoppeSteder[3], new AfPåTidCombi())),
                (new StoppestedMTid(TestRute.StoppeSteder[4], new AfPåTidCombi())),
                (new StoppestedMTid(TestRute.StoppeSteder[5], new AfPåTidCombi())));

            /* Historik til alle 6 stoppesteder */
            TestBus.StoppeStederMTid[0].AfPåTidComb[0] = (new AfPåTidCombi(0, 3, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30));
            TestBus.StoppeStederMTid[1].AfPåTidComb[0] = (new AfPåTidCombi(0, 4, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 10), 4, 30));
            TestBus.StoppeStederMTid[2].AfPåTidComb[0] = (new AfPåTidCombi(0, 5, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 20), 6, 30));
            TestBus.StoppeStederMTid[3].AfPåTidComb[0] = (new AfPåTidCombi(0, 6, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 30), 8, 30));
            TestBus.StoppeStederMTid[4].AfPåTidComb[0] = (new AfPåTidCombi(0, 7, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 40), 10, 30));
            TestBus.StoppeStederMTid[5].AfPåTidComb[0] = (new AfPåTidCombi(0, 8, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 50), 12, 30));

            Bus AndenTestBus = TestBus;
            /* Dagens rute, hvor bussen har besøgt 5 stoppesteder */
            TestBus.StoppeStederMTid[0].AfPåTidComb.Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[0], TestBus, (day)2, 1, new Tidspunkt(10, 00), 2, 30));
            TestBus.StoppeStederMTid[1].AfPåTidComb.Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[1], TestBus, (day)2, 1, new Tidspunkt(10, 10), 4, 30));
            TestBus.StoppeStederMTid[2].AfPåTidComb.Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[2], TestBus, (day)2, 1, new Tidspunkt(10, 20), 6, 30));
            TestBus.StoppeStederMTid[3].AfPåTidComb.Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[3], TestBus, (day)2, 1, new Tidspunkt(10, 30), 8, 30));
            TestBus.StoppeStederMTid[4].AfPåTidComb.Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[4], TestBus, (day)2, 1, new Tidspunkt(10, 40), 12, 30));
            //TestBus.StoppeStederMTid[5].AfPåTidComb.Add(new AfPåTidCombi(0, 900, TestRute.StoppeSteder[5], TestBus, (day)2, 1, new Tidspunkt(10, 50), 12, 30));

            List<AfPåTidCombi> TestAfPåTidCombi = new List<AfPåTidCombi>();

            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[0].AfPåTidComb[0]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[1].AfPåTidComb[0]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[2].AfPåTidComb[0]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[3].AfPåTidComb[0]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[4].AfPåTidComb[0]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[5].AfPåTidComb[0]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[0].AfPåTidComb[1]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[1].AfPåTidComb[1]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[2].AfPåTidComb[1]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[3].AfPåTidComb[1]);
            TestAfPåTidCombi.Add(TestBus.StoppeStederMTid[4].AfPåTidComb[1]);
            return Algoritme.Algoritmen(TestBus, TestAfPåTidCombi, 5).StoppeStederMTid[5].AfPåTidComb[0].ForventetPassagere;
        }
    }
}
 