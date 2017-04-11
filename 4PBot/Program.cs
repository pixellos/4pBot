using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using _4PBot.Dependencies;
using _4PBot.Model.ComunicateService;
using System.Collections.Generic;

namespace _4PBot
{
    public class MainClass
    {
        //Todo: To some queue?
        public static Queue<Action<IXmpp>> MessageToInvoke = new Queue<Action<IXmpp>>();

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
                while (true)
                {
                    await Task.Delay(100);
                    if (token.IsCancellationRequested)
                    {
                        if (MainClass.MessageToInvoke != null)
                        {
                            if (MainClass.MessageToInvoke.Count > 0)
                            {
                                var msg = MainClass.MessageToInvoke.Dequeue();
                                msg(xmpp);
                            }
                        }
                    }
                }
            }, token);
        }
    }
}