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
        public static void AlgoritmeTest1()
        {
            Rute TestRute;

            // Stoppesteder til rute 2 med Storvorde //
            Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309));
            Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441));
            Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364));
            Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831));
            Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901));
            Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311));
            Stoppested Stop1 = new Stoppested("TesStop1", 7, new GPS(57.053136, 9.706311));
            Stoppested Stop2 = new Stoppested("TesStop2", 8, new GPS(57.053136, 9.706311));
            Stoppested Stop3 = new Stoppested("TesStop3", 9, new GPS(57.053136, 9.706311));
            Stoppested Stop4 = new Stoppested("TesStop4", 10, new GPS(57.053136, 9.706311));
            Stoppested Stop5 = new Stoppested("TesStop5", 11, new GPS(57.053136, 9.706311));
            Stoppested Stop6 = new Stoppested("TesStop6", 12, new GPS(57.053136, 9.706311));
            Stoppested Stop7 = new Stoppested("TesStop7", 13, new GPS(57.053136, 9.706311));
            Stoppested Stop8 = new Stoppested("TesStop8", 14, new GPS(57.053136, 9.706311));
            Stoppested Stop9 = new Stoppested("TesStop9", 15, new GPS(57.053136, 9.706311));
            Stoppested Stop0 = new Stoppested("TesStop0", 16, new GPS(57.053136, 9.706311));

            TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                                stopFriluftsbadet,
                                                                                stopStenBillesGade,
                                                                                stopHaradlslund,
                                                                                stopSchleppegrellsgade,
                                                                                stopSctJoseph,
                                                                                Stop1,
                                                                                Stop2,
                                                                                Stop3,
                                                                                Stop4,
                                                                                Stop5,
                                                                                Stop6,
                                                                                Stop7,
                                                                                Stop8,
                                                                                Stop9,
                                                                                Stop0
                                                                                );

            Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute);
            List<AfPåTidCombi> HistoryData = new List<AfPåTidCombi>();
            /* Historik til alle 6 stoppesteder */
            AfPåTidCombi NyHis;
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30);
            NyHis.ID = 8001;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 05), 4, 30);
            NyHis.ID = 8002;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 3, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 10), 7, 30);
            NyHis.ID = 8003;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 15), 8, 30);
            NyHis.ID = 8004;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 0, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 20), 8, 30);
            NyHis.ID = 8005;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(2, 0, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 25), 6, 30);
            NyHis.ID = 8006;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[6], TestBus, (day)1, 1, new Tidspunkt(10, 30), 5, 30);
            NyHis.ID = 8007;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[7], TestBus, (day)1, 1, new Tidspunkt(10, 35), 6, 30);
            NyHis.ID = 8008;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[8], TestBus, (day)1, 1, new Tidspunkt(10, 35), 7, 30);
            NyHis.ID = 8009;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[9], TestBus, (day)1, 1, new Tidspunkt(10, 35), 8, 30);
            NyHis.ID = 8010;
            HistoryData.Add(NyHis);

            NyHis = new AfPåTidCombi(0, 4, TestRute.StoppeSteder[10], TestBus, (day)1, 1, new Tidspunkt(10, 35), 12, 30);
            NyHis.ID = 8011;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[11], TestBus, (day)1, 1, new Tidspunkt(10, 35), 14, 30);
            NyHis.ID = 8012;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[12], TestBus, (day)1, 1, new Tidspunkt(10, 35), 16, 30);
            NyHis.ID = 8013;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 4, TestRute.StoppeSteder[13], TestBus, (day)1, 1, new Tidspunkt(10, 35), 20, 30);
            NyHis.ID = 8014;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[14], TestBus, (day)1, 1, new Tidspunkt(10, 35), 22, 30);
            NyHis.ID = 8015;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[15], TestBus, (day)1, 1, new Tidspunkt(10, 35), 21, 30);
            NyHis.ID = 8016;
            HistoryData.Add(NyHis);
            // ----- HISTORIE SLUT -----


            //------ DATA FRA IDAG ------
            StoppestedMTid NytStopMTid;

            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[0];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 00), 4, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9001;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 2;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[1];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 05), 6, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9002;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 3;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[2];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 10), 10, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9003;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[3];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 15), 12, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9004;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[4];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 20), 13, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9005;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 13;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[5];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 14, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9006;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 14;
            TestBus.StoppeStederMTid.Add(NytStopMTid);

            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[6];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(2, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 12, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9007;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[7];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(2, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 10, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9008;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 10;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[8];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 9, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9009;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 9;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[9];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 8, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9010;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 8;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[10];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(1, 0, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 7, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9011;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[11];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 8, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9012;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 8;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[12];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[12], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[12].AfPåTidComb[0].ID = 9013;
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[13];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[13], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[13].AfPåTidComb[0].ID = 9014;
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[14];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[14], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[14].AfPåTidComb[0].ID = 9015;
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[15];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[15], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[15].AfPåTidComb[0].ID = 9016;
            // ---------- Dags data slut ---------- 
            Bus OuputBus = Algoritme.Algoritmen(TestBus, HistoryData, 12);
            StringBuilder NewStringBuilder = new StringBuilder();
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[12].AfPåTidComb[0].ForventetPassagere.ToString());
            NewStringBuilder.Append(",");
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[13].AfPåTidComb[0].ForventetPassagere.ToString());
            NewStringBuilder.Append(",");
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[14].AfPåTidComb[0].ForventetPassagere.ToString());
            NewStringBuilder.Append(",");
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[15].AfPåTidComb[0].ForventetPassagere.ToString());
            string output = NewStringBuilder.ToString();
            Assert.AreEqual("7,8,7,3", output);
        }

        [Test()]
        public static void AlgoritmeTest2()
        {
            Rute TestRute;

            // Stoppesteder til rute 2 med Storvorde //
            Stoppested stopSkydebanevej = new Stoppested("Skydebanevej v/ Væddeløbsbanen", 1, new GPS(57.054779, 9.882309));
            Stoppested stopFriluftsbadet = new Stoppested("Friluftsbadet (Skydebanevej / Aalborg)", 2, new GPS(57.055173, 9.887441));
            Stoppested stopStenBillesGade = new Stoppested("Steen Billes Gade (Kastetvej / Aalborg)", 3, new GPS(57.055785, 9.893364));
            Stoppested stopHaradlslund = new Stoppested("Haraldslund (Kastetvej / Aalborg)", 4, new GPS(57.054829, 9.897831));
            Stoppested stopSchleppegrellsgade = new Stoppested("Schleppegrellsgade (Kastetvej / Aalborg)", 5, new GPS(57.053932, 9.901901));
            Stoppested stopSctJoseph = new Stoppested("Sct. Joseph (Kastetvej / Aalborg)", 6, new GPS(57.053136, 9.906311));
            Stoppested Stop1 = new Stoppested("TesStop1", 7, new GPS(57.053136, 9.706311));
            Stoppested Stop2 = new Stoppested("TesStop2", 8, new GPS(57.053136, 9.706311));
            Stoppested Stop3 = new Stoppested("TesStop3", 9, new GPS(57.053136, 9.706311));
            Stoppested Stop4 = new Stoppested("TesStop4", 10, new GPS(57.053136, 9.706311));

            TestRute = new Rute("Rute2 Væddeløbsbanen - Storvorde", 129, stopSkydebanevej,
                                                                                stopFriluftsbadet,
                                                                                stopStenBillesGade,
                                                                                stopHaradlslund,
                                                                                stopSchleppegrellsgade,
                                                                                stopSctJoseph,
                                                                                Stop1,
                                                                                Stop2,
                                                                                Stop3,
                                                                                Stop4
                                                                                );

            Bus TestBus = new Bus("TestBus", 10001, 15, 15, TestRute);
            List<AfPåTidCombi> HistoryData = new List<AfPåTidCombi>();
            /* Historik til alle 6 stoppesteder */
            AfPåTidCombi NyHis;
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[0], TestBus, (day)1, 1, new Tidspunkt(10, 00), 2, 30);
            NyHis.ID = 8001;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 2, TestRute.StoppeSteder[1], TestBus, (day)1, 1, new Tidspunkt(10, 05), 4, 30);
            NyHis.ID = 8002;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 3, TestRute.StoppeSteder[2], TestBus, (day)1, 1, new Tidspunkt(10, 10), 7, 30);
            NyHis.ID = 8003;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[3], TestBus, (day)1, 1, new Tidspunkt(10, 15), 8, 30);
            NyHis.ID = 8004;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 0, TestRute.StoppeSteder[4], TestBus, (day)1, 1, new Tidspunkt(10, 20), 8, 30);
            NyHis.ID = 8005;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(2, 0, TestRute.StoppeSteder[5], TestBus, (day)1, 1, new Tidspunkt(10, 25), 6, 30);
            NyHis.ID = 8006;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(1, 0, TestRute.StoppeSteder[6], TestBus, (day)1, 1, new Tidspunkt(10, 30), 5, 30);
            NyHis.ID = 8007;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[7], TestBus, (day)1, 1, new Tidspunkt(10, 35), 6, 30);
            NyHis.ID = 8008;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[8], TestBus, (day)1, 1, new Tidspunkt(10, 35), 7, 30);
            NyHis.ID = 8009;
            HistoryData.Add(NyHis);
            NyHis = new AfPåTidCombi(0, 1, TestRute.StoppeSteder[9], TestBus, (day)1, 1, new Tidspunkt(10, 35), 8, 30);
            NyHis.ID = 8010;
            HistoryData.Add(NyHis);
            // ----- HISTORIE SLUT -----


            //------ DATA FRA IDAG ------
            StoppestedMTid NytStopMTid;

            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[0];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 00), 4, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9001;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 2;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[1];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 05), 6, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9002;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 3;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[2];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 4, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 10), 10, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9003;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 7;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[3];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 2, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 15), 12, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9004;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 12;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[4];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 20), 13, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9005;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 13;
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[5];
            NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>();
            NytStopMTid.AfPåTidComb.Add(new AfPåTidCombi(0, 1, NytStopMTid.Stop, TestBus, (day)2, 1, new Tidspunkt(10, 25), 14, 30));
            NytStopMTid.AfPåTidComb[0].ID = 9006;
            NytStopMTid.AfPåTidComb[0].ForventetPassagere = 14;
            TestBus.StoppeStederMTid.Add(NytStopMTid);

            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[6];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[6], TestBus, (day)2, 1, new Tidspunkt(10, 30), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[6].AfPåTidComb[0].ID = 9007;
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[7];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[7], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[7].AfPåTidComb[0].ID = 9008;
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[8];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[8], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[8].AfPåTidComb[0].ID = 9009;
            //------------------------------------------------
            NytStopMTid = new StoppestedMTid();
            NytStopMTid.Stop = TestRute.StoppeSteder[9];
            (NytStopMTid.AfPåTidComb = new List<AfPåTidCombi>()).Add(new AfPåTidCombi(0, 0, TestRute.StoppeSteder[9], TestBus, (day)2, 1, new Tidspunkt(10, 35), 0, 30));
            TestBus.StoppeStederMTid.Add(NytStopMTid);
            TestBus.StoppeStederMTid[9].AfPåTidComb[0].ID = 9010;

            Bus OuputBus = Algoritme.Algoritmen(TestBus, HistoryData, 6);
            StringBuilder NewStringBuilder = new StringBuilder();
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[6].AfPåTidComb[0].ForventetPassagere.ToString());
            NewStringBuilder.Append(",");
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[7].AfPåTidComb[0].ForventetPassagere.ToString());
            NewStringBuilder.Append(",");
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[8].AfPåTidComb[0].ForventetPassagere.ToString());
            NewStringBuilder.Append(",");
            NewStringBuilder.Append(OuputBus.StoppeStederMTid[9].AfPåTidComb[0].ForventetPassagere.ToString());
            string output = NewStringBuilder.ToString();
            Assert.AreEqual("14,16,18,20", output);
        }
    }
}

 