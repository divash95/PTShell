using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace TaskbookShell.Instructions
{
    public abstract class Instruction
    {
        public PTSettings PTSet{ get; set; }
        public abstract Task Do(bool onlineMod);
    }

    [JsonConverter(typeof(InstConverter))]
    public class InstList
    {
        public List<Instruction> Instructions { get; set; }

        public InstList()
        {
            Instructions = new List<Instruction>();
        }

        public void Add(Instruction i)
        {
            Instructions.Add(i);
        }

        public async void Do(bool onlineMod)
        {
            foreach (Instruction i in Instructions)
                 await i.Do(onlineMod);

        }
    }

    class InstConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(InstList));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken  jt = JToken.Load(reader);
            InstList instList = new InstList();
            PTSettings context = serializer.Context.Context as PTSettings;
            foreach (JObject obj in jt)
            {
                string operation = (string)obj["operation"];
                switch (operation)
                {
                    case "execute":
                        Execute exe = obj.ToObject<Instructions.Execute>(serializer);
                        exe.PTSet = context;
                        instList.Instructions.Add(exe);
                        break;
                    case "install":
                        Install i = obj.ToObject<Instructions.Install>(serializer);
                        i.PTSet = context;
                        instList.Instructions.Add(i);
                        break;
                    default:
                        Link l = obj.ToObject<Instructions.Link>(serializer);
                        l.PTSet = context;
                        instList.Instructions.Add(l);
                        break;
                }
            }
            return instList;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
