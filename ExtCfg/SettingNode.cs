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
        public static implicit operator SettingNode(int b) => new SettingNode(null, b.ToString());
        public static implicit operator SettingNode(long b) => new SettingNode(null, b.ToString());
        public static implicit operator SettingNode(short b) => new SettingNode(null, b.ToString());
        public static implicit operator SettingNode(bool b) => new SettingNode(null, b.ToString().ToLower());

        public string Key { get; private set; }
        public string Value { get; private set; }

        /// <summary>
        /// 获得该设置项表示的String类型，出现异常时将返回默认值
        /// </summary>
        /// <param name="Default">默认值</param>
        public string AsString(string Default)
        {
            try
            {
                return this;
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>
        /// 获得该设置项表示的Int类型
        /// </summary>
        /// <exception cref="SettingException">无法转换到Int类型</exception>
        public int AsInt()
        {
            Int32 o;
            if (Int32.TryParse(Value, out o))
            {
                return o;
            }
            return err<Int32>();
        }
        /// <summary>
        /// 获得该设置项表示的Int类型，出现异常时将返回默认值
        /// </summary>
        /// <param name="Default">默认值</param>
        public int AsInt(Int32 Default)
        {
            try
            {
                return AsInt();
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>
        /// 获得该设置项表示的Short类型
        /// </summary>
        /// <exception cref="SettingException">无法转换到Short类型</exception>
        public short AsShort()
        {
            Int16 o;
            if (Int16.TryParse(Value, out o))
            {
                return o;
            }
            return err<Int16>();
        }
        /// <summary>
        /// 获得该设置项表示的Short类型，出现异常时将返回默认值
        /// </summary>
        /// <param name="Default">默认值</param>
        public short AsShort(short Default)
        {
            try
            {
                return AsShort();
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>
        /// 获得该设置项表示的Long类型
        /// </summary>
        /// <exception cref="SettingException">无法转换到Long类型</exception>
        public long AsLong()
        {
            Int64 o;
            if (Int64.TryParse(Value, out o))
            {
                return o;
            }
            return err<Int64>();
        }
        /// <summary>
        /// 获得该设置项表示的Long类型，出现异常时将返回默认值
        /// </summary>
        /// <param name="Default">默认值</param>
        public long AsLong(long Default)
        {
            try
            {
                return AsLong();
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>
        /// 获得该设置项表示的Boolean类型
        /// </summary>
        /// <exception cref="SettingException">无法转换到Boolean类型</exception>
        public bool AsBoolean()
        {
            Boolean o;
            if (Boolean.TryParse(Value, out o))
            {
                return o;
            }
            return err<Boolean>();
        }
        /// <summary>
        /// 获得该设置项表示的Boolean类型，出现异常时将返回默认值
        /// </summary>
        /// <param name="Default">默认值</param>
        public bool AsBoolean(bool Default)
        {
            try
            {
                return AsBoolean();
            }
            catch
            {
                return Default;
            }
        }

        /// <summary>
        /// 获得该设置项是否存在
        /// </summary>
        public bool Exists() => Value != null;

        private T err<T>()
        {
            throw new SettingException(Resources.ERR_CAST);
        }

        public IEnumerator<SettingNode> GetEnumerator() => Children.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Children.Values.GetEnumerator();
    }
}
