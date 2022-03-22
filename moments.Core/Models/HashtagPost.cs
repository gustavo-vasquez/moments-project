using System;

namespace moments.Core.Models
{
    public class HashtagPost
    {
        public int IdHashtag { get; set; }
        public Hashtag Hashtag { get; set; }
        public int IdPost { get; set; }
        public Post Post { get; set; }
    }
}