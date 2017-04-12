using System;
using System.Threading;
using System.Threading.Tasks;
using _4PBot.Model.ComunicateService;
using System.Collections.Generic;
using _4PBot.Model.Functions;
using CoreBot;
using _4PBot.Model.Facades;
using _4PBot.Model.Functions.HighLevel;
using System.Linq;

namespace _4PBot
{
    public class BotContext
    {
        public Queue<Action<IXmpp>> MessageToInvoke = new Queue<Action<IXmpp>>();
        private List<ICommand> iListImplementations = new List<ICommand>();
        public IXmpp IXmpp { get; private set; }
        private IXmpp Build()
        {
            var actions = new Actions();
            var repeater = new Repeater();
            var soDownloader = new Model.Functions.StackOverflow.Downloader();
            var pDownloader = new Model.Functions._4Programmers.Downloader();
            var soChecker = new Model.Functions.StackOverflow.Checker(soDownloader);
            var pChecker = new Model.Functions._4Programmers.Checker(pDownloader);
            var xmpp = new XmppFreeWithCommandsHandling(actions);
            List<ICommand> iListImplementations = new List<ICommand>
            {
                new Messages(),
                new StackOverflow(repeater, soChecker),
                new _4Programmers(repeater, pChecker),
                new ActionsHelp(actions),
                xmpp
            };
            foreach (var item in iListImplementations.SelectMany(x => x.Actions))
            {
                actions.Add(item.Key, item.Value);
            }
            return xmpp;
        }

        public Task Start(CancellationTokenSource cancellationTokenSourcetoken)
        {
            var token = cancellationTokenSourcetoken.Token;
            if (this.IXmpp != null)
            {
                throw new Exception("App cant be started multiple times.");
            }
            var xmpp = this.Build();
            this.IXmpp = xmpp;
            return Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(100);
                    if (this.MessageToInvoke != null)
                    {
                        if (this.MessageToInvoke.Count > 0)
                        {
                            var msg = this.MessageToInvoke.Dequeue();
                            msg(xmpp);
                        }
                    }
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }, token);
        }
    }
}