using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class SerializeThreadObject
{
    public StringBuilder OutputString;
    public object Obj;
    public Type ObjType;
    public SerializeThreadObject(ref StringBuilder OutputString, object Obj, Type ObjType)
    {
        this.OutputString = OutputString;
        this.Obj = Obj;
        this.ObjType = ObjType;
    }
}