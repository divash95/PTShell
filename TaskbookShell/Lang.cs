using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace TaskbookShell
{
    public class Lang
    {
        //Название языка
        [JsonProperty("name")]
        public string Name { get; set; }

        //Название файла справочной системы для языка
        [JsonProperty("helpNamespace")]
        public string HelpNamespace { get; set; }

        //Каталог языка(на сайте)
        [JsonProperty("siteDir")]
        public string SiteDir { get; set; }

        //Соответствие элемент формы - заголовок
        [JsonProperty("elements")]
        public List<LangElem> Elements { get; set; }

        public Lang(string name, string helpNamespace, string siteDir)
        {
            Name = name;
            HelpNamespace = helpNamespace;
            SiteDir = siteDir;
            Elements = new List<LangElem>();
        }

        public void Add(string elem, string text)
        {
            Elements.Add(new LangElem(elem, text));
        }

    }

    public class LangElem
    {
        [JsonProperty("elName")]
        public string Name { get; set; }

        [JsonProperty("elText")]
        public string Text { get; set; }

        public LangElem(string name, string text)
        {
            this.Name = name;
            this.Text = text;
        }
    }

    public class LangList
    {
        [JsonProperty("languages")]
        public List<Lang> Languages { get; set; }

        public LangList()
        {
            Languages = new List<Lang>();
        }

    }
}
