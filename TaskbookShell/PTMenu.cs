using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using TaskbookShell.Instructions;
using System.Runtime.Serialization;

namespace TaskbookShell
{
    //Список с разделами меню
    public class PTMenuString
    {
        [JsonProperty("menuStrip")]
        public List<PTMenuItem> MenuItems { get; set; }

        public PTMenuString()
        {
            MenuItems = new List<PTMenuItem>();
        }

    }

    //Раздел меню
    public class PTMenuItem
    {
        //Подменю
        [JsonProperty("subMenu")]
        public List<PTMenuItem> SubMenu { get; set; }

        //Наименование элемента формы
        [JsonProperty("name")]
        public string Name { get; set; }

        //Заголовок
        [JsonProperty("text")]
        public string Text { get; set; }

        //Выводится только в онлайн режиме
        [JsonProperty("onlineOnly")]
        public bool OnlineOnly { get; set; }

        [JsonProperty("instructions")]
        //Список инструкций, которые будут вызываться при нажатии на раздел
        public InstList InstList { get; set; }

        public PTMenuItem(string name, string text, bool onlineOnly = false)
        {
            Name = name;
            Text = text;
            SubMenu = new List<PTMenuItem>();
            InstList = new InstList();
            OnlineOnly = onlineOnly;
        }

        public void DoInstructions(bool onlineMod)
        {
           InstList.Do(onlineMod);
        }

    }


}
