using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

namespace CD
{
    public class Program
    {
        private CommandService _commands;
        private DiscordSocketClient _client;
        private IServiceProvider _services;

        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            var token = new Token();

            _services = new ServiceCollection().AddSingleton(_client).AddSingleton(_commands).BuildServiceProvider();

            _client.Log += Logger;

            await InstallCommandsAsync();
            await _commands.AddModulesAsync(Assembly.GetAssembly(typeof(ResponseModule)));

            await _client.LoginAsync(TokenType.Bot, token.GetToken());
            await _client.StartAsync();
            

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        public async Task InstallCommandsAsync()
        {
            // Hook the MessageReceived Event into our Command Handler
            _client.MessageReceived += HandleCommandAsync;
            // Discover all of the commands in this assembly and load them.
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a System Message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;
            // Create a number to track where the prefix ends and the command begins
            var argPos = 0;
            var dummyPos = 0;
            // Determine if the message is a command, based on if it starts with '!' or a mention prefix
            if (!message.HasMentionPrefix(_client.CurrentUser, ref argPos) && !message.HasStringPrefix("img", ref dummyPos)) return;
            // Create a Command Context
            var context = new SocketCommandContext(_client, message);
            // Execute the command. (result does not indicate a return value, 
            // rather an object stating if the command executed successfully)
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (!result.IsSuccess)
                await context.Channel.SendMessageAsync(result.ErrorReason);
        }


        private static Task Logger(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.FromResult(0);
        }
    }

}
