using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
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
            web.OverrideEncoding = Encoding.GetEncoding(1251);
            web.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
            string s =web.AutoDetectEncoding.ToString();
            var htmlDoc = web.Load(html);
            
            var node_list = htmlDoc.DocumentNode.SelectSingleNode("//span[contains(., 'Sibnet')]");
            var n = node_list.ParentNode.GetAttributeValue("onclick", null);
            htmlDoc.Save("test.html", Encoding.UTF8);
            List<Anime> l = new List<Anime>();
            return l;
        }

        public string getVideoUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}
