using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace TaskbookShell.Instructions
{
    //Инструкция для установки файлов
    public class Install: Instruction
    {
        //url файла для установки, который нужно скачать
        [JsonProperty("link")]
        public string Url { get; set; }

        //Название файла, который скачаем и после этого запустим(из url)
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        //Установка в режиме админа
        [JsonProperty("adminMode")]
        public bool AdminMode { get; set; }

        [JsonProperty("msi")]
        //Устанавливаем файлы .msi
        public bool Msi { get; set; }

        [JsonProperty("waitEnd")]
        public bool WaitEnd { get; set; }

        public async override Task Do(bool onlineMod)
        {

            string downloadFileName = System.IO.Path.GetFileName(FileName);
            string outPath = PTSet.ProjectPath + "/downloads/" + downloadFileName;
            //загружаем файл и подключаем обработчики событий, чтобы отображать процесс загрузки
            using (WebClient webClient = new WebClient())
            {
                WebClient wc = new WebClient();
                wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);
                wc.DownloadFileCompleted += new AsyncCompletedEventHandler(DownloadFileCompleted);
                await wc.DownloadFileTaskAsync(new Uri(Url), outPath);
            }
            //После загрузки запускаем загруженный файл
            ProcessStartInfo startInfo = new ProcessStartInfo(outPath);

            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            if (Msi)
            {
                startInfo.FileName = "msiexec";
                startInfo.Arguments = "/i " + outPath;
            }
            if (AdminMode)
            {
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
            }

            try
            {
                Process p = Process.Start(startInfo);
                if (WaitEnd)
                    p.WaitForExit();
            }
            catch (Exception)
            {
            }

        }

        void DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            PTSet.DownloadProgress.Visible = true;
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            PTSet.DownloadProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
        }

        void DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            PTSet.DownloadProgress.Visible = false;
        }

    }

}
