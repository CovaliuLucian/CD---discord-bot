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
        [Alias("send help", "help me")]
        public async Task HelpAsync()
        {
            await ReplyAsync("Go fuck yourself!");
        }

        [Command("bang")]
        [Summary("bang (request from Mihai)")]
        public async Task BangAsync(SocketUser user = null)
        {
            if (user != null)
                await ReplyAsync($"{user.Username}, we'll bang, ok?");
            else
                await ReplyAsync("Nobody to bang? :sad:");

        }

        [Command("my id")]
        [Summary("shows your id")]
        public async Task MyIdAsync()
        {
            await ReplyAsync($"Your ID is {Context.User.Id}");
        }

        [Command("refresh")]
        [Summary("refresh")]
        public async Task RefreshAsync()
        {
            if (Context.User.Id == 196241129560211456)
            {
                await Downloader.DownloadImageListAsync();
                ImagesUrl.Read();
                await ReplyAsync("Refresh succesful!");
            }
            else
                await ReplyAsync("Only the admin can refresh the command list");
        }

        [Group("pls")]
        public class Sample : ModuleBase<SocketCommandContext>
        {
            [Command("fuck")]
            [Summary("Fuck that guy")]
            public async Task FuckAsync([Summary("The (optional) user")] SocketUser user = null, [Remainder]string mode = null)
            {
                var userInfo = user ?? Context.Client.CurrentUser;
                var msg = $"Fuck {userInfo.Username}";
                if (mode != null)
                    msg += $", but {mode}";
                await ReplyAsync(msg);
            }

            [Command("help")]
            [Summary("help")]
            [Alias("send help", "help me")]
            public async Task HelpAsync()
            {
                await ReplyAsync("Help is still in the works\nInvoke the bot by taging him at the start of the message(except the img command)\nCommands:\nimg for a reaction image (https://pastebin.com/NHc7u1w7)\ntext for making the text.. fancy ;)\npls fuck [tagged user] for..well, you can guess\nmore to come and document!");
            }
        }
    }
}