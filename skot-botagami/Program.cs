﻿// <copyright file="Program.cs" company="Landon Deam">
// Copyright (c) Landon Deam. All rights reserved.
// </copyright>

namespace SkotBotagami
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Discord;
    using Discord.Interactions;
    using Discord.WebSocket;

    /// <summary>
    /// Main class for the bot.
    /// </summary>
    internal class Program
    {
        private readonly IServiceProvider services;
        private DiscordSocketClient client;
        private InteractionService commands;

        /// <summary>
        /// Main function to start the bot.
        /// </summary>
        /// <param name="args">Additional Command line args, currently unused.</param>
        public static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        /// <summary>
        /// Runs the bot.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task RunBotAsync()
        {
            this.client = new DiscordSocketClient();

            this.commands = new InteractionService(this.client);

            string token = Environment.GetEnvironmentVariable("TOKEN");

            this.client.Log += this.Client_Log;

            this.client.ReactionAdded += ReactionEventHandler.ReactionAdded;

            this.client.ButtonExecuted += ButtonInteractionHandler.OnButtonExecution;

            await this.RegisterCommandsAsync();

            await this.commands.AddModuleAsync<Modules.Commands>(services: null);

            await this.client.LoginAsync(TokenType.Bot, token);

            LoggingService log = new (this.client, this.commands);

            BalanceManager.OpenDatabase();

            Funcs.SetRandom();

            await this.client.StartAsync();

            await Task.Delay(-1);
        }
        
        /// <summary>
        /// Registers the commands.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        public async Task RegisterCommandsAsync()
        {
            this.client.SlashCommandExecuted += this.SlashCommandHandler;
            await this.commands.AddModulesAsync(Assembly.GetEntryAssembly(), this.services);
        }

        private Task Client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        private async Task SlashCommandHandler(SocketSlashCommand command)
        {
            if (command.User.IsBot)
            {
                return;
            }

            await this.commands.ExecuteCommandAsync((IInteractionContext)command, this.services);
        }

    }
}
