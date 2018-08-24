/*
 * Created by SharpDevelop.
 * User: lagutov
 * Date: 22.08.2018
 * Time: 15:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace AnimeVLC
{
	/// <summary>
	/// Description of Sites.
	/// </summary>
	public class Sites
	{
		private Dictionary<String, String> dict = new Dictionary<string, string>();
		
		public void Set(string key, string value)
		{
			if (dict.ContainsKey(key)) {
				dict[key] = value;
			} else {
				dict.Add(key, value);
			}
		}

		public string Get(string key)
		{
			string result = null;

			if (dict.ContainsKey(key)) {
				result = dict[key];
			}

			return result;
		}
		
		public Dictionary<String,String> getDictionary(Dictionary<String,String> Dictionary){
			Dictionary = this.dict;
			return Dictionary;
		}
		
		public String Site {
			get;
			set;
		}


		public String Url_to_anime {
			get;
			set;
		}

		public Sites()
		{
		}
		
		
		public void fillDictionary()
		{
			String[] lines = System.IO.File.ReadAllLines("AnimeList.txt");
			foreach (string line in lines)
			{
				string[] splitted = line.Split(',');
				Set(splitted[0], splitted[1]);
			}
		}
	}
}
