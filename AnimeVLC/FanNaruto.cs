using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeVLC
{
    public class FanNaruto : ParserInterface
    {
        public List<string> xpathList = new List<string>() { "//*[@id=\"MT_overroll\"]/div[2]", "//*[@id=\"tabs5\"]/div[2]", "//span[contains(@class,\"ep3\")]" };
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
            string html = url;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument htmlDocument = web.Load(html);
            HtmlNodeCollection nodeCollection = checkXpath(htmlDocument, xpathList);

            HtmlNode singleNode = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"MT_overroll\"]/div[2]");
            /* TODO переделать процесс чтобы проверялось single node или node collection и возвращалось 
             * в зависимости от того что в итоге получено */
            if (singleNode == null)
            {
                singleNode = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"tabs5\"]/div[2]");
                if (singleNode == null)
                {
                    htmlDocument.Save("debug.html", Encoding.UTF8);
                    throw new ArgumentNullException("singleNode");
                }
                              
            }
            HtmlNodeCollection nodeCollection = singleNode.SelectNodes("i");
            String key = "";
            String value = "";
            String player = "";
            List<Anime> animeItemsList = new List<Anime>();
            foreach (var node in nodeCollection)
            {
                key = node.InnerText;
                value = node.GetAttributeValue("data-id", null);
                player = node.GetAttributeValue("data-p", null);
                Anime animeItems = new Anime(key, player, value);
                animeItemsList.Add(animeItems);
            }

            return animeItemsList;
        }

        public HtmlNodeCollection checkXpath (HtmlDocument htmlDocument, List<String> xpathList)
        {
            HtmlNodeCollection list = null;
            foreach (var xpath in xpathList)
            {
                list = htmlDocument.DocumentNode.SelectNodes(xpath);
                if (list != null)
                {
                    break;
                }
            }
            
            return list;
        }

        public string getVideoUrl(string url)
        {
            if (url.Contains("sibnet"))
            {
                var html = url;
                HtmlWeb web = new HtmlWeb();
                web.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
                HtmlDocument htmlDocument = web.Load(html);
                string sourceCodeHtmlDocument = htmlDocument.Text;
                string path_to_source_video = "";
                if (sourceCodeHtmlDocument.IndexOf("{src:") > 0)
                {
                    int start_index_string_to_url_video = sourceCodeHtmlDocument.IndexOf("{src:") + 7;
                    int end_index_string_to_url_video = sourceCodeHtmlDocument.IndexOf(", type:") - 1;
                    path_to_source_video = sourceCodeHtmlDocument.Substring(start_index_string_to_url_video, end_index_string_to_url_video - start_index_string_to_url_video);
                    var prepared_link_to_get_final_link_video = "http://video.sibnet.ru" + path_to_source_video;
                    HtmlWeb htmlWeb = new HtmlWeb();
                    htmlWeb.UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
                    var htmlDocument_request_to_final_link = htmlWeb.Load(prepared_link_to_get_final_link_video);
                    path_to_source_video = htmlWeb.ResponseUri.ToString();
                }

                return path_to_source_video;
            }
            else
            {
                return null;
            }
        }
    }
}
