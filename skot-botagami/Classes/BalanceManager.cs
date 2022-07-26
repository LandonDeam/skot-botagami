using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

public class BalanceManager
{
    public static BalanceDatabase _balanceDatabase;

    public static BalanceDatabase openDatabase()
    {
        _balanceDatabase = new BalanceDatabase();
        _balanceDatabase.openDatabase();
        return _balanceDatabase;
    }
    
    public static ulong getGuildUserBalance(IGuildUser balanceHolder)
    {
        if (_balanceDatabase == null) openDatabase();
        return _balanceDatabase.getGuildUserBalance(balanceHolder);
    }

    public static ulong getGuildUserBalance(ulong userId, ulong guildId)
    {
        if (_balanceDatabase == null) openDatabase();
        return _balanceDatabase.getGuildUserBalance(userId, guildId);
    }

    public static void setGuildUserBalance(IGuildUser user, ulong balance)
    {
        if (_balanceDatabase == null) openDatabase();
        _balanceDatabase.setGuildUserBalance(user, balance);
    }

    public static void addGuildUser(IGuildUser user)
    {
        if (_balanceDatabase == null) openDatabase();
        _balanceDatabase.addGuildUser(user, 100);
    }

    public static void addGuildUserBalance(IGuildUser user, ulong diff)
    {
        if (_balanceDatabase == null) openDatabase();
        setGuildUserBalance(user, checked(getGuildUserBalance(user) + diff));
    }

    public static void subtractGuildUserBalance(IGuildUser user, ulong diff)
    {
        if (_balanceDatabase == null) openDatabase();
        setGuildUserBalance(user, checked(getGuildUserBalance(user) - diff));
    }
}
