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
using System.Drawing;
using System.Windows.Forms;

namespace AnimeVLC
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
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
			MessageBox.Show(comboBox1.SelectedValue.ToString());
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
	}
}
