using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Funcs
{
    public static Random random;

    public static long MapUlongToLong(ulong input)
    {
        return unchecked((long)input);
    }

    public static ulong MapLongToUlong(long input)
    {
        return unchecked(unchecked((ulong)input));
    }
}
