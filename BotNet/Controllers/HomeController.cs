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
        private static Task TaskHandler = null;
        private static CancellationTokenSource CancellationToken;

        public async Task<ActionResult> StartBot()
        {
            if (TaskHandler == null)
            {
                HomeController.CancellationToken = new CancellationTokenSource();
                HomeController.TaskHandler = MainClass.Start(CancellationToken);
                await TaskHandler;
            }
            else
            {
                MainClass.MessageToInvoke = xmpp => xmpp.Open();
                CancellationToken.Cancel();
            }

            return View("Index");
        }

        public ActionResult StopBot()
        {
            MainClass.MessageToInvoke = xmpp => xmpp.Close();
            CancellationToken.Cancel();
            return View("Index");
        }

        public ActionResult ChangeRoom()
        {
            MainClass.MessageToInvoke = xmpp => xmpp.ChangeRoom("General");
            CancellationToken.Cancel();
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