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

    internal void AddGuildUser(IGuildUser user, ulong balance)
    {
        // Add user to database
        this.AddGuildUser(user.Id, user.GuildId, balance);
    }

    internal void AddGuildUser(ulong userId, ulong guildId, ulong balance)
    {
        // Add user to database
        this.AddGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId), Funcs.MapUlongToLong(balance));
    }

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

    internal void AddGuildUser(IGuildUser user)
    {
        // Add user to database
        this.AddGuildUser(user.Id, user.GuildId);
    }

    internal void AddGuildUser(ulong userId, ulong guildId)
    {
        // Add user to database
        this.AddGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

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

    internal void RemoveGuildUser(IGuildUser user)
    {
        // Remove guild user from database
        this.RemoveGuildUser(user.Id, user.GuildId);
    }

    internal void RemoveGuildUser(ulong userId, ulong guildId)
    {
        // Remove guild user from database
        this.RemoveGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

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

    internal void RemoveUser(IUser user)
    {
        // Remove user from database
        this.RemoveUser(user.Id);
    }

    internal void RemoveUser(ulong userId)
    {
        // Remove user from database
        this.RemoveUser(Funcs.MapUlongToLong(userId));
    }

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

    internal ulong GetGuildUserBalance(IGuildUser user)
    {
        // Gets a users balance
        return this.GetGuildUserBalance(user.Id, user.GuildId);
    }

    internal ulong GetGuildUserBalance(ulong userId, ulong guildId)
    {
        // Gets a users balance
        return this.GetGuildUserBalance(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    internal ulong GetGuildUserBalance(long userId, long guildId)
    {
        // Gets a users balance
        var selectCmd = this.connection.CreateCommand();
        selectCmd.CommandText = $@"SELECT balance FROM balance WHERE userId = {userId} AND guildId = {guildId};";
        return Funcs.MapLongToUlong(selectCmd.ExecuteReader().GetInt64(0));
    }

    internal void SetGuildUserBalance(IGuildUser user, ulong balance)
    {
        // Set a users balance
        this.SetGuildUserBalance(user.Id, user.GuildId, balance);
    }

    internal void SetGuildUserBalance(ulong userId, ulong guildId, ulong balance)
    {
        // Set a users balance
        this.SetGuildUserBalance(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId), Funcs.MapUlongToLong(balance));
    }

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

    internal bool GuildUserExists(IGuildUser user)
    {
        // Checks if a user exists in the database
        return this.GuildUserExists(user.Id, user.GuildId);
    }

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

    internal void CloseDatabase()
    {
        // Closes the database
        this.connection.Close();
        this.isOpen = false;
    }
}