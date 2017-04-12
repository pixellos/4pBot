using System.Threading;
using Autofac;
using _4PBot.Dependencies;

namespace _4PBot
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var token = new CancellationTokenSource();
            var context = new BotContext();
            context.Start(token).Wait();
        }
    }
}