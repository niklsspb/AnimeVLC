/*
 * Created by SharpDevelop.
 * User: lagutov
 * Date: 22.08.2018
 * Time: 15:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using HtmlAgilityPack;

namespace AnimeVLC
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private Parser parser=null;
		private Dictionary<string,string> myDict;
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		void PictureBox1Click(object sender, EventArgs e)
		{
			if (comboBox2.SelectedText.Equals("")){
				
				if (comboBox2.Text.Contains("sibnet"))
				    {
				    	string result = parser.getVideoUrl("http://video.sibnet.ru/video"+comboBox2.SelectedValue.ToString());
				    	Process.Start("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe",result);
				    	//MessageBox.Show(result);
				    }
				else
				{
					MessageBox.Show(comboBox2.Text);
				}
				
				//MessageBox.Show(comboBox2.Text);
			    }
			else
			{
				//MessageBox.Show(comboBox2.SelectedText);
				if (comboBox2.SelectedText.Contains("sibnet"))
				    {
				    	string result = parser.getVideoUrl("http://video.sibnet.ru/video"+comboBox2.SelectedValue.ToString());
				    	Process.Start("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe",result);
				    	//MessageBox.Show(result);
				    }
				else
				{
					MessageBox.Show(comboBox2.Text);
				}
			}
			
			
			//MessageBox.Show("http://video.sibnet.ru/video"+comboBox2.SelectedValue.ToString());
			
		}
		void MainFormLoad(object sender, EventArgs e)
		{
			//Создаем экземпляр класса с сайтами и названиями аниме
			Sites sites = new Sites();
			//Заполняем словарь который определен в классе Sites из текстового файла
			sites.fillDictionary();
			//Устанавливаем локально определенному словарю данные из словаря определенного в классе Sites.
			//А нужно ли переопределять значения??? Скорее всего да, так как словарь private в классе Sites
			myDict = sites.getDictionary(myDict);
			//Заполняем значениями из словаря
			comboBox1.DataSource = new BindingSource(myDict,null);
			comboBox1.DisplayMember = "Key";
			comboBox1.ValueMember = "Value";
			
			
			
		}
		void Button1Click(object sender, EventArgs e)
		{
			//пробуем получить все серии
			parser = new Parser();
			//parser.getUrl(comboBox1.SelectedValue.ToString());
			List <Anime> result = parser.getUrl(comboBox1.SelectedValue.ToString());
			BindingSource bsource = new BindingSource();
			bsource.DataSource = result;
			comboBox2.DataSource = bsource.DataSource;
			comboBox2.DisplayMember = "FullName";
			comboBox2.ValueMember = "Url";
			
		}
	}
}
