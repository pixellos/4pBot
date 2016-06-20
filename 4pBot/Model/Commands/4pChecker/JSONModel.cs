﻿using System.Collections.Generic;

namespace pBot.Model.Commands._4pChecker
{
    //Generated by tool.

    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public object photo_url { get; set; }
        public int reputation { get; set; }
    }

    public class FirstPost
    {
        public int id { get; set; }
        public int time { get; set; }
        public User user { get; set; }
    }

    public class User2
    {
        public int id { get; set; }
        public string name { get; set; }
        public string photo_url { get; set; }
        public int reputation { get; set; }
    }

    public class LastPost
    {
        public int id { get; set; }
        public int time { get; set; }
        public User2 user { get; set; }
    }

    public class Main
    {
        public int topic_id { get; set; }
        public FirstPost first_post { get; set; }
        public LastPost last_post { get; set; }
        public string forum { get; set; }
        public string subject { get; set; }
        public int timestamp { get; set; }
        public int sticky { get; set; }
        public int locked { get; set; }
        public object solved { get; set; }
        public int replies { get; set; }
        public int announcement { get; set; }
        public int votes { get; set; }
        public List<string> tags { get; set; }
    }

    public class RootObject
    {
        public List<Main> Main { get; set; }
    }


}
