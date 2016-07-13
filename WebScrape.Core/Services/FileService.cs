using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WebScrape.Core.Models;

namespace WebScrape.Core.Services
{
    public interface IFileService
    {
        bool Exists(string fileName);
        string Read(string filePath);
        string UniqueFileName(CacheType cacheType, string path, int index);
        void Write(string filePath, string html);
    }

    public class FileService : IFileService
    {
        public bool Exists(string fileName)
            => File.Exists(fileName);

        public string UniqueFileName(CacheType cacheType, string path, int index)
            => $"cache\\{cacheType}_{Hash(path)}_{index}.html";

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