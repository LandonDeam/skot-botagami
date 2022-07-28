using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace skot_botagami
{
    class Program
    {
        static void Main(String[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            //string token = _config["token"]; // use this line instead of the one under it to run the bot locally
            string token = Environment.GetEnvironmentVariable("TOKEN");

            _client.Log += _client_Log;

            await RegisterCommandsAsync();

            await _client.LoginAsync(TokenType.Bot, token);

            LoggingService log = new(_client, _commands);

            BalanceManager.openDatabase();
            
            await _client.StartAsync();

            await Task.Delay(-1);

        }

        private Task _client_Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.CompletedTask;
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            SocketUserMessage message = arg as SocketUserMessage;
            SocketCommandContext context = new SocketCommandContext(_client, message);
            if (message.Author.IsBot) return;

            int argPos = 0;
            if (message.HasStringPrefix(".", ref argPos))
            {
                IResult result = await _commands.ExecuteAsync(context, argPos, _services);
                if (!result.IsSuccess) Console.WriteLine(result.ErrorReason);
            }
            /*else if(message.Content.Contains("188362544283516928"))
            {
                await message.ReplyAsync("https://cdn.discordapp.com/attachments/450511755391533057/877014295693651988/madshinji.gif");
            }*/
        }
    }
}
