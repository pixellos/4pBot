using Autofac;
using Gtk;
using pBot.Dependencies;
using pBot.Model.ComunicateService;

namespace pBot
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			IContainer container = AutofacSetup.GetContainer();

			var xmpp = container.Resolve<IXmpp>();
            
            Application.Init();
            MainWindow window = new MainWindow(xmpp);

            window.Hide();
            Application.Run();
		}
	}
}
