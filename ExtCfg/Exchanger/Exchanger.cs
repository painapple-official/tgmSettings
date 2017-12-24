namespace ExtCfg.Exchanger
{
    abstract class Exchanger
    {
        public abstract void Save(string path, SettingNode settingNode);
        public abstract void Load(string path, SettingNode settingNode);
    }
}
