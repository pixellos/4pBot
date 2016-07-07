using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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
                CancellationToken = new CancellationTokenSource();

                TaskHandler = pBot.MainClass.GetTask(CancellationToken);
                
                await TaskHandler;
            }
            else
            {
                pBot.MainClass.invokeMessage = xmpp => xmpp.Open();
                CancellationToken.Cancel();
            }

            return View("Index");
        }

        public ActionResult StopBot()
        {
            pBot.MainClass.invokeMessage = xmpp => xmpp.Close();
            CancellationToken.Cancel();
            return View("Index");
        }

        public ActionResult ChangeRoom()
        {
            pBot.MainClass.invokeMessage = xmpp => xmpp.ChangeRoom("General");
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