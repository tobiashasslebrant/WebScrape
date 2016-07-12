using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebScrape.Core
{
    public class FileService
    {
        public void WriteToDisk(CacheType cacheType, string path, string html, int index)
        {
            Directory.CreateDirectory("cache");
            var filePath = $"cache\\{cacheType}_{Hash(path)}_{index}.html";
            File.WriteAllText(filePath, html, Encoding.UTF8);
        }

        public string ReadFromDisk(CacheType cacheType, string path, int index)
        {
            var filePath = $".\\cache\\{cacheType}_{Hash(path)}_{index}.html";
            if (!File.Exists(filePath))
                throw new Exception($"could not find a matching file in cache for path {filePath}");
            return File.ReadAllText(filePath);
        }

        public string ReadAllText(string path) => File.ReadAllText(Path.GetFullPath(path));

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