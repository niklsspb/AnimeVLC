using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeVLC
{
    public class FanNaruto : ParserInterface
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
            var node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"MT_overroll\"]/div[2]");
            var n = node.SelectNodes("i");
            String key = "";
            String value = "";
            String player = "";
            List<Anime> animeArrayList = new List<Anime>();
            foreach (var nodes in n)
            {
                key = nodes.InnerText;
                value = nodes.GetAttributeValue("data-id", null);
                player = nodes.GetAttributeValue("data-p", null);
                Anime array = new Anime(key, player, value);
                animeArrayList.Add(array);
            }

            return animeArrayList;
        }

        public string getVideoUrl(string url)
        {
            if (url.Contains("sibnet"))
            {
                var html = url;
                HtmlWeb web = new HtmlWeb();
                web.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
                var htmlDoc = web.Load(html);
                string result = htmlDoc.Text;
                string prop = "";
                if (result.IndexOf("{src:") > 0)
                {
                    int start_index = result.IndexOf("{src:") + 7;
                    int end_index = result.IndexOf(", type:") - 1;
                    prop = result.Substring(start_index, end_index - start_index);
                    var ht = "http://video.sibnet.ru" + prop;
                    HtmlWeb htweb = new HtmlWeb();
                    htweb.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
                    var htDoc = htweb.Load(ht);
                    prop = htweb.ResponseUri.ToString();
                }

                return prop;
            }
            else
            {
                return null;
            }
        }
    }
}
