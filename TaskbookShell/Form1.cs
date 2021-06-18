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
        //Сайт задачника
        private string host = "ptaskbook.com";

        //Меню разделов со всеми инструкциями
        private PTMenuString ptMenuString;

        //Соответствие название элемента формы - меню разделов
        private Dictionary<string, PTMenuItem> menuItems = new Dictionary<string, PTMenuItem>();

        //Соотвтетсвие индекс языка(по порядку) и настроек языка
        private Dictionary<int, Lang> languages = new Dictionary<int, Lang>();

        //Список доступных языков
        private LangList langs;

        //Выбранный язык
        private Lang selectedLang;

        //Рабочий каталог
        private string workingDirectory;

        //Системная папка задачника
        private string ptDirectory = @"C:\Program Files (x86)\PT4\";

        //путь для загрузки настроек меню(по-умолчанию)
        private string menuSettingsUrl = "http://ptaskbook.com/download/menuSettings.json";

        //путь для загрузки настроек языков(по-умолчанию)
        private string locSettingsUrl = "http://ptaskbook.com/download/localization.json";

        //путь для загрузки настроек языков(по-умолчанию)
        private string settingsPath;

        //файл настроек меню
        private string menuSettingsPath;

        //файл настроек языка
        private string localizationPath;

        //Контекст задачника, который передается в инструкции
        PTSettings ptSettings;

        //Есть подключение к сайту(можно переключить в меню)     
        private bool onlineMod = true;

        //язык при запуске(получаем из файла настроек)
        private string startLang = "ru";
        public Form1()
        {
            InitializeComponent();

            string projectPath = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\"));
           
            //Параметры отображения( поверх всех, почти правый угол, сворачивается в трей)
            this.TopMost = true;
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width, 0);
           
            Resize += new System.EventHandler(this.Form1_Resize);

            //Проверяем доступ к сайту задачника
            onlineMod = CheckConnection();

            //Обновлям файлы настроек
            UpdateSettingFiles(projectPath);

            //Проверка, задачник должен быть установлен и найден
            if (!CheckDirrectories())
                MessageBox.Show("Не найден установленный задачник PT4");

            //Загрузить языковые настройки и выбрать язык, указанный в настройках
            LoadLanguages(localizationPath);

            //selectedLang = languages[0];

            //Сохраняем контекст для сериализации файла настроек   
            ptSettings = new PTSettings(host, ptDirectory + selectedLang.HelpNamespace, selectedLang.SiteDir, ptDirectory, workingDirectory, projectPath, downloadProgressBar);
            JsonSerializerSettings settings = new JsonSerializerSettings { Context = new StreamingContext(StreamingContextStates.Other, ptSettings) };

            //Преобразуем json в класс PTMenuString(настройки меню)
            string json = File.ReadAllText(menuSettingsPath);
            ptMenuString = JsonConvert.DeserializeObject<PTMenuString>(json, settings);

            //Создаем элементы формы на основе настроек
            LoadMenuString();
            //Меняем текст и видимость элементов в зависимости от языка и режима
            ModLangChange();

        }

        private void UpdateSettingFiles(string projectPath)
        {
            //downloadSettings.txt содержит дату последнего обновления настроек и url для загрузки этих настроек
            settingsPath = string.Format("{0}startSettings.txt", projectPath);
            DateTime lastDownload = new DateTime(2000, 1, 1);
            try
            {
                string[] lines = File.ReadAllLines(settingsPath);
                int posX = -1;
                int posY = -1;
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fileAttrs = lines[i].Split('=');
                    string parKey = fileAttrs[0];
                    string parValue = fileAttrs[1];
                    switch (parKey)
                    {
                        case "lastDownload":
                            DateTime.TryParse(parValue, out lastDownload);
                            break;
                        case "menuSettingsUrl":
                            menuSettingsUrl = parValue;
                            break;
                        case "locSettingsUrl":
                            locSettingsUrl = parValue;
                            break;
                        case "lang":
                            startLang = parValue;
                            break;
                        case "startPositionX":
                            if (parValue != "")
                                Int32.TryParse(parValue, out posX);
                            break;
                        case "startPositionY":
                            if (parValue != "")
                                Int32.TryParse(parValue, out posY);
                            break;
                        case "onlineMod":
                            onlineMod = (parValue == "1" && onlineMod);
                            break;
                    }
                }
                if (posX > 0 && posY > 0)
                    this.Location = new Point(posX, posY);
            }
            catch (Exception exept)
            {
                MessageBox.Show(exept.Message);
            }

            //Настройки обновляются раз в день и только в онлайн режиме
            bool needUpdate = true;
            if (DateTime.Now.Subtract(lastDownload).TotalDays < 1)
                needUpdate = false;
            bool settingsUpdated = false;
            if (onlineMod && needUpdate)
            {
                menuSettingsPath = string.Format("{0}/downloads/menuSettings.json", projectPath);
                localizationPath = string.Format("{0}/downloads/localization.json", projectPath);
                settingsUpdated = DownloadSettings();
                //Меняем дату последнего обновления на текущую
                ChangeUpdateDate(DateTime.Now.Date);
                //settingsUpdated = false;
            }
            if (!onlineMod || !settingsUpdated)
            {
                menuSettingsPath = string.Format("{0}menuSettings.json", projectPath);
                localizationPath = string.Format("{0}localization.json", projectPath);
            }
        }

        //Обновляет файл настроек загрузки(меняет дату обновления на текущую)
        private void ChangeUpdateDate(DateTime date)
        {
            string[] lines = File.ReadAllLines(settingsPath);
            string newText = "";
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fileAttrs = lines[i].Split('=');
                string parKey = fileAttrs[0];
                string parValue = fileAttrs[1];
                if (parKey == "lastDownload")
                    newText += "lastDownload=" + date.ToShortDateString() + Environment.NewLine;
                else
                    newText += parKey + "=" + parValue + Environment.NewLine;
            }
            File.WriteAllText(settingsPath, newText);
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

        //Изменение текста(видимости) элементов для выбранного языка и режима
        public void ChangeVisibilityTextByLangMod()
        {
            ptSettings.Lang = selectedLang.SiteDir;
            ptSettings.HelpNamespace = selectedLang.HelpNamespace;
            foreach (LangElem el in selectedLang.Elements)
            {
                bool visibility = (el.Text != "");
                bool visibleByMod = true;
                if (menuItems.ContainsKey(el.Name))
                    visibleByMod = onlineMod || !menuItems[el.Name].OnlineOnly;
                switch (el.Name)
                {
                    case "radioButtonOnline" :
                        radioButtonOnline.Text = el.Text;
                        break;
                    case "radioButtonLocal":
                        radioButtonLocal.Text = el.Text;
                        break;
                    default:
                        ToolStripItem[] foundMenuItems = this.MainMenuStrip.Items.Find(el.Name, true);
                        if (foundMenuItems.Count() != 0)
                        {
                            foundMenuItems[0].Visible = visibility && visibleByMod;
                            foundMenuItems[0].Text = el.Text;
                        }
                        break;
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

            langs = JsonConvert.DeserializeObject<LangList>(json);

            int i = 0;
            foreach (Lang l in langs.Languages)
            {
                ToolStripMenuItem langItem = new ToolStripMenuItem();
                langItem.Name = "langMenu_" + i.ToString();
                langItem.Click += new EventHandler(PTForm_LangCheck);
                langItem.Text = l.Name;
                if (l.SiteDir == startLang)
                {
                    selectedLang = l;
                    langItem.Checked = true;
                }
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

        //При нажатии на подменю выбора языков меняем язык и видимость элементов формы
        private void PTForm_LangCheck(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string[] temp = item.Name.Split('_');
            string langIndex = temp[1];
            int i;
            int.TryParse(langIndex,out i);
            selectedLang = languages[i];
            ChangeVisibilityTextByLangMod();
            foreach (ToolStripMenuItem subMenuItem in langMenu.DropDownItems)
            {
                subMenuItem.Checked = (subMenuItem == item);
            }
        }

        //Изменение режима работы(онлайн или локальный)
        private void ModMenuClick(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item.Name == "internetModMenu")
                onlineMod = true;
            else
                onlineMod = false;
            ModLangChange();
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
            using (WebClient wc = new WebClient())
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
            }
            return true;
        }

        //Проверка соединения с сайтом задачника
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
            if (e.Modifiers == Keys.Control)
            {
                if(e.KeyCode == Keys.S)
                    PTStartProcess(string.Format("{0}PT4Setup.exe", ptDirectory));
                if (e.KeyCode == Keys.L)
                    PTStartProcess(string.Format("{0}PT4Load.exe", ptDirectory), false);
                if (e.KeyCode == Keys.D)
                    PTStartProcess(string.Format("{0}PT4Demo.exe", ptDirectory), false);
                if (e.KeyCode == Keys.R)
                    PTStartProcess(string.Format("{0}PT4Res.exe", ptDirectory));
            }
        }

        private void ModLangChange()
        {
            radioButtonOnline.Checked = onlineMod;
            radioButtonLocal.Checked = !onlineMod;
            localModMenu.Checked = !onlineMod;
            internetModMenu.Checked = onlineMod;
            if (onlineMod)
            {
                radioButtonOnline.BackColor = SystemColors.ActiveCaption;
                radioButtonLocal.BackColor = SystemColors.Control;
            }
            else
            {
                radioButtonLocal.BackColor = SystemColors.ActiveCaption;
                radioButtonOnline.BackColor = SystemColors.Control;
            }
            if (selectedLang != null)
                ChangeVisibilityTextByLangMod();
        }

        private void RadioButtonOnline_Click(object sender, EventArgs e)
        {
            onlineMod = true;
            ModLangChange();
        }

        private void RadioButtonLocal_Click(object sender, EventArgs e)
        {
            onlineMod = false;
            ModLangChange();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            // проверяем наше окно, и если оно было свернуто, делаем событие        
            if (WindowState == FormWindowState.Minimized)
            {
                // прячем наше окно из панели
                this.ShowInTaskbar = false;
                // делаем нашу иконку в трее активной
                notifyIcon1.Visible = true;
            }
        }

        private void PictureBox3_MouseHover(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = SystemColors.ActiveCaption;
        }

        private void PictureBox3_MouseLeave(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = SystemColors.Control;
        }

        private void NotifyIcon1_Click(object sender, EventArgs e)
        {
            // делаем нашу иконку скрытой
            notifyIcon1.Visible = false;
            // возвращаем отображение окна в панели
            this.ShowInTaskbar = true;
            //разворачиваем окно
            WindowState = FormWindowState.Normal;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
                string[] lines = File.ReadAllLines(settingsPath);
                string newText = "";
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fileAttrs = lines[i].Split('=');
                    string parKey = fileAttrs[0];
                    string parValue = fileAttrs[1];
                switch (parKey)
                {
                    case "lang":
                        newText += "lang=" + selectedLang.SiteDir + Environment.NewLine;
                        break;
                    case "startPositionX":
                        newText += "startPositionX=" + this.Location.X + Environment.NewLine;
                        break;
                    case "startPositionY":
                        newText += "startPositionY=" + this.Location.Y + Environment.NewLine;
                        break;
                    case "onlineMod":
                        if (onlineMod)
                            newText += "onlineMod=1" + Environment.NewLine;
                        else
                            newText += "onlineMod=0" + Environment.NewLine;
                        break;
                    default:
                        newText += parKey + "=" + parValue + Environment.NewLine;
                        break;

                }
                }
                File.WriteAllText(settingsPath, newText);
        }
    }

 
}



