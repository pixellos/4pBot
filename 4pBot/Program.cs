using System.Threading.Tasks;
using Autofac;
using Microsoft.CodeAnalysis.CSharp;
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
        }

        public static async Task GetTask()
        {

            Task task = new Task(() =>
            {
                var container = AutofacSetup.GetContainer();

                var xmpp = container.Resolve<IXmpp>();
                var controller = container.Resolve<Controllers>();

                controller.ControllerInitialize();
            });
            await task;
            
        }
    }
}