using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class DeSerializeThreadObject
{
    public List<NetworkObject> OutputList;
    public string Obj;
    public Type ObjType;
    public DeSerializeThreadObject(ref List<NetworkObject> OutputList, string Obj, Type ObjType)
    {
        this.OutputList = OutputList;
        this.Obj = Obj;
        this.ObjType = ObjType;
    }
}