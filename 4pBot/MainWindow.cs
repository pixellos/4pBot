using System;
using System.Threading.Tasks;
using GLib;
using Gtk;
using pBot;
using Thread = System.Threading.Thread;

public partial class MainWindow : Gtk.Window
{
	private Xmpp xmpp;
	public MainWindow(Xmpp xmpp) : base(Gtk.WindowType.Toplevel)
	{
		this.xmpp = xmpp;

		Build();

	    button2.Clicked += (sender, args) => xmpp.StartWork();
	    this.Hidden += (sender, args) =>
	    {
	        Thread.Sleep(TimeSpan.FromSeconds(5));
	        xmpp.StartWork();
	    };
	}



	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}
}
