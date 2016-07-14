using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using pBot.Dependencies;
using pBot.Model.ComunicateService;

namespace pBot
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            var container = AutofacSetup.GetContainer();

            var xmpp = container.Resolve<IXmpp>();
            var controller = container.Resolve<Controllers>();

            controller.ControllerInitialize();

            while (true)
            {

            }
        }

        public static Action<IXmpp> invokeMessage;
        
        public static Task GetTask(CancellationTokenSource cancellationTokenSourcetoken)
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
                        if (invokeMessage != null)
                        {
                            invokeMessage(xmpp);
                            invokeMessage = null;
                        }
                    }
                }

            }
            ,token);

            
        }
    }
}