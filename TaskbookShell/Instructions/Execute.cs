using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace TaskbookShell.Instructions
{
    //Выполнение файла
    public class Execute:Instruction
    {
        //путь к файлу
        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        //рабочий каталог задачника
        public string WorkingDirectory { get; set; }

        //Запускать в режиме админа
        [JsonProperty("adminMode")]
        public bool AdminMode { get; set; }

        //Ожидать завершение, после чего запускать следующие инструкции
        [JsonProperty("waitEnd")]
        public bool WaitEnd { get; set; }

        public Execute(string filePath, string workingDirectory, bool waitEnd = false, bool adminMode = false)
        {
            FilePath = filePath;
            WorkingDirectory = workingDirectory;
            AdminMode = adminMode;
            WaitEnd = waitEnd;
        }
        public override void Do(bool onlineMod)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(FilePath);
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.WorkingDirectory = WorkingDirectory;
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            if (AdminMode)
            {
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
            }
            var p = Process.Start(startInfo);
            if (WaitEnd)
                p.WaitForExit();
        }
    }
}
