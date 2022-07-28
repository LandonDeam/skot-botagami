// <copyright file="GuildUser.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

/// <summary>
/// Class used for extending off of the IGuildUser class for personal use.
/// </summary>
public class GuildUser : IGuildUser
{
    private IGuildUser baseUser;
    private ulong balance;

    /// <summary>
    /// Initializes a new instance of the <see cref="GuildUser"/> class.
    /// </summary>
    /// <param name="user">The guild user to use for constructing the object.</param>
    public GuildUser(IGuildUser user)
    {
        // Initialize IGuildUser interface
        this.baseUser = user;

        this.balance = BalanceManager.GetGuildUserBalance(user);
    }

    // Implementation of the base IGuildUser interface

    /// <inheritdoc/>
    public DateTimeOffset? JoinedAt => this.baseUser.JoinedAt;

    /// <inheritdoc/>
    public string DisplayName => this.baseUser.DisplayName;

    /// <inheritdoc/>
    public string Nickname => this.baseUser.Nickname;

    /// <inheritdoc/>
    public string DisplayAvatarId => this.baseUser.DisplayAvatarId;

    /// <inheritdoc/>
    public string GuildAvatarId => this.baseUser.GuildAvatarId;

    /// <inheritdoc/>
    public GuildPermissions GuildPermissions => this.baseUser.GuildPermissions;

    /// <inheritdoc/>
    public IGuild Guild => this.baseUser.Guild;

    /// <inheritdoc/>
    public ulong GuildId => this.baseUser.GuildId;

    /// <inheritdoc/>
    public DateTimeOffset? PremiumSince => this.baseUser.PremiumSince;

    /// <inheritdoc/>
    public IReadOnlyCollection<ulong> RoleIds => this.baseUser.RoleIds;

    /// <inheritdoc/>
    public bool? IsPending => this.baseUser.IsPending;

    /// <inheritdoc/>
    public int Hierarchy => this.baseUser.Hierarchy;

    /// <inheritdoc/>
    public DateTimeOffset? TimedOutUntil => this.baseUser.TimedOutUntil;

    /// <inheritdoc/>
    public string AvatarId => this.baseUser.AvatarId;

    /// <inheritdoc/>
    public string Discriminator => this.baseUser.Discriminator;

    /// <inheritdoc/>
    public ushort DiscriminatorValue => this.baseUser.DiscriminatorValue;

    /// <inheritdoc/>
    public bool IsBot => this.baseUser.IsBot;

    /// <inheritdoc/>
    public bool IsWebhook => this.baseUser.IsWebhook;

    /// <inheritdoc/>
    public string Username => this.baseUser.Username;

    /// <inheritdoc/>
    public UserProperties? PublicFlags => this.baseUser.PublicFlags;

    /// <inheritdoc/>
    public DateTimeOffset CreatedAt => this.baseUser.CreatedAt;

    /// <inheritdoc/>
    public ulong Id => this.baseUser.Id;

    /// <inheritdoc/>
    public string Mention => this.baseUser.Mention;

    /// <inheritdoc/>
    public UserStatus Status => this.baseUser.Status;

    /// <inheritdoc/>
    public IReadOnlyCollection<ClientType> ActiveClients => this.baseUser.ActiveClients;

    /// <inheritdoc/>
    public IReadOnlyCollection<IActivity> Activities => this.baseUser.Activities;

    /// <inheritdoc/>
    public bool IsDeafened => this.baseUser.IsDeafened;

    /// <inheritdoc/>
    public bool IsMuted => this.baseUser.IsMuted;

    /// <inheritdoc/>
    public bool IsSelfDeafened => this.baseUser.IsSelfDeafened;

    /// <inheritdoc/>
    public bool IsSelfMuted => this.baseUser.IsSelfMuted;

    /// <inheritdoc/>
    public bool IsSuppressed => this.baseUser.IsSuppressed;

    /// <inheritdoc/>
    public IVoiceChannel VoiceChannel => this.baseUser.VoiceChannel;

    /// <inheritdoc/>
    public string VoiceSessionId => this.baseUser.VoiceSessionId;

    /// <inheritdoc/>
    public bool IsStreaming => this.baseUser.IsStreaming;

    /// <inheritdoc/>
    public bool IsVideoing => this.baseUser.IsVideoing;

    /// <inheritdoc/>
    public DateTimeOffset? RequestToSpeakTimestamp => this.baseUser.RequestToSpeakTimestamp;

    /// <inheritdoc/>
    public Task AddRoleAsync(ulong roleId, RequestOptions options = null)
    {
        return this.baseUser.AddRoleAsync(roleId, options);
    }

    /// <inheritdoc/>
    public Task AddRoleAsync(IRole role, RequestOptions options = null)
    {
        return this.baseUser.AddRoleAsync(role, options);
    }

    /// <inheritdoc/>
    public Task AddRolesAsync(IEnumerable<ulong> roleIds, RequestOptions options = null)
    {
        return this.baseUser.AddRolesAsync(roleIds, options);
    }

    /// <inheritdoc/>
    public Task AddRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
    {
        return this.baseUser.AddRolesAsync(roles, options);
    }

    /// <inheritdoc/>
    public Task<IDMChannel> CreateDMChannelAsync(RequestOptions options = null)
    {
        return this.baseUser.CreateDMChannelAsync(options);
    }

    /// <inheritdoc/>
    public string GetAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return this.baseUser.GetAvatarUrl(format, size);
    }

    /// <inheritdoc/>
    public string GetDefaultAvatarUrl()
    {
        return this.baseUser.GetDefaultAvatarUrl();
    }

    /// <inheritdoc/>
    public string GetDisplayAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return this.baseUser.GetDisplayAvatarUrl(format, size);
    }

    /// <inheritdoc/>
    public string GetGuildAvatarUrl(ImageFormat format = ImageFormat.Auto, ushort size = 128)
    {
        return this.baseUser.GetGuildAvatarUrl(format, size);
    }

    /// <inheritdoc/>
    public ChannelPermissions GetPermissions(IGuildChannel channel)
    {
        return this.baseUser.GetPermissions(channel);
    }

    /// <inheritdoc/>
    public Task KickAsync(string reason = null, RequestOptions options = null)
    {
        return this.baseUser.KickAsync(reason, options);
    }

    /// <inheritdoc/>
    public Task ModifyAsync(Action<GuildUserProperties> func, RequestOptions options = null)
    {
        return this.baseUser.ModifyAsync(func, options);
    }

    /// <inheritdoc/>
    public Task RemoveRoleAsync(ulong roleId, RequestOptions options = null)
    {
        return this.baseUser.RemoveRoleAsync(roleId, options);
    }

    /// <inheritdoc/>
    public Task RemoveRoleAsync(IRole role, RequestOptions options = null)
    {
        return this.baseUser.RemoveRoleAsync(role, options);
    }

    /// <inheritdoc/>
    public Task RemoveRolesAsync(IEnumerable<ulong> roleIds, RequestOptions options = null)
    {
        return this.baseUser.RemoveRolesAsync(roleIds, options);
    }

    /// <inheritdoc/>
    public Task RemoveRolesAsync(IEnumerable<IRole> roles, RequestOptions options = null)
    {
        return this.baseUser.RemoveRolesAsync(roles, options);
    }

    /// <inheritdoc/>
    public Task RemoveTimeOutAsync(RequestOptions options = null)
    {
        return this.baseUser.RemoveTimeOutAsync(options);
    }

    /// <inheritdoc/>
    public Task SetTimeOutAsync(TimeSpan span, RequestOptions options = null)
    {
        return this.baseUser.SetTimeOutAsync(span, options);
    }
}
