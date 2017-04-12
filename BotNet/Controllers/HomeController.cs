using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using _4PBot;

namespace BotNet.Controllers
{
    public class HomeController : Controller
    {
        private static BotContext Context;
        private static Task TaskHandler;
        private static CancellationTokenSource CancellationToken;

        public async Task<ActionResult> StartBot()
        {
            if (HomeController.Context == null)
            {
                var context = new BotContext();
                HomeController.CancellationToken = new CancellationTokenSource();
                HomeController.TaskHandler = context.Start(CancellationToken);
                await HomeController.TaskHandler;
            }
            else
            {
                HomeController.Context.MessageToInvoke.Enqueue(xmpp => xmpp.Open());
            }
            return View("Index");
        }

        public ActionResult StopBot()
        {
            HomeController.Context.MessageToInvoke.Enqueue(xmpp => xmpp.Close());
            CancellationToken.Cancel();
            return View("Index");
        }

        public ActionResult ChangeRoom()
        {
            HomeController.Context.MessageToInvoke.Enqueue(xmpp => xmpp.ChangeRoom("General"));
            return View("Index");
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}