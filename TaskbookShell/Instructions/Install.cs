using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
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

        //Ожидать завершения загрузки и установки перед запуском следующих инструкций
        [JsonProperty("waitEnd")]
        public bool WaitEnd { get; set; }

        //Установка в режиме админа
        [JsonProperty("adminMode")]
        public bool AdminMode { get; set; }

        [JsonProperty("msi")]
        //Устанавливаем файлы .msi
        public bool Msi { get; set; }

        //клиент для загрузки
        public WebClient Wc { get; set; }

        public Install(WebClient wc, string link, string fileName, bool msi = false, bool waitEnd = false, bool adminMode = false)
        {
            Url = link;
            FileName = fileName;
            WaitEnd = waitEnd;
            AdminMode = adminMode;
            Msi = msi;
            Wc = wc;
        }
        public override void Do(bool onlineMod)
        {
            //Пока загружаю в папку загрузки
            string downloadFileName = System.IO.Path.GetFileName(FileName);
            string downloadsPath = System.Environment.ExpandEnvironmentVariables("%userprofile%/downloads/");
            string outPath = downloadsPath + downloadFileName;
            Wc.DownloadFile(new Uri(Url), outPath);

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
            Process p = Process.Start(startInfo);
            if (WaitEnd)
                p.WaitForExit();
        }

    }
}
