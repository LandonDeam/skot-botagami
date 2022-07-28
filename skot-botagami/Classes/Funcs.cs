// <copyright file="Funcs.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Class used for general project-wide functions.
/// Will likely be slimmed or cut out entirely upon release.
/// </summary>
public class Funcs
{
    /// <summary>
    /// Random to be used globally.
    /// </summary>
    private static Random random;

    /// <summary>
    /// Sets the random to its first value.
    /// </summary>
    public static void SetRandom()
    {
        if (random is null)
        {
            random = new Random();
        }
    }

    /// <summary>
    /// Gets the random to be used.
    /// </summary>
    /// <returns>The random.</returns>
    public static Random GetRandom()
    {
        return random;
    }

    /// <summary>
    /// Maps a ulong to a long without changing the bits.
    /// </summary>
    /// <param name="input">ulong to convert.</param>
    /// <returns>Output long.</returns>
    public static long MapUlongToLong(ulong input) => unchecked((long)input);

    /// <summary>
    /// Maps a long to a ulong without changing the bits.
    /// </summary>
    /// <param name="input">long to convert.</param>
    /// <returns>Output ulong.</returns>
    public static ulong MapLongToUlong(long input)
    {
        return unchecked(unchecked((ulong)input));
    }
}
