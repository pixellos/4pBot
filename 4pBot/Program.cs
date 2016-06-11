using System;
using Autofac;
using Gtk;
using Matrix.Xmpp.Client;
namespace pBot
{
	class MainClass
	{

		public static void Main(string[] args)
		{
			IContainer container = pBot.AutofacSetup.GetContainer();
			Application.Init();
			MainWindow win = new MainWindow();
			win.Show();

			XmppClient client = new XmppClient();

			Application.Run();
		}
	}
}
