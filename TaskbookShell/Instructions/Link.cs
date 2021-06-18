using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace TaskbookShell.Instructions
{
    public class Link : Instruction
    {
        //адрес раздела сайта(без хоста и языкового каталога)
        [JsonProperty("onlinePath")]
        public string OnlinePath { get; set; }

        //Название раздела в файле chm
        [JsonProperty("offlineTopic")]
        public string OfflineTopic { get; set; }

        //Если есть соединение, то вызываем раздел сайта. Иначе раздел справочной системы
        public async override Task Do(bool onlineMod)
        {
            await Task.Run(() =>
            {
                if (onlineMod && OnlinePath !="" )
                {
                    string path = "http://" + PTSet.Host + "/" + PTSet.Lang + OnlinePath;
                    Process.Start(path);
                }
                else if(OfflineTopic != "")
                {
                    Help.ShowHelp(null, PTSet.PtDirectory + PTSet.HelpNamespace, HelpNavigator.Topic, OfflineTopic);
                }
            });
        }

        
    }
}
