using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

public class GuildUser : IGuildUser
{
    private IGuildUser baseUser;
    public GuildUser(IGuildUser user)
    {
        // Initialize IGuildUser interface
        baseUser = user;

        balance = BalanceManager.getBalance(user);
    }

    ulong balance;

    //
    // Implementation of the base IGuildUser interface
    //
    public DateTimeOffset? JoinedAt => baseUser.JoinedAt;

    public string DisplayName => baseUser.DisplayName;

    public string Nickname => baseUser.Nickname;

    public string DisplayAvatarId => baseUser.DisplayAvatarId;

    public string GuildAvatarId => baseUser.GuildAvatarId;

    public GuildPermissions GuildPermissions => baseUser.GuildPermissions;

    public IGuild Guild => baseUser.Guild;

    public ulong GuildId => baseUser.GuildId;

    public DateTimeOffset? PremiumSince => baseUser.PremiumSince;

    public IReadOnlyCollection<ulong> RoleIds => baseUser.RoleIds;

    public bool? IsPending => baseUser.IsPending;

    public int Hierarchy => baseUser.Hierarchy;

    public DateTimeOffset? TimedOutUntil => baseUser.TimedOutUntil;

    public string AvatarId => baseUser.AvatarId;

    public string Discriminator => baseUser.Discriminator;

    public ushort DiscriminatorValue => baseUser.DiscriminatorValue;

    public bool IsBot => baseUser.IsBot;

    public bool IsWebhook => baseUser.IsWebhook;

    public string Username => baseUser.Username;

    public UserProperties? PublicFlags => baseUser.PublicFlags;

    public DateTimeOffset CreatedAt => baseUser.CreatedAt;

    public ulong Id => baseUser.Id;

    public string Mention => baseUser.Mention;

    public UserStatus Status => baseUser.Status;

    public IReadOnlyCollection<ClientType> ActiveClients => baseUser.ActiveClients;

    public IReadOnlyCollection<IActivity> Activities => baseUser.Activities;

    public bool IsDeafened => baseUser.IsDeafened;

    public bool IsMuted => baseUser.IsMuted;

    public bool IsSelfDeafened => baseUser.IsSelfDeafened;

    public bool IsSelfMuted => baseUser.IsSelfMuted;

    public bool IsSuppressed => baseUser.IsSuppressed;

    public IVoiceChannel VoiceChannel => baseUser.VoiceChannel;

    public string VoiceSessionId => baseUser.VoiceSessionId;

    public bool IsStreaming => baseUser.IsStreaming;

    public bool IsVideoing => baseUser.IsVideoing;

    public DateTimeOffset? RequestToSpeakTimestamp => baseUser.RequestToSpeakTimestamp;

    public Task AddRoleAsync(ulong roleId, RequestOptions options = null)
    {
        return baseUser.AddRoleAsync(roleId, options);
    }

    public Task AddRoleAsync(IRole role, RequestOptions options = null)
    {
        return baseUser.AddRoleAsync(role, options);
    }

    public Task AddRolesAsync(IEnumerable<ulong> roleIds, RequestOptions options = null)
    {
        return baseUser.AddRolesAsync(roleIds, options);
    }

    public Task AddRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
    {
        return baseUser.AddRolesAsync(roles, options);
    }

    public Task<IDMChannel> CreateDMChannelAsync(RequestOptions options = null)
    {
        return baseUser.CreateDMChannelAsync(options);
    }

    public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return baseUser.GetAvatarUrl(format, size);
    }

    public string GetDefaultAvatarUrl()
    {
        return baseUser.GetDefaultAvatarUrl();
    }

    public string GetDisplayAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return baseUser.GetDisplayAvatarUrl(format, size);
    }

    public string GetGuildAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return baseUser.GetGuildAvatarUrl(format, size);
    }

    public ChannelPermissions GetPermissions(IGuildChannel channel)
    {
        return baseUser.GetPermissions(channel);
    }

    public Task KickAsync(string reason = null, RequestOptions options = null)
    {
        return baseUser.KickAsync(reason, options);
    }

    public Task ModifyAsync(Action<GuildUserProperties> func, RequestOptions options = null)
    {
        return baseUser.ModifyAsync(func, options);
    }

    public Task RemoveRoleAsync(ulong roleId, RequestOptions options = null)
    {
        return baseUser.RemoveRoleAsync(roleId, options);
    }

    public Task RemoveRoleAsync(IRole role, RequestOptions options = null)
    {
        return baseUser.RemoveRoleAsync(role, options);
    }

    public Task RemoveRolesAsync(IEnumerable<ulong> roleIds, RequestOptions options = null)
    {
        return baseUser.RemoveRolesAsync(roleIds, options);
    }

    public Task RemoveRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
    {
        return baseUser.RemoveRolesAsync(roles, options);
    }

    public Task RemoveTimeOutAsync(RequestOptions options = null)
    {
        return baseUser.RemoveTimeOutAsync(options);
    }

    public Task SetTimeOutAsync(TimeSpan span, RequestOptions options = null)
    {
        return baseUser.SetTimeOutAsync(span, options);
    }
}
