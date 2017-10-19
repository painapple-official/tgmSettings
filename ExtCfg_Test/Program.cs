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
                settingStorage["n1"]["k1"] = "v1";
                settingStorage["n2"]["k2"] = "v2";
                settingStorage["n2"]["k3"] = "v3";
                settingStorage.Save();
                Console.ReadKey();
            }
            {
                SettingStorage settingStorage = new SettingStorage("test.ini", Format.INI);
                Console.WriteLine(settingStorage["n1"]["k1"]);
                Console.WriteLine(settingStorage["n2"]["k2"]);
                Console.WriteLine(settingStorage["n2"]["k3"]);
                Console.ReadKey();
            }
            {
                SettingStorage settingStorage = new SettingStorage("test.json");
                settingStorage["n1"]["k1"] = "v1";
                settingStorage["n2"]["k2"] = "v2";
                settingStorage["n2"]["n3"]["k3"] = "v3";
                settingStorage.Save();
                Console.ReadKey();
            }
            {
                SettingStorage settingStorage = new SettingStorage("test.json");
                Console.WriteLine(settingStorage["n1"]["k1"]);
                Console.WriteLine(settingStorage["n2"]["k2"]);
                Console.WriteLine(settingStorage["n2"]["n3"]["k3"]);
                Console.ReadKey();
            }
        }
    }
}
