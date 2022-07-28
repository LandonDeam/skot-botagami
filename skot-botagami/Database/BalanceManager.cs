// <copyright file="BalanceManager.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

/// <summary>
/// Class used for managing the balance of a user.
/// </summary>
public class BalanceManager
{
    private static BalanceDatabase balanceDatabase;

    /// <summary>
    /// Opens the databse for the balance manager.
    /// </summary>
    /// <returns>The balance database.</returns>
    public static BalanceDatabase OpenDatabase()
    {
        balanceDatabase = new BalanceDatabase();
        balanceDatabase.OpenDatabase();
        return balanceDatabase;
    }

    /// <summary>
    /// Gets the given guild user's balance.
    /// </summary>
    /// <param name="balanceHolder">The guild user to look for.</param>
    /// <returns>The given guild user's balance.</returns>
    public static ulong GetGuildUserBalance(IGuildUser balanceHolder)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        return balanceDatabase.GetGuildUserBalance(balanceHolder);
    }

    /// <summary>
    /// Gets the guild user's balance.
    /// </summary>
    /// <param name="userId">Guild user's ID.</param>
    /// <param name="guildId">Guild's ID.</param>
    /// <returns>The given guild user's balance.</returns>
    public static ulong GetGuildUserBalance(ulong userId, ulong guildId)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        return balanceDatabase.GetGuildUserBalance(userId, guildId);
    }

    /// <summary>
    /// Sets the given guild user's balance to the given value.
    /// </summary>
    /// <param name="user">Guild user to set the balance of.</param>
    /// <param name="balance">Balance to set the given guild user to.</param>
    public static void SetGuildUserBalance(IGuildUser user, ulong balance)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        balanceDatabase.SetGuildUserBalance(user, balance);
    }

    /// <summary>
    /// Adds the given guild user to the database.
    /// </summary>
    /// <param name="user">Guild user to add to the database.</param>
    public static void AddGuildUser(IGuildUser user)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        balanceDatabase.AddGuildUser(user, 100);
    }

    /// <summary>
    /// Adds the given balance to the given guild user's balance.
    /// </summary>
    /// <param name="user">Guild user to add balance to.</param>
    /// <param name="diff">Balance to give to the given guild user.</param>
    public static void AddGuildUserBalance(IGuildUser user, ulong diff)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        SetGuildUserBalance(user, checked(GetGuildUserBalance(user) + diff));
    }

    /// <summary>
    /// Subtracts the given balance from the given guild user's balance.
    /// </summary>
    /// <param name="user">Guild user to subtract the balance from.</param>
    /// <param name="diff">Balance to subtract from the given guild user.</param>
    public static void SubtractGuildUserBalance(IGuildUser user, ulong diff)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        SetGuildUserBalance(user, checked(GetGuildUserBalance(user) - diff));
    }

    /// <summary>
    /// Checks if the given guild user exists in the database.
    /// </summary>
    /// <param name="user">Guild user to check.</param>
    /// <returns>true if the guild user exists, false if the guild user does not.</returns>
    public static bool GuildUserExists(IGuildUser user)
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        return balanceDatabase.GuildUserExists(user);
    }

    /// <summary>
    /// Closes the database.
    /// </summary>
    public static void CloseDatabase()
    {
        if (balanceDatabase == null)
        {
            OpenDatabase();
        }

        balanceDatabase.CloseDatabase();
    }
}