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

        //Запускать в режиме админа
        [JsonProperty("adminMode")]
        public bool AdminMode { get; set; }

        [JsonProperty("waitEnd")]
        public bool WaitEnd { get; set; }

        public async override Task Do(bool onlineMod)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo( PTSet.PtDirectory + FilePath);
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.WorkingDirectory = PTSet.WorkingDirectory;
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            if (AdminMode)
            {
                startInfo.UseShellExecute = true;
                startInfo.Verb = "runas";
            }
            await Task.Run(() =>
            {
                try
                {
                    Process p = Process.Start(startInfo);
                    if (WaitEnd)
                        p.WaitForExit();
                }
                catch (Exception)
                {
                }
            });
        }
    }
}
