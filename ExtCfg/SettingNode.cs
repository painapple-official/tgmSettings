using ExtCfg.Properties;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ExtCfg
{

    public partial class SettingNode : IEnumerable<SettingNode>
    {
        public Dictionary<string, SettingNode> Children = new Dictionary<string, SettingNode>();

        public SettingNode(string key, string value)
        {
            Key = key;
            Value = value;
        }
        public SettingNode() { }

        public void Clear() => Children.Clear();

        public SettingNode this[string key]
        {
            get
            {
                if (!Children.ContainsKey(key)) Children[key] = new SettingNode();
                Children[key].Key = key;
                return Children[key];
            }

            set
            {
                value.Key = key;
                Children[key] = value;
            }
        }

        public static implicit operator string(SettingNode b) => b.Value;
        public static implicit operator SettingNode(string b) => new SettingNode(null, b);

        public string Key { get; private set; }
        public string Value { get; private set; }

        public int AsInt()
        {
            Int32 o;
            if (Int32.TryParse(Value, out o))
            {
                return o;
            }
            return err<Int32>();
        }
        public short AsShort()
        {
            Int16 o;
            if (Int16.TryParse(Value, out o))
            {
                return o;
            }
            return err<Int16>();
        }
        public long AsLong()
        {
            Int64 o;
            if (Int64.TryParse(Value, out o))
            {
                return o;
            }
            return err<Int64>();
        }
        public bool AsBoolean()
        {
            Boolean o;
            if (Boolean.TryParse(Value, out o))
            {
                return o;
            }
            return err<Boolean>();
        }

        private T err<T>()
        {
            throw new SettingException(Resources.E_Cast);
        }

        public IEnumerator<SettingNode> GetEnumerator() => Children.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Children.Values.GetEnumerator();
    }
}
