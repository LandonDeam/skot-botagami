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
    
    public void addUser(IGuildUser user, ulong balance)
    {
        // Add user to database
        addUser(user.Id, user.GuildId, balance);
    }

    public void addUser(IGuildUser user)
    {
        // Add user to database
        addUser(user.Id, user.GuildId);
    }

    public void addUser(ulong userId, ulong guildId, ulong balance)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Add user to database
        var addUserCmd = _connection.CreateCommand();
        addUserCmd.CommandText = $@"INSERT INTO balances VALUES ({userId},{guildId},{balance});";
        addUserCmd.ExecuteNonQuery();
    }

    public void addUser(ulong userId, ulong guildId)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Add user to database
        var addUserCmd = _connection.CreateCommand();
        addUserCmd.CommandText = $@"INSERT INTO balances (userId, guildId) VALUES ({userId},{guildId});";
        addUserCmd.ExecuteNonQuery();
    }

    public GuildUser getUser(IGuildUser user)
    {
        // Gets user from database
        throw new NotImplementedException();
    }

    public static ulong getUserBalance(IGuildUser user)
    {
        // Gets a users balance
        throw new NotImplementedException();
    }

    public void removeGuildUser(IGuildUser user)
    {
        // Remove guild user from database
        removeGuildUser(user.Id, user.GuildId);
    }

    public void removeGuildUser(ulong userId, ulong guildId)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Remove guild user from database
        var delGuildUserCmd = _connection.CreateCommand();
        delGuildUserCmd.CommandText = $@"DELETE FROM balances WHERE userId = {userId} AND guildId = {guildId};";
        delGuildUserCmd.ExecuteNonQuery();
    }

    public void removeUser(ulong userId)
    {
        // Check that database is open
        if (!isOpen) openDatabase();
        // Remove user from database
        var delUserCmd = _connection.CreateCommand();
        delUserCmd.CommandText = $@"DELETE FROM balances WHERE userId = {userId};";
        delUserCmd.ExecuteNonQuery();
    }
}
