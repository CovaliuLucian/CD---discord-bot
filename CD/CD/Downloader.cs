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
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            var inputStream = await response.Content.ReadAsStreamAsync();
            var image = Image.Load(inputStream);
            if (image != null)
            {
                Stream outputStream = new MemoryStream();
                image.SaveAsJpeg(outputStream); /*Saves the cat image as jpg (you can change this)*/
                outputStream.Position = 0;
                var file = File.Create($"images/{name}.jpg"); /*Saves it with the random string*/
                await outputStream.CopyToAsync(file);
                file.Dispose();           
            }
        }
    }
}
