using ExtCfg.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExtCfg.Exchanger
{
    class JSONExc : Exc
    {
        public override void Load(string path, SettingNode settingNode)
        {
            try
            {
                string data;
                settingNode.Clear();
                using (StreamReader sr = new StreamReader(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read), Encoding.UTF8))
                {
                    data = sr.ReadToEnd();
                }
                JObject j = data == "" ? new JObject() : JObject.Parse(data);
                LoadJson(j, settingNode);
            }
            catch (Exception e)
            {
                throw new SettingException(Resources.E_Load, e);
            }
        }

        private void LoadJson(JObject j, SettingNode settingNode)
        {
            foreach (KeyValuePair<string, JToken> t in j)
            {
                if (t.Value.HasValues)
                {
                    LoadJson(t.Value as JObject, settingNode[t.Key]);
                }
                else
                {
                    settingNode[t.Key] = t.Value.ToString();
                }
            }
        }

        public override void Save(string path, SettingNode settingNode)
        {
            JObject j = new JObject();
            SaveJson(j, settingNode);
            using (StreamWriter sr = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                sr.Write(j.ToString());
            }
        }

        private void SaveJson(JObject j, SettingNode settingNode)
        {
            foreach (SettingNode t in settingNode)
            {
                if (t.Children.Count != 0)
                {
                    j.Add(t.Key, new JObject());
                    SaveJson(j[t.Key] as JObject, t);
                }
                else
                {
                    j.Add(t.Key, t.Value);
                }
            }
        }
    }
}
