﻿using Gtk;
using pBot.Model.ComunicateService;

namespace pBot
{
    public partial class MainWindow : Window
    {
        public MainWindow() : base(WindowType.Toplevel)
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