/*
 * Created by SharpDevelop.
 * User: lagutov
 * Date: 22.08.2018
 * Time: 15:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using System.Collections.Generic;
using HtmlAgilityPack;

namespace AnimeVLC
{
	/// <summary>
	/// Description of Parser.
	/// </summary>
	public class Parser
	{

		public String url {
			get;
			set;
		}

		public Parser()
		{
		}
		
		public List<Anime> getUrl(String url)
		{
			var html = url;
			HtmlWeb web = new HtmlWeb();
			var htmlDoc = web.Load(html);
			var node = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"MT_overroll\"]/div[2]");
			var n = node.SelectNodes("i");
			//String [] s = n.Split(Environment.NewLine.ToCharArray());
			String key="";
			String value="";
			String player="";
			List <Anime> animeArrayList = new List<Anime>();
			foreach (var nodes in n){
				//Nodes.Add(nodes.InnerText);
				key = nodes.InnerText;
				value = nodes.GetAttributeValue("data-id",null);//.Attributes.AttributesWithName("data-id").ToString();
				player = nodes.GetAttributeValue("data-p", null);
				//nodes.Attributes.AttributesWithName("data-p").ToString();
				Anime array = new Anime(key,player,value);
				animeArrayList.Add(array);
			}
			
			return animeArrayList;
		}
		
		public String getVideoUrl(String url)
		{
			if (url.Contains("sibnet")){
			var html = url;
			HtmlWeb web = new HtmlWeb();
			web.UserAgent="Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
			var htmlDoc = web.Load(html);
			string result = htmlDoc.Text;
			string prop="";
			if (result.IndexOf("{src:")>0)
			{
				int start_index = result.IndexOf("{src:")+7;
				int end_index = result.IndexOf(", type:")-1;
				prop = result.Substring(start_index,end_index-start_index);
				//string prop1 = result.Substring(result.IndexOf(", type:")-1);
				var ht = "http://video.sibnet.ru"+prop;
				HtmlWeb htweb = new HtmlWeb();
				htweb.UserAgent="Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/62.0.3202.94 Mobile Safari/537.36";
				var htDoc = htweb.Load(ht);
				prop = htweb.ResponseUri.ToString();
			}
			
			return prop;
		}
			else {
				return null;
			}
			}

	}
}
