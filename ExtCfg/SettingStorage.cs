using ExtCfg.Exchanger;
using System;

namespace ExtCfg
{
    public class SettingStorage
    {
        private string FileName;
        private SettingNode Root = new SettingNode();

        public SettingStorage(string FileName, Format format = Format.JSON)
        {
            Format = format;
            this.FileName = FileName;
            Load();
        }

        private void Load()
        {
            switch (Format)
            {
                case Format.JSON:
                    {
                        new JSONExchanger().Load(FileName, Root);
                        break;
                    }
                case Format.INI:
                    {
                        new INIExchanger().Load(FileName, Root);
                        break;
                    }
            }
        }
        public void Save()
        {
            switch (Format)
            {
                case Format.JSON:
                    {
                        new JSONExchanger().Save(FileName, Root);
                        break;
                    }
                case Format.INI:
                    {
                        new INIExchanger().Save(FileName, Root);
                        break;
                    }
            }
        }

        public Format Format
        {
            get; private set;
        }

        public SettingNode this[string key]
        {
            get
            {
                return Root[key];
            }
            set
            {
                Root[key] = value;
            }
        }
    }
}

public enum Format
{
    JSON, INI
}

[Serializable]
public class SettingException : Exception
{
    public SettingException(string message, Exception inner = null) : base(message, inner) { }
}