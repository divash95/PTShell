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

        //языковой каталог на сайте (ru или eng)
        public string Lang { get; set; }

        //путь к файлу справки chm
        public string HelpNamespace { get; set; }

        //Сайт
        public string Host { get; set; }



        public Link(string onlinePath, string offlineTopic, string host, string helpNamespace, string lang = "ru")
        {
            OnlinePath = onlinePath;
            OfflineTopic = offlineTopic;
            Host = host;
            HelpNamespace = helpNamespace;
            Lang = lang;
        }

        //Если есть соединение, то вызываем раздел сайта. Иначе раздел справочной системы
        public override void Do(bool onlineMod)
        {
            if (onlineMod && OnlinePath !="" )
            {
                string path = "http://" + Host + "/" + Lang + OnlinePath;
                Process.Start(path);
            }
            else if(OfflineTopic != "")
            {
                Help.ShowHelp(null, HelpNamespace, HelpNavigator.Topic, OfflineTopic);
            }
        }

        
    }
}
