using System;
using System.IO;
using System.Threading.Tasks;
using Discord;
using Discord.Audio.Streams;
using Discord.Commands;
using Discord.WebSocket;

namespace CD
{
    public class ResponseModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echos a message.")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("img")]
        [Summary("Echos a message.")]
        public async Task ReactionAsync([Remainder] [Summary("The text to echo")] string input)
        {
            if (input.ToUpper() == "NANI?!")
            {
                if(!File.Exists("images/nani.jpg"))
                    await Downloader.DownloadAsync("https://media.discordapp.net/attachments/408242513141563395/408255825497292811/nani.jpg?width=907&height=511", "nani");
                await Context.Channel.SendFileAsync("images/nani.jpg");
            }
        }

        [Group("pls")]
        public class Sample : ModuleBase<SocketCommandContext>
        {
            [Command("fuck")]
            [Summary("Fuck that guy")]
            [Alias("user", "whois")]
            public async Task UserInfoAsync([Summary("The (optional) user")] SocketUser user = null)
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"Fuck {userInfo.Username}");
            }
        }

        

    }
}
