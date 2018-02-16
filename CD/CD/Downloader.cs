using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace CD
{
    public static class Downloader
    {
        public static async Task DownloadAsync(string url, string name)
        {
            if (File.Exists($"images/{name}.png")) return;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var inputStream = await response.Content.ReadAsStreamAsync();
            var image = Image.Load(inputStream);
            if (image != null)
            {
                Stream outputStream = new MemoryStream();
                image.SaveAsPng(outputStream);
                outputStream.Position = 0;
                var file = File.Create($"images/{name}.png"); 
                await outputStream.CopyToAsync(file);
                file.Dispose();           
            }
        }

        public static async Task DownloadImageListAsync()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://pastebin.com/raw/NHc7u1w7");
            var inputStream = await response.Content.ReadAsStreamAsync();
            var fileStream = File.Create($"images/images.txt");
            inputStream.Seek(0, SeekOrigin.Begin);
            inputStream.CopyTo(fileStream);
            fileStream.Close();
        }
    }
}
