using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Microsoft.Data.Sqlite;

public class BalanceDatabase
{
    private SqliteConnection _connection;
    private bool isOpen = false;
    public void openDatabase()
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

        _connection = connection;
        isOpen = true;
    }
    
    public void addGuildUser(IGuildUser user, ulong balance)
    {
        // Add user to database
        addGuildUser(user.Id, user.GuildId, balance);
    }

    public void addGuildUser(ulong userId, ulong guildId, ulong balance)
    {
        // Add user to database
        addGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId), Funcs.MapUlongToLong(balance));
    }

    public void addGuildUser(long userId, long guildId, long balance)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Add user to database
        var addGuildUserCmd = _connection.CreateCommand();
        addGuildUserCmd.CommandText = $@"INSERT INTO balances VALUES ({userId},{guildId},{balance});";
        addGuildUserCmd.ExecuteNonQuery();
    }

    public void addGuildUser(IGuildUser user)
    {
        // Add user to database
        addGuildUser(user.Id, user.GuildId);
    }

    public void addGuildUser(ulong userId, ulong guildId)
    {
        // Add user to database
        addGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    public void addGuildUser(long userId, long guildId)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Add user to database
        var addGuildUserCmd = _connection.CreateCommand();
        addGuildUserCmd.CommandText = $@"INSERT INTO balances (userId, guildId) VALUES ({userId},{guildId});";
        addGuildUserCmd.ExecuteNonQuery();
    }

    public void removeGuildUser(IGuildUser user)
    {
        // Remove guild user from database
        removeGuildUser(user.Id, user.GuildId);
    }

    public void removeGuildUser(ulong userId, ulong guildId)
    {
        // Remove guild user from database
        removeGuildUser(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    public void removeGuildUser(long userId, long guildId)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Remove guild user from database
        var delGuildUserCmd = _connection.CreateCommand();
        delGuildUserCmd.CommandText = $@"DELETE FROM balances WHERE userId = {userId} AND guildId = {guildId};";
        delGuildUserCmd.ExecuteNonQuery();
    }

    public void removeUser(IUser user)
    {
        // Remove user from database
        removeUser(user.Id);
    }

    public void removeUser(ulong userId)
    {
        // Remove user from database
        removeUser(Funcs.MapUlongToLong(userId));
    }

    public void removeUser(long userId)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Remove user from database
        var delUserCmd = _connection.CreateCommand();
        delUserCmd.CommandText = $@"DELETE FROM balances WHERE userId = {userId};";
        delUserCmd.ExecuteNonQuery();
    }

    public ulong getGuildUserBalance(IGuildUser user)
    {
        // Gets a users balance
        return getGuildUserBalance(user.Id, user.GuildId);
    }

    public ulong getGuildUserBalance(ulong userId, ulong guildId)
    {
        // Gets a users balance
        return getGuildUserBalance(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId));
    }

    public ulong getGuildUserBalance(long userId, long guildId)
    {
        // Gets a users balance
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = $@"SELECT balance FROM balance WHERE userId = {userId} AND guildId = {guildId};";
        return Funcs.MapLongToUlong(selectCmd.ExecuteReader().GetInt64(0));
    }

    public void setGuildUserBalance(IGuildUser user, ulong balance)
    {
        // Set a users balance
        setGuildUserBalance(user.Id, user.GuildId, balance);
    }

    public void setGuildUserBalance(ulong userId, ulong guildId, ulong balance)
    {
        // Set a users balance
        setGuildUserBalance(Funcs.MapUlongToLong(userId), Funcs.MapUlongToLong(guildId), Funcs.MapUlongToLong(balance));
    }

    public void setGuildUserBalance(long userId, long guildId, long balance)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Set a users balance
        var setGuildUserBalanceCmd = _connection.CreateCommand();
        setGuildUserBalanceCmd.CommandText = $@"UPDATE balances SET balance = {balance} WHERE userId = {userId} AND guildId = {guildId};";
        setGuildUserBalanceCmd.ExecuteNonQuery();
    }

    public void closeDatabase()
    {
        // Closes the database
        _connection.Close();
        isOpen = false;
    }
}