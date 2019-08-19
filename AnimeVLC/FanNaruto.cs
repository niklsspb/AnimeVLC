using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimeVLC
{
    public class FanNaruto : ParserInterface
    {
        public List<string> xpathList = new List<string>() { "//*[@id=\"MT_overroll\"]/div[2]", "//*[@id=\"tabs5\"]/div[2]", "//*[@class =\"lips\"]" };
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
            List<Anime> animeItemsList = null;
            String key = "";
            String value = "";
            String player = "";
            
            /*HtmlNodeCollection nodeCollectionTest = checkXpath(htmlDocument, xpathList);
            //var check = nodeCollectionTest.LongCount();
            if (nodeCollectionTest.LongCount()>1)
            {
                animeItemsList = new List<Anime>();
                foreach (var node in nodeCollectionTest)
                {
                    if (node.GetAttributeValue("class", null) == "sub")
                    {
                        key = node.InnerText;
                        value = node.GetAttributeValue("data-id", null);
                        player = node.GetAttributeValue("data-p", null);
                        Anime animeItems = new Anime(key, player, value);
                        animeItemsList.Add(animeItems);
                    }
                }
            }*/
            //HtmlNode singleNode = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"MT_overroll\"]/div[2]");
            /* TODO переделать процесс чтобы проверялось single node или node collection и возвращалось 
             * в зависимости от того что в итоге получено */
            /*if (singleNode == null)
            {
                singleNode = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"tabs5\"]/div[2]");
                if (singleNode == null)
                {
                    htmlDocument.Save("debug.html", Encoding.UTF8);
                    throw new ArgumentNullException("singleNode");
                }
                              
            }*/
            if (url.Contains("van_pis"))
            {
                HtmlNode singleNode = checkXpath(htmlDocument, xpathList);
                HtmlNodeCollection nodeCollection = singleNode.SelectNodes("i");
                animeItemsList = new List<Anime>();
                foreach (var node in nodeCollection)
                {
                        key = node.InnerText;
                        value = node.GetAttributeValue("data-id", null);
                        player = node.GetAttributeValue("data-p", null);
                        Anime animeItems = new Anime(key, player, value);
                        animeItemsList.Add(animeItems);
                }
            }
            else
            {
                HtmlNode singleNode = checkXpath(htmlDocument, xpathList);
                HtmlNodeCollection nodeCollection = singleNode.SelectNodes("i[@class=\"sub\"]|span[@class=\"ep3\"]");
                animeItemsList = new List<Anime>();
                string SeriesName = null;
                foreach (var node in nodeCollection)
                {
                    if (node.GetAttributeValue("class",null) == "ep3")
                    {
                        if (node.InnerText!="Русские субтитры")
                            SeriesName = node.InnerText;
                    }
                    if (node.GetAttributeValue("class", null) == "sub")
                    {
                        if (node == null)
                            throw new ArgumentNullException("node");
                        key = SeriesName ?? node.InnerText;
                        value = node.GetAttributeValue("data-id", null);
                        player = node.GetAttributeValue("data-p", null);
                        Anime animeItems = new Anime(key, player, value);
                        animeItemsList.Add(animeItems);
                        
                    }
                }
            }
            return animeItemsList;
        }
       /*
        public HtmlNodeCollection checkXpath(HtmlDocument htmlDocument, List<String> xpathList)
        {
            HtmlNodeCollection list = null;
            if (htmlDocument == null)
                throw new ArgumentNullException("htmlDocument");
            if (xpathList == null)
                throw new ArgumentNullException("xpathList");

            foreach (var xpath in xpathList)
            {
                list = htmlDocument.DocumentNode.SelectNodes(xpath);
                if (list != null)
                {
                    break;
                }
            }

            return list;
        }*/

        public HtmlNode checkXpath(HtmlDocument htmlDocument, List<String> xpathList)
        {
            HtmlNode list = null;
            if (htmlDocument == null)
                throw new ArgumentNullException("htmlDocument");
            if (xpathList == null)
                throw new ArgumentNullException("xpathList");

            foreach (var xpath in xpathList)
            {
                list = htmlDocument.DocumentNode.SelectSingleNode(xpath);
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
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(prepared_link_to_get_final_link_video);
                    req.Referer =  url;
                    req.UserAgent ="Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
                    HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                    path_to_source_video = resp.ResponseUri.ToString();
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
