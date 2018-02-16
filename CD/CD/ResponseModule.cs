using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace CD
{
    public class ResponseModule : ModuleBase<SocketCommandContext>
    {
        [Command("say")]
        [Summary("Echos a message.")]
        [Alias("echo")]
        public async Task SayAsync([Remainder] [Summary("The text to echo")] string echo)
        {
            await ReplyAsync(echo);
        }

        [Command("img")]
        [Summary("Sends a reaction image")]
        public async Task ReactionAsync([Remainder] [Summary("The input")] string input)
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

        [Command("Hello")]
        [Summary("Echos a hello")]
        [Alias("Hi", "Greetings", "Hey")]
        public async Task HelloAsync()
        {
            await ReplyAsync("Hello " + Context.User.Username);
        }

        [Command("text")]
        [Summary("Makes text fancy")]
        [Alias("fancy", "fancy text")]
        public async Task FancyTextAsync([Remainder] string input)
        {
            await ReplyAsync(input.Fancy());
        }

        [Command("help")]
        [Summary("help")]
        [Alias("send help", "pls help", "help me")]
        public async Task HelpAsync()
        {
            await ReplyAsync("Go fuck yourself!");
        }

        [Group("pls")]
        public class Sample : ModuleBase<SocketCommandContext>
        {
            [Command("fuck")]
            [Summary("Fuck that guy")]
            public async Task FuckAsync([Summary("The (optional) user")] SocketUser user = null)
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                await ReplyAsync($"Fuck {userInfo.Username}");
            }
        }
    }
}