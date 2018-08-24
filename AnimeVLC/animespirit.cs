using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeVLC
{
    public class animespirit : ParserInterface
    {
        public string url
        {
            get
            {
                return url;
            }

            set
            {
                url = value;
            }
        }

        public List<Anime> getUrl(string url)
        {
            var html = url;
            HtmlWeb web = new HtmlWeb();
            var htmlDoc = web.Load(html);

            throw new NotImplementedException();
        }

        public string getVideoUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}
