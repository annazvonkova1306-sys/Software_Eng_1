using System.Collections.Generic;

namespace TxtToJsonConverter.Models
{
    public class Article
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public string Content { get; set; }
        public string HashCode { get; set; }
        
        public Article()
        {
            Authors = new List<string>();
        }
    }
}