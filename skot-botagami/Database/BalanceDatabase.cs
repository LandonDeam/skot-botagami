// <copyright file="BalanceDatabase.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Microsoft.Data.Sqlite;

/// <summary>
/// Class used for managing database for the balances of all users.
/// </summary>
public class BalanceDatabase
{
    private SqliteConnection connection;
    private bool isOpen = false;

    /// <summary>
    /// Opens the database for use.
    /// </summary>
    internal void OpenDatabase()
    {
        // Opens the database for use
        var connectionStringBuilder = new SqliteConnectionStringBuilder();
        connectionStringBuilder.DataSource = "./balancesheet.db";

        var connection = new SqliteConnection(connectionStringBuilder.ConnectionString);
        connection.Open();

        var tableCmd = connection.CreateCommand();
        tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS balances (
                                        userId INTEGER NOT NULL,
                                        guildId INTEGER NOT NULL,
                                        balance INTEGER,
                                        PRIMARY KEY (userId, guildId)
                                    );";
        tableCmd.ExecuteNonQuery();

        this.connection = connection;
        this.isOpen = true;
    }

    /// <summary>
    /// Adds a guild user with the given balance to the database.
    /// </summary>
    /// <param name="user">Guild user to add.</param>
    /// <param name="balance">Balance of the guild user.</param>
    internal void AddGuildUser(IGuildUser user, ulong balance)
    {
        // Add user to database
        this.AddGuildUser(user.Id, user.GuildId, balance);
    }

    /// <summary>
    /// Adds a guild user with the given balance to the database.
    /// </summary>
    /// <param name="userId">User to add.</param>
    /// <param name="guildId">Guild the user is in.</param>
    /// <param name="balance">Balance of the guild user.</param>
    internal void AddGuildUser(ulong userId, ulong guildId, ulong balance)
    {
        // Add user to database
        this.AddGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId), Funcs.MapUlongToLong(balance));
    }

    /// <summary>
    /// Adds a guild user with the given balance to the database.
    /// </summary>
    /// <param name="userId">User to add.</param>
    /// <param name="guildId">Guild the user is in.</param>
    /// <param name="balance">Balance of the guild user.</param>
    internal void AddGuildUser(long userId, long guildId, long balance)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Add user to database
        var addGuildUserCmd = this.connection.CreateCommand();
        addGuildUserCmd.CommandText = $@"INSERT INTO balances VALUES ({userId},{guildId},{balance});";
        addGuildUserCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Adds a guild user to the database.
    /// </summary>
    /// <param name="user">Guild user to add.</param>
    internal void AddGuildUser(IGuildUser user)
    {
        // Add user to database
        this.AddGuildUser(user.Id, user.GuildId);
    }

    /// <summary>
    /// Adds a guild user to the database.
    /// </summary>
    /// <param name="userId">Guild user to add.</param>
    /// <param name="guildId">Guild the user is in.</param>
    internal void AddGuildUser(ulong userId, ulong guildId)
    {
        // Add user to database
        this.AddGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    /// <summary>
    /// Adds a guild user to the database.
    /// </summary>
    /// <param name="userId">Guild user to add.</param>
    /// <param name="guildId">Guild the user is in.</param>
    internal void AddGuildUser(long userId, long guildId)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Add user to database
        var addGuildUserCmd = this.connection.CreateCommand();
        addGuildUserCmd.CommandText = $@"INSERT INTO balances (userId, guildId) VALUES ({userId},{guildId});";
        addGuildUserCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Removes a guild user from the database.
    /// </summary>
    /// <param name="user">Guild user to remove.</param>
    internal void RemoveGuildUser(IGuildUser user)
    {
        // Remove guild user from database
        this.RemoveGuildUser(user.Id, user.GuildId);
    }

    /// <summary>
    /// Removes a guild user from the database.
    /// </summary>
    /// <param name="userId">ID of the user to remove.</param>
    /// <param name="guildId">Guild ID of the guild user to remove.</param>
    internal void RemoveGuildUser(ulong userId, ulong guildId)
    {
        // Remove guild user from database
        this.RemoveGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    /// <summary>
    /// Removes a guild user from the database.
    /// </summary>
    /// <param name="userId">ID of the user to remove.</param>
    /// <param name="guildId">Guild ID of the guild user to remove.</param>
    internal void RemoveGuildUser(long userId, long guildId)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Remove guild user from database
        var delGuildUserCmd = this.connection.CreateCommand();
        delGuildUserCmd.CommandText = $@"DELETE FROM balances WHERE userId = {userId} AND guildId = {guildId};";
        delGuildUserCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Removes a user from the database.
    /// </summary>
    /// <param name="user">User to remove.</param>
    internal void RemoveUser(IUser user)
    {
        // Remove user from database
        this.RemoveUser(user.Id);
    }

    /// <summary>
    /// Removes a user from the database.
    /// </summary>
    /// <param name="userId">User to remove.</param>
    internal void RemoveUser(ulong userId)
    {
        // Remove user from database
        this.RemoveUser(Funcs.MapUlongToLong(userId));
    }

    /// <summary>
    /// Removes a user from the database.
    /// </summary>
    /// <param name="userId">User to remove.</param>
    internal void RemoveUser(long userId)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Remove user from database
        var delUserCmd = this.connection.CreateCommand();
        delUserCmd.CommandText = $@"DELETE FROM balances WHERE userId = {userId};";
        delUserCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Gets the balance of a guild user.
    /// </summary>
    /// <param name="user">Guild user to get the balance of.</param>
    /// <returns>Balance of the guild user.</returns>
    internal ulong GetGuildUserBalance(IGuildUser user)
    {
        // Gets a users balance
        return this.GetGuildUserBalance(user.Id, user.GuildId);
    }

    /// <summary>
    /// Gets the balance of a guild user.
    /// </summary>
    /// <param name="userId">User ID to get the balance of.</param>
    /// <param name="guildId">Guild ID of the guild user to get the balance of.</param>
    /// <returns>Balance of the guild user.</returns>
    internal ulong GetGuildUserBalance(ulong userId, ulong guildId)
    {
        // Gets a users balance
        return this.GetGuildUserBalance(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    /// <summary>
    /// Gets the balance of a guild user.
    /// </summary>
    /// <param name="userId">User ID to get the balance of.</param>
    /// <param name="guildId">Guild ID of the guild user to get the balance of.</param>
    /// <returns>Balance of the guild user.</returns>
    internal ulong GetGuildUserBalance(long userId, long guildId)
    {
        // Gets a users balance
        var selectCmd = this.connection.CreateCommand();
        selectCmd.CommandText = $@"SELECT balance FROM balance WHERE userId = {userId} AND guildId = {guildId};";
        return Funcs.MapLongToUlong(selectCmd.ExecuteReader().GetInt64(0));
    }

    /// <summary>
    /// Sets the balance of a guild user.
    /// </summary>
    /// <param name="user">User to set the balance of.</param>
    /// <param name="balance">Balance to set the user's balance to.</param>
    internal void SetGuildUserBalance(IGuildUser user, ulong balance)
    {
        // Set a users balance
        this.SetGuildUserBalance(user.Id, user.GuildId, balance);
    }

    /// <summary>
    /// Sets the balance of a guild user.
    /// </summary>
    /// <param name="userId">User ID to set the balance of.</param>
    /// <param name="guildId">Guild ID of the guild user to set the balance of.</param>
    /// <param name="balance">Balance to set the user's balance to.</param>
    internal void SetGuildUserBalance(ulong userId, ulong guildId, ulong balance)
    {
        // Set a users balance
        this.SetGuildUserBalance(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId), Funcs.MapUlongToLong(balance));
    }

    /// <summary>
    /// Sets the balance of a guild user.
    /// </summary>
    /// <param name="userId">User ID to set the balance of.</param>
    /// <param name="guildId">Guild ID of the guild user to set the balance of.</param>
    /// <param name="balance">Balance to set the user's balance to.</param>
    internal void SetGuildUserBalance(long userId, long guildId, long balance)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Set a users balance
        var setGuildUserBalanceCmd = this.connection.CreateCommand();
        setGuildUserBalanceCmd.CommandText = $@"UPDATE balances SET balance = {balance} WHERE userId = {userId} AND guildId = {guildId};";
        setGuildUserBalanceCmd.ExecuteNonQuery();
    }

    /// <summary>
    /// Checks if a user exists in the database.
    /// </summary>
    /// <param name="user">Guild user to check for.</param>
    /// <returns>true if user exists, false if they don't.</returns>
    internal bool GuildUserExists(IGuildUser user)
    {
        // Checks if a user exists in the database
        return this.GuildUserExists(user.Id, user.GuildId);
    }

    /// <summary>
    /// Checks if a user exists in the database.
    /// </summary>
    /// <param name="userId">ID of the user to check.</param>
    /// <param name="guildId">Guild ID of the guild user to check.</param>
    /// <returns>true if user exists, false if they don't.</returns>
    internal bool GuildUserExists(ulong userId, ulong guildId)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Checks if a user exists in the database
        return this.GuildUserExists(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    /// <summary>
    /// Checks if a user exists in the database.
    /// </summary>
    /// <param name="userId">ID of the user to check.</param>
    /// <param name="guildId">Guild ID of the guild user to check.</param>
    /// <returns>true if user exists, false if they don't.</returns>
    internal bool GuildUserExists(long userId, long guildId)
    {
        // Check that database is open
        if (!this.isOpen)
        {
            this.OpenDatabase();
        }

        // Checks if a user exists in the database
        var selectCmd = this.connection.CreateCommand();
        selectCmd.CommandText = $@"SELECT * FROM balances WHERE userId = {userId} AND guildId = {guildId};";
        return selectCmd.ExecuteReader().Read();
    }

    /// <summary>
    /// Closes the database connection.
    /// </summary>
    internal void CloseDatabase()
    {
        // Closes the database
        this.connection.Close();
        this.isOpen = false;
    }
}