using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
public static class Json
{
    public static string Serialize<T>(T Obj)
    {
        return $"object,{typeof(T).ToString()}|{JsonConvert.SerializeObject(Obj)}";
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

    public static NetworkObject Deserialize(string Obj)
    {
        //Makes sure that there is nothing but the Json
        // Since all objects are transfered with added text (eks: Person [JSON.....])
        Type TypeOfObject = GetTypeFromString(Obj);
        TrimJson(ref Obj);
        var obj = Activator.CreateInstance(TypeOfObject);
        // JsonSerializerSettings Jsettings = new JsonSerializerSettings();
        obj = JsonConvert.DeserializeObject(Obj, TypeOfObject);
        return obj as NetworkObject;
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