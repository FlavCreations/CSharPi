﻿
using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace csharpi
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        private readonly IConfiguration _config;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! I'm a discord bot!");
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        public Program()
        {
            _client = new DiscordSocketClient();

            //Hook into log event and write it out to the console
            _client.Log += LogAsync;

            //Hook into the client ready event
            _client.Ready += ReadyAsync;

            //Hook into the message received event, this is how we handle the hello world example
            _client.MessageReceived += MessageReceivedAsync;

            //Create the configuration
            var _builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile(path: "config.json");
            _config = _builder.Build();
        }

        public async Task MainAsync()
        {
            //This is where we get the Token value from the configuration file
            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            await _client.StartAsync();

            // Block the program until it is closed.
            await Task.Delay(-1);
        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private Task ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [] :)");
            return Task.CompletedTask;
        }

        //I wonder if there's a better way to handle commands (spoiler: there is :))
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            //This ensures we don't loop things by responding to ourselves (as the bot)
            if (message.Author.Id == _client.CurrentUser.Id)
                return;

            if (message.Content == ".hello")
            {
                await message.Channel.SendMessageAsync("world!");
            }
            else if (message.Channel.Id.Equals(614206030372667396) & message.Content.Equals("ping"))
            {
                await message.Channel.SendMessageAsync("pong!");
            }
            else if (message.Content.Equals("RAWR"))
            {
                await message.Channel.SendMessageAsync("RAWR!!!");
            }
            else if (message.Content.Equals("!website"))
            {
                await message.Channel.SendMessageAsync("https://www.flavcreations.com/");
            }
            else if (message.Content.Equals("!twitch"))
            {
                await message.Channel.SendMessageAsync("https://www.twitch.tv/flavcreations");
            }
            else if (message.Content.Equals("!github"))
            {
                await message.Channel.SendMessageAsync("https://github.com/Flavius-The-Person");
            }
            else if (message.Content.Equals("!patreon"))
            {
                await message.Channel.SendMessageAsync("");
            }
            else if (message.Content.Equals("!streamwarriors"))
            {
                await message.Channel.SendMessageAsync("https://github.com/Flavius-The-Person/stream-warriors-engine");
            }
            else if (message.Content.Equals("!legionslive"))
            {
                await message.Channel.SendMessageAsync("Legions Live Game Coming Soon!");
            }
        }
    }
}