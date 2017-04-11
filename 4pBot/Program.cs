using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using _4PBot.Dependencies;
using _4PBot.Model.ComunicateService;

namespace _4PBot
{
    public class MainClass
    {
        //Todo: To some queue?
        public static Action<IXmpp> MessageToInvoke;

        public static void Main(string[] args)
        {
            var token = new CancellationTokenSource();
            MainClass.Start(token).Wait();
        }

        public static Task Start(CancellationTokenSource cancellationTokenSourcetoken)
        {
            var token = cancellationTokenSourcetoken.Token;
            return Task.Run(async () =>
            {
                var container = AutofacSetup.GetContainer();
                var xmpp = container.Resolve<IXmpp>();
                var controller = container.Resolve<Controllers>();
                controller.ControllerInitialize();
                while (true)
                {
                    await Task.Delay(100);
                    if (token.IsCancellationRequested)
                    {
                        if (MainClass.MessageToInvoke != null)
                        {
                            MainClass.MessageToInvoke(xmpp);
                            MainClass.MessageToInvoke = null;
                        }
                    }
                }
            }, token);
        }
    }
}