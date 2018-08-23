/*
 * Created by SharpDevelop.
 * User: lagutov
 * Date: 22.08.2018
 * Time: 15:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace AnimeVLC
{
	/// <summary>
	/// Description of Anime.
	/// </summary>
	public class Anime
	{

		public String SeriesNumber {
			get;
			set;
		}

		public String Player {
			get;
			set;
		}

		public String Url {
			get;
			set;
		}
		public Anime()
		{
		}
		
		public Anime(String SeriesNumber,String Player, String Url)
		{
			this.SeriesNumber = SeriesNumber;
			this.Player = Player;
			this.Url = Url;
		}
		
		public String FullName
		{
			get {return this.SeriesNumber + " - " + this.Player;}
		}
	}
}
