using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class ArrayHandler
{
    public static int CountNonZeroElementsInByteArray(byte[] ArrayToCount)
    {
        int k = 0;
        for (int i = 0; i < ArrayToCount.Count(); i++)
        {
            if (ArrayToCount[i] != 0)
            {
                k++;
            }
        }
        return k;
    }
}