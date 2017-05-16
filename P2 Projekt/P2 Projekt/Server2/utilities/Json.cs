using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;
namespace JsonSerializer
{
    public static class Json
    {
        public const string SplitterString = "\n***NEWELEMENT***\n";
        public static string Serialize<T>(T Obj)
        {
            string ObjectType = typeof(T).ToString();
            // get type of object:
            if (typeof(T).ToString() == "System.Object")
            {
                ObjectType = Obj.GetType().ToString();
            }
            return $"object,{ObjectType}|{JsonConvert.SerializeObject(Obj)}";
        }

        public static void SerializeThreaded(object Obj)
        {
            // Det objekt der bliver modtaget, skal være et SerializeThreadObject
            if (!(Obj is SerializeThreadObject))
            {
                throw new NotCorrectObject("Object must be of type SerializeThreadObject!");
            }
            SerializeThreadObject NewObj = (Obj as SerializeThreadObject);
            JsonSerializerSettings Settings = new JsonSerializerSettings();
            lock (NewObj.OutputString)
            {
                NewObj.OutputString.Append($"object,{NewObj.ObjType.ToString()}|{JsonConvert.SerializeObject(NewObj.Obj)}" + SplitterString);
            }
        }

        public static string Serialize<T>(List<T> ObjList)
        {
            Debug.Print($"Serializing {ObjList.Count} of type {typeof(T).ToString()}");
            StringBuilder CompleteJsonString = new StringBuilder();
            int Counter = 0;
            List<Thread> SerializeThreads = new List<Thread>();
            // Opretter en tråd for hvert object der skal serialiseres, og tilføjer den til en liste af tråde.
            foreach (T SinObject in ObjList)
            {
                // Laver en ny paramatiseret tråd
                Thread SerialiserThread = new Thread(new ParameterizedThreadStart(SerializeThreaded));
                // Laver det SerializeThreadObject som skal behandles af serializeren
                SerializeThreadObject ObjToSerilize = new SerializeThreadObject(ref CompleteJsonString, SinObject, typeof(T));
                // Fotæller tråden hvad for en object den skal arbejde med
                SerialiserThread.Start(ObjToSerilize);
                // Tiføjer tråden til tråd listen (List<Thread> SerializeThreads)
                SerializeThreads.Add(SerialiserThread);

            }
            // Tjekker om alle tråde er færdige med at Serialisere
            // Hvis counter er ligmed antallet af Objecter der skulle serialiseres, så er alle tråde færdige. 
            while (Counter != SerializeThreads.Count)
            {
                Counter = 0;
                // Går alle trådene igennem
                foreach (Thread SingleThread in SerializeThreads)
                {
                    // Hvis tråden er død (færdig) så tæl counter op. 
                    if (SingleThread.IsAlive == false)
                    {
                        Counter++;
                    }
                }
            }
            // Retunere den komplette streng. 
            Debug.Print($"Serialized {CompleteJsonString.ToString().Split(new string[] { SplitterString }, StringSplitOptions.None).Length} of type {typeof(T).ToString()}");
            return CompleteJsonString.ToString();
        }

        public static Type GetTypeFromString(string JsonObj)
        {
            int start = JsonObj.IndexOf(",") + 1;
            int end = JsonObj.IndexOf("|");
            int length = end - start;
            if (length <= 0)
            {
                return null;
            }
            string StrType = JsonObj.Substring(start, length);
            return Type.GetType($"{StrType}");
        }

        public static void TrimJson(ref string Raw)
        {
            int start = Raw.IndexOf("|") + 1;
            Raw = Raw.Substring(start);
        }

        public static string[] TrimJsonList(string[] Raw)
        {
            List<string> OutputList = new List<string>();
            foreach (string SingleObject in Raw)
            {
                int start = SingleObject.IndexOf("|") + 1;
                OutputList.Add(SingleObject.Substring(start));
            }
            return OutputList.ToArray();
        }

        public static void DeSerializeThreaded(object Obj)
        {
            // Det objekt der bliver modtaget, skal være et DeSerializeThreadObject
            if (!(Obj is DeSerializeThreadObject))
            {
                throw new NotCorrectObject("Object must be of type DeSerializeThreadObject!");
            }
            DeSerializeThreadObject NewObj = (Obj as DeSerializeThreadObject);
            NewObj.Obj = NewObj.Obj.Replace(@"\", string.Empty);
            NewObj.Obj = NewObj.Obj.Replace("<EOF>", string.Empty);
            var obj = Activator.CreateInstance(NewObj.ObjType);
            Debug.Print($"Deserialiser string: {NewObj.Obj}");
            obj = JsonConvert.DeserializeObject(NewObj.Obj, NewObj.ObjType);
            // Sørger for at andre tråde ikke kan tilgå listen, hvis den er i brug af en tråd. (Kø system)
            lock (NewObj.OutputList)
            {
                if (obj != null)
                {
                    NewObj.OutputList.Add(obj as NetworkObject);
                }
            }
        }

        public static List<NetworkObject> Deserialize(string Obj, bool IsList = false)
        {
            //Makes sure that there is nothing but the Json
            // Since all objects are transfered with added text (eks: Person [JSON.....])
            Type TypeOfObject = GetTypeFromString(Obj);
            string[] Objs = Obj.Split(new string[] { SplitterString }, StringSplitOptions.None);
            Objs = TrimJsonList(Objs);
            List<NetworkObject> ListObjects = new List<NetworkObject> { };
            Debug.Print($"DeSerializing {Objs.Length - 1} of type {TypeOfObject.ToString()}");
            List<Thread> DeSerializeThreads = new List<Thread> { };
            int Counter = 0;
            // Laver tråde, samme som Serialize<T>(List<T> ObjList) |||SE DEN FOR KOMMENTERET KODE|||
            foreach (string SingleObj in Objs)
            {
                if (SingleObj != "")
                {
                    Thread DeSerializeThread = new Thread(new ParameterizedThreadStart(DeSerializeThreaded));
                    DeSerializeThreadObject DeSerObj = new DeSerializeThreadObject(ref ListObjects, SingleObj, TypeOfObject);
                    DeSerializeThread.Start(DeSerObj);
                    DeSerializeThreads.Add(DeSerializeThread);
                }
            }
            // Tjekker om alle tråde er færdige med at DeSerialisere
            // Hvis counter er ligmed antallet af Objecter der skulle deserialiseres, så er alle tråde færdige. 
            while (Counter != DeSerializeThreads.Count)
            {
                Counter = 0;
                // Går alle trådene igennem
                foreach (Thread SingleThread in DeSerializeThreads)
                {
                    // Hvis tråden er død (færdig) så tæl counter op. 
                    if (SingleThread.IsAlive == false)
                    {
                        Counter++;
                    }
                }
            }
            return ListObjects;
        }

        public static Stream StringToStream(string Input)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(Input);
            MemoryStream stream = new MemoryStream(byteArray);
            return stream;
        }

        public static string StreamToString(Stream stream)
        {
            return stream.ToString();
        }

        public static void TrimData(ref string Data)
        {
            Data = Data.Replace("<EOF>", "");
        }
    }
}
