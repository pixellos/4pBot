using Autofac;
using Gtk;
using System.Threading.Tasks;
namespace pBot
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			IContainer container = pBot.AutofacSetup.GetContainer();

			var xmpp = container.Resolve<Xmpp>();
			Application.Init();

			MainWindow win = new MainWindow(xmpp);
            win.Hide();
			Application.Run();
		}
	}
}
