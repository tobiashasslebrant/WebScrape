using System.Threading.Tasks;

namespace WebScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var asyncProgram = new AsyncProgram();
                await asyncProgram.Main(args);
            }).GetAwaiter().GetResult();
        }
    }
}