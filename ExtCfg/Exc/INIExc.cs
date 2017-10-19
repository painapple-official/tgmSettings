using ExtCfg.Properties;
using System.IO;
using System.Linq;
using System.Text;

namespace ExtCfg.Exchanger
{
    class INIExc : Exc
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
                LoadINI(data, settingNode);
            }
            catch (SettingException e)
            {
                throw new SettingException(Resources.E_Load,e);
            }
        }
        private void LoadINI(string s, SettingNode settingNode)
        {
            string[] s1 = s.Trim().Split('[');
            foreach (string s1p in s1)
            {
                string[] s1p1 = s1p.Trim().Split(']');
                s1p1 = s1p1.Where(sa => !string.IsNullOrEmpty(sa)).ToArray();
                if (s1p1.Length > 1)
                {
                    string[] s1p1c = s1p1[1].Trim().Split('\n');
                    s1p1c = s1p1c.Where(sa => !string.IsNullOrEmpty(sa)).ToArray();
                    foreach (string s1p1cp in s1p1c)
                    {
                        string[] s1p1cps = s1p1cp.Trim().Split('=');
                        s1p1cps = s1p1cps.Where(sa => !string.IsNullOrEmpty(sa)).ToArray();
                        settingNode[s1p1[0]][s1p1cps[0]] = s1p1cps[1];
                    }
                }
            }
        }
        public override void Save(string path, SettingNode settingNode)
        {
            StringBuilder j = new StringBuilder();
            SaveINI(j, settingNode);
            using (StreamWriter sr = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                sr.Write(j.ToString());
            }
        }
        private void SaveINI(StringBuilder s, SettingNode settingNode)
        {
            foreach (SettingNode t in settingNode)
            {
                if (t.Children.Count != 0)
                {
                    s.AppendLine("[" + t.Key + "]");
                    foreach (SettingNode t2 in t)
                    {
                        s.AppendLine(t2.Key + "=" + t2.Value);
                    }
                }
            }
        }
    }
}
