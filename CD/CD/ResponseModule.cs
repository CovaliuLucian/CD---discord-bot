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
            var name = input.ToLower().Sanitize();
            var url = ImagesUrl.GetUrl(name);
            if (url == null)
            {
                await ReplyAsync("Image not found");
                return;
            }
           
            await Downloader.DownloadAsync(url, name);
            await Context.Channel.SendFileAsync($"images/{name}.png");
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
