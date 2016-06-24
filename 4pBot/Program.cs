using Autofac;
using Gtk;
using pBot.Dependencies;
using pBot.Model.ComunicateService;

namespace pBot
{
    internal class MainClass
    {
        public static void Main(string[] args)
        {
            var container = AutofacSetup.GetContainer();

            var xmpp = container.Resolve<IXmpp>();

            Application.Init();
            var window = new MainWindow(xmpp);

            window.Hide();
            Application.Run();
        }
    }
}