
using System;
using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Timers;

namespace csharpi
{
    class Program
    {
        private readonly DiscordSocketClient _client;
        private static SocketChannel chnl;
        private readonly IConfiguration _config;
        private string prefix;
        private IMessageChannel timerChan;
        private static System.Timers.Timer testTimer;

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
            _client.Connected += ConnectedAsync;
        }

        public async Task MainAsync()
        {
            //This is where we get the Token value from the configuration file
            await _client.LoginAsync(TokenType.Bot, _config["Token"]);
            prefix = _config["Prefix"];
            await _client.StartAsync();


            ulong srvrid = 264439640474386433;
            ulong chnlid = 614206030372667396;
            chnl = _client.GetChannel(chnlid);
            //chnl = (ITextChannel)_client.GetChannel(chnlid);
            //.GetTextChannel(chnlid);
            //await _client.GetGuild(srvrid).GetChannel(chnlid).SendMessageAsync();
            await _client.GetGuild(srvrid).GetTextChannel(chnlid).SendMessageAsync("loaded test message");

            Console.WriteLine("should have fired channel message");
            /*.Ready += async e =>
            {

                ulong chnlid = 614206030372667396;
                chnl = await discord.GetChannelAsync(chnlid);
                await discord.SendMessageAsync(chnl, "loaded test message", false, null);
            };*/

            // Block the program until it is closed.
            await Task.Delay(-1);
        }

        public async Task ConnectedAsync()
        {
            //_client.
            //await chnl.SendMessageAsync("Testing Message Sent On Load");
            //await chnl.create
            //ulong serverID = 264439640474386433;
            //ulong channelID = 614206030372667396; 
            //await _client.GetGuild(264439640474386433).GetTextChannel(614206030372667396).SendMessageAsync("Testing Text On Load.");

            //Console.WriteLine("timer went off for channel : " + timerChan + " ||  ");
            //await _client.GetGuild(serverID).GetChannel(channelID).SendMessageAsync("Testing timer");


        }

        private Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private async Task<Task> ReadyAsync()
        {
            Console.WriteLine($"Connected as -> [] :)");

            ulong srvrid = 264439640474386433;
            ulong chnlid = 614206030372667396;
            chnl = _client.GetChannel(chnlid);
            await _client.GetGuild(srvrid).GetTextChannel(chnlid).SendMessageAsync("loaded test message");

            return Task.CompletedTask;
            
        }

        //I wonder if there's a better way to handle commands (spoiler: there is :))
        private async Task MessageReceivedAsync(SocketMessage message)
        {
            //This ensures we don't loop things by responding to ourselves (as the bot)
            if (message.Author.Id == _client.CurrentUser.Id)
                return;

            if (message.Content == prefix + "hello")
            {
                await message.Channel.SendMessageAsync("world!");
            }
            else if (message.Channel.Id.Equals(614206030372667396) & message.Content.Equals(prefix + "ping"))
            {
                await message.Channel.SendMessageAsync("pong!");
            }
            else if (message.Content.Equals(prefix + "RAWR"))
            {
                await message.Channel.SendMessageAsync("RAWR!!!");
            }
            else if (message.Content.Equals(prefix + "website"))
            {
                await message.Channel.SendMessageAsync("https://www.flavcreations.com/");
            }
            else if (message.Content.Equals(prefix + "twitch"))
            {
                await message.Channel.SendMessageAsync("https://www.twitch.tv/flavcreations");
            }
            else if (message.Content.Equals(prefix + "github"))
            {
                await message.Channel.SendMessageAsync("https://github.com/Flavius-The-Person");
            }
            /*else if (message.Content.Equals( prefix+"patreon"))
            {
                await message.Channel.SendMessageAsync("");
            }*/
            else if (message.Content.Equals(prefix + "streamwarriors"))
            {
                await message.Channel.SendMessageAsync("I made my own engine in java and almost released it! I decided working with a framework developed for games would be more beneficial so I swapped to monogame." +
                    " Here is the repo for the stream warriors engine built in native java - https://github.com/Flavius-The-Person/stream-warriors-engine");
            }
            else if (message.Content.Equals(prefix + "roleplaygamelive") || message.Content.Equals(prefix + "rpglive") || message.Content.Equals(prefix + "rpg"))
            {
                await message.Channel.SendMessageAsync("I'm working on a new RPG which is mostly meant to be played as a stream game for both the streamer and the viewers! More information coming soon.");
            }

        }
    }
}