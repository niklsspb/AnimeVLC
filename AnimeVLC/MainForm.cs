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
using System.Windows.Forms;

namespace AnimeVLC
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private ParserInterface parser = null;
        private Dictionary<string, string> myDict;
        private List<Anime> result;

        public MainForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            //
        }
        void PictureBox1Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            if (comboBox2.SelectedText.Equals(""))
            {

                if (comboBox2.Text.Contains("sibnet"))
                {
                    try
                    {
                        string url_video = parser.getVideoUrl("http://video.sibnet.ru/shell.php?videoid=" + comboBox2.SelectedValue.ToString());
                        using (Process exeProcess = Process.Start("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe", url_video))
                        {
                            //this.WindowState = FormWindowState.Minimized;
                            exeProcess.WaitForExit();
                        }
                        this.WindowState = FormWindowState.Normal;
                        MessageBox.Show("Вы просмотрели серию?");
                        //Process.Start("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe", url_video);
                        //MessageBox.Show(result);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }

                }
                else
                {
                    MessageBox.Show(comboBox2.Text);
                }

            }
            else
            {
                //MessageBox.Show(comboBox2.SelectedText);
                if (comboBox2.SelectedText.Contains("sibnet"))
                {
                    try
                    {
                        string url_video = parser.getVideoUrl("http://video.sibnet.ru/shell.php?videoid=" + comboBox2.SelectedValue.ToString());
                        using (Process exeProcess = Process.Start("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe", url_video))
                        {
                            exeProcess.WaitForExit();
                        }
                        //Process.Start("C:\\Program Files\\VideoLAN\\VLC\\vlc.exe", url_video);
                        //MessageBox.Show(result);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show(comboBox2.Text);
                }
            }

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
            SetDataSourceComboBox(myDict, comboBox1);

        }


        void Button1Click(object sender, EventArgs e)
        {
            //пробуем получить все серии
            // проверка на сайт
            if (comboBox1.SelectedValue.ToString().Contains("fan-naruto"))
            {
                parser = new FanNaruto();
                result = parser.getUrl(comboBox1.SelectedValue.ToString());
            }
            if (comboBox1.SelectedValue.ToString().Contains("animespirit"))
            {
                parser = new animespirit();
                result = parser.getUrl(comboBox1.SelectedValue.ToString());
            }
            SetDataSourceComboBox(result, comboBox2);

        }

        void SetDataSourceComboBox(Dictionary<string, string> result, ComboBox box)
        {
            box.DataSource = new BindingSource(result, null);
            box.DisplayMember = "Key";
            box.ValueMember = "Value";
        }

        void SetDataSourceComboBox(List<Anime> result, ComboBox box)
        {
            box.DataSource = new BindingSource(result, null);
            box.DisplayMember = "FullName";
            box.ValueMember = "Url";
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)

        {

            KeyEventArgs e = new KeyEventArgs(keyData);

            if (e.Control && e.KeyCode == Keys.G)

            {
                if (comboBox1.SelectedValue.ToString().Contains("fan-naruto"))
                {
                    parser = new FanNaruto();
                    result = parser.getUrl(comboBox1.SelectedValue.ToString());
                }
                if (comboBox1.SelectedValue.ToString().Contains("animespirit"))
                {
                    parser = new animespirit();
                    result = parser.getUrl(comboBox1.SelectedValue.ToString());
                }
                SetDataSourceComboBox(result, comboBox2);
                return true; // handled

            }

            return base.ProcessCmdKey(ref msg, keyData);

        }
    }
}
