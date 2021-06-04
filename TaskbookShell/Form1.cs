using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace TaskbookShell
{
    public partial class Form1 : Form
    {
        //Папка текущего проекта
        private string projectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));

        private string host = "ptaskbook.com";
        
        //Меню разделов со всеми инструкциями
        private PTMenuString ptMenuString;

        //Соответствие название элемента формы - меню разделов
        private Dictionary<string, PTMenuItem> menuItems = new Dictionary<string, PTMenuItem>();

        //Соотвтетсвие индекс языка(по порядку) и настроек языка
        private Dictionary<int, Lang> languages = new Dictionary<int, Lang>();

        //Выбранный язык
        private Lang selectedLang;

        //Рабочий каталог
        private string workingDirectory;

        //Системная папка задачника
        private string ptDirectory = @"C:\Program Files (x86)\PT4\";

        private string menuSettingsUrl = "http://ptaskbook.com/download/menuSettings.json";

        private string locSettingsUrl = "http://ptaskbook.com/download/localization.json";

        private string menuSettingsPath;

        private string localizationPath;

        //Веб-клиент для загрузки файлов
        private WebClient wc = new WebClient();

        //Есть подключение к сайту(можно переключить в меню)     
        private bool onlineMod = true;
  
        public Form1()
        {
            InitializeComponent();


            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(0, 0);

            onlineMod = CheckConnection();

            internetModMenu.Checked = onlineMod;
            localModMenu.Checked = !onlineMod;

            bool settingsUpdated = false;

            if (onlineMod)
            {
                menuSettingsPath = string.Format("{0}/downloads/menuSettings.json", projectPath);
                localizationPath = string.Format("{0}/downloads/localization.json", projectPath);
                settingsUpdated = DownloadSettings();
                settingsUpdated = false;
            }
            if(!onlineMod || !settingsUpdated)
            {
                menuSettingsPath = string.Format("{0}menuSettings.json", projectPath);
                localizationPath = string.Format("{0}localization.json", projectPath);
            }

            if (!CheckDirrectories())
                MessageBox.Show("Не найден установленный задачник PT4");

            //Загрузить языковые настройки и выбрать первый язык из списка
            LoadLanguages(localizationPath);
       
            selectedLang = languages[0];

            //Заполнение меню из файла настроек
            //Сохраняем контекст для сериализации
            PTSettings ptSettings = new PTSettings(wc, host, ptDirectory + selectedLang.HelpNamespace, selectedLang.SiteDir, ptDirectory, workingDirectory);
            JsonSerializerSettings settings = new JsonSerializerSettings{Context = new StreamingContext(StreamingContextStates.Other, ptSettings)};
            
            //Преобразуем json в класс PTMenuString
            string json = File.ReadAllText(menuSettingsPath);
            ptMenuString = JsonConvert.DeserializeObject<PTMenuString>(json, settings);

            LoadMenuString();

            //Изменение языка для элементов формы
            ChangeLang(selectedLang);

            //Еще не доделал, это для вывода процесса загрузки файлов(чтобы не казалось, что приложение зависло, пока идет загрузка)
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
        }

        //Проверка корректности системной папки и получение из конфига рабочего каталога
        public bool CheckDirrectories()
        {
            if (!Directory.Exists(ptDirectory))
            {
                using (var dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Не найден задачник Programming Taskbook, укажите папку с задачником";
                    if (dialog.ShowDialog() == DialogResult.OK)
                        ptDirectory = dialog.SelectedPath + "/";
                    else
                        return false;
                }
            }
            string[] filestxt = System.IO.File.ReadAllLines(ptDirectory + @"\PT.ini");
            workingDirectory = "";
            for (int i = 0; i < filestxt.Length; i++)
            {
                if (filestxt[i].Contains("Path"))
                {
                    string[] attrs = filestxt[i].Split('=');
                    if (attrs.Count() > 1)
                    {
                        workingDirectory = attrs[1];
                        break;
                    }
                    
                }
            }
            return true;
        }

        //Изменение текста элементов для выбранного языка
        public void ChangeLang(Lang l)
        {
            foreach(LangElem el in l.Elements)
            {
                bool visibility = (el.Text != "");
                bool visibleByMod = true;
                if (menuItems.ContainsKey(el.Name))
                    visibleByMod = onlineMod || !menuItems[el.Name].OnlineOnly;
                ToolStripItem[] foundMenuItems = this.MainMenuStrip.Items.Find(el.Name, true);
                if (foundMenuItems.Count() != 0)
                {
                    foundMenuItems[0].Visible = visibility && visibleByMod;
                    foundMenuItems[0].Text = el.Text;
                }

                ToolStripItem[] foundpanelItems = panelStrip.Items.Find(el.Name, true);
                if (foundpanelItems.Count() != 0)
                {
                    foundpanelItems[0].Visible = visibility && visibleByMod;
                    foundpanelItems[0].Text = el.Text;
                }
            }
        }

        //Загрузка элементов меню(формы) из настроек меню, предварительно загруженных из файла
        public void LoadMenuString()
        {
            //Загрузка меню из файла
            foreach (PTMenuItem ptMenuiItem in ptMenuString.MenuItems)
            {
                ToolStripMenuItem menuItem = new ToolStripMenuItem(ptMenuiItem.Name);
                menuItem.Click += new EventHandler(PTForm_MenuClick);
                menuItem.Text = ptMenuiItem.Text;
                menuItem.Name = ptMenuiItem.Name;
                MainMenuStrip.Items.Insert(MainMenuStrip.Items.Count - 1, menuItem);
                menuItems.Add(menuItem.Name, ptMenuiItem);
                foreach (PTMenuItem ptSubMenuItem in ptMenuiItem.SubMenu)
                {
                    ToolStripMenuItem subMenuItem = new ToolStripMenuItem(ptSubMenuItem.Name);
                    subMenuItem.Text = ptSubMenuItem.Text;
                    subMenuItem.Name = ptSubMenuItem.Name;
                    subMenuItem.Click += new EventHandler(PTForm_MenuClick);
                    //subMenuItem.BackColor = backColor;
                    menuItem.DropDownItems.Add(subMenuItem);
                    menuItems.Add(subMenuItem.Name, ptSubMenuItem);
                    foreach (PTMenuItem ptSubMenuItem2 in ptSubMenuItem.SubMenu)
                    {
                            ToolStripMenuItem subMenuItem2 = new ToolStripMenuItem(ptSubMenuItem.Name);
                            subMenuItem2.Text = ptSubMenuItem2.Text;
                            subMenuItem2.Name = ptSubMenuItem2.Name;
                            subMenuItem2.Click += new EventHandler(PTForm_MenuClick);
                            //subMenuItem2.BackColor = backColor;
                            subMenuItem.DropDownItems.Add(subMenuItem2);
                            menuItems.Add(subMenuItem2.Name, ptSubMenuItem2);
                    }
                }
            }

        }



        //Загрузка языков из файла
        public void LoadLanguages(string fileName)
        {
            string json = File.ReadAllText(fileName, System.Text.Encoding.Default);

            LangList langs = JsonConvert.DeserializeObject<LangList>(json);

            int i = 0;
            bool first = true;
            foreach (Lang l in langs.Languages)
            {
                ToolStripMenuItem langItem = new ToolStripMenuItem();
                langItem.Name = "langMenu_" + i.ToString();
                langItem.Click += new EventHandler(PTForm_LangCheck);
                langItem.Text = l.Name;
                langItem.Checked = first;
                first = false;
                langMenu.DropDownItems.Add(langItem);
                languages.Add(i, l);
                i++;
            }
        }

        private void SetupBtn_Click(object sender, EventArgs e)
        {
            PTStartProcess(string.Format("{0}PT4Setup.exe", ptDirectory));
        }
        private void DemoBtn_Click(object sender, EventArgs e)
        {
            PTStartProcess(string.Format("{0}PT4Demo.exe", ptDirectory), false);
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            PTStartProcess(string.Format("{0}PT4Load.exe", ptDirectory),false);
        }

        private void ResultBtn_Click(object sender, EventArgs e)
        {
            PTStartProcess(string.Format("{0}PT4Res.exe", ptDirectory));
        }


        //При нажатии на раздел вызывать соответствующие инструкции
        private void PTForm_MenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            try
            {
                menuItems[item.Name].DoInstructions(onlineMod);
            }
            catch (Exception exept)
            {
                MessageBox.Show(exept.Message);
            }
        }

        private void PTForm_LangCheck(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string[] temp = item.Name.Split('_');
            string langIndex = temp[1];
            int i;
            int.TryParse(langIndex,out i);
            ChangeLang(languages[i]);
            foreach (ToolStripMenuItem subMenuItem in langMenu.DropDownItems)
            {
                subMenuItem.Checked = (subMenuItem == item);
            }
        }

        private void ModMenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Name == "internetModMenu" || item.Name == "internetModPanel")
            {
                onlineMod = true;
                internetModMenu.Checked = true;
                localModMenu.Checked = false;
            }
            else
            {
                localModMenu.Checked = true;
                internetModMenu.Checked = false;
                onlineMod = false;
            }
            ChangeLang(selectedLang);
        }
 
        //Запуск процесса
        private void PTStartProcess(string path, bool UseShellExecute = true)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.UseShellExecute = UseShellExecute;
            startInfo.WorkingDirectory = workingDirectory;
            try
            {
                Process.Start(startInfo);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        //ЗагрузкаНастроек
        private bool DownloadSettings()
        {
            try
            {
                wc.DownloadFile(new Uri(menuSettingsUrl), menuSettingsPath);
                wc.DownloadFile(new Uri(locSettingsUrl), localizationPath);
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }
        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Visible = true;
            //downloadLabel.Visible = true;
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            //downloadLabel.Text = "Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive;
            downloadProgressBar.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            downloadProgressBar.Visible = false;
            //downloadLabel.Visible = false;
        }

        private bool CheckConnection()
        {
            try
            {
                Ping myPing = new Ping();
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);

            }
            catch (Exception)
            {
                return false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }

    //Контекст для сериализации Json
    public class PTSettings
    {
        public WebClient Wc { get; set; }

        public string Host { get; set; }

        public string HelpNamespace { get; set; }

        public string Lang { get; set; }

        public string PtDirectory { get; set; }

        public string WorkingDirectory { get; set; }

        public PTSettings(WebClient wc, string host, string helpNamespace, string lang, string ptDirectory, string workingDirectory)
        {
            Wc = wc;
            Host = host;
            HelpNamespace = helpNamespace;
            Lang = lang;
            PtDirectory = ptDirectory;
            WorkingDirectory = workingDirectory;
        }
    }
}



