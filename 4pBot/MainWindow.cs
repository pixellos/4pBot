using Gtk;
using pBot.Model.ComunicateService;

namespace pBot
{
    public partial class MainWindow : Gtk.Window
    {
        private IXmpp xmpp;
        public MainWindow(IXmpp xmpp) : base(Gtk.WindowType.Toplevel)
        {
            Build();
        }

        protected void OnDeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
            a.RetVal = true;
        }
    }
}
