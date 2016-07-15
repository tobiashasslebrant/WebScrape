using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebScrape.Core.Models;

namespace WebScrape.Core.Services
{
    public interface IFileService
    {
        string UniqueFileName(CacheType cacheType, string path);
        Task<bool> ExistsAsync(string fileName);
        Task<string> ReadAsync(string filePath);
        Task WriteAsync(string filePath, string text);
    }

    public class FileService : IFileService
    {
        public async Task<bool> ExistsAsync(string fileName)
        {
            var exists = await Task.Run(() => File.Exists(fileName));
            return exists;
        }

        public string UniqueFileName(CacheType cacheType, string path)
            => $"cache\\{cacheType}_{Hash(path)}.html";

        public void Write(string filePath, string html)
        {
            if(!Directory.Exists("cache"))
                Directory.CreateDirectory("cache");

           File.WriteAllText(filePath, html, Encoding.UTF8);
        }
        
        public string Read(string filePath)
        {
            if (!File.Exists(filePath))
                throw new Exception($"could not find a matching file in cache for path {filePath}");

            return File.ReadAllText(filePath);
        }

        public async Task<string> ReadAsync(string filePath)
        {
            var exists = await ExistsAsync(filePath);
            if(!exists)
                return $"ERROR: Could not find a matching file in cache for path {filePath}";

            using (var sourceStream = new FileStream(filePath,FileMode.Open, FileAccess.Read, FileShare.Read,
                bufferSize: 4096, useAsync: true))
            {
                var sb = new StringBuilder();
                var buffer = new byte[0x1000];
                int numRead;
                while ((numRead = await sourceStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    var text = Encoding.UTF8.GetString(buffer, 0, numRead);
                    sb.Append(text);
                }

                return sb.ToString();
            }
        }
        public async Task WriteAsync(string filePath, string text)
        {
            var exists = await Task.Run(() => Directory.Exists("cache"));
            if(!exists)
                Directory.CreateDirectory("cache");

            var encodedText = Encoding.UTF8.GetBytes(text);

            using (var sourceStream = new FileStream(filePath,FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
            {
                await sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
            }
        }

        string Hash(string str)
        {
            var md5Hash = MD5.Create();
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }
    }
}