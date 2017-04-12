using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace _4PBot.Model.Functions._4Programmers
{
    //Tool generated
    [Serializable]
    public class Rootobject
    {
        public Post[] Posts { get; set; }
    }

    [Serializable]
    public class Post
    {
        public string topic_id { get; set; }
        public string post_id { get; set; }
        public string forum { get; set; }
        public string subject { get; set; }
        public string timestamp { get; set; }
        public string votes { get; set; }
        public object[] tags { get; set; }
    }
}