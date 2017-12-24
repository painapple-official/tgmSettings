using ExtCfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtCfg_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                SettingStorage settingStorage = new SettingStorage("test.ini", Format.INI);
                settingStorage["n1"]["k1"] = 1;
                settingStorage.Save();
                Console.WriteLine(settingStorage["n1"]["k1"].AsInt(0));
                Console.WriteLine(settingStorage["n1"]["k2"].AsInt(0));
                Console.WriteLine(settingStorage["n1"]["k2"].AsInt());
                Console.ReadKey();
            }
        }
    }
}
