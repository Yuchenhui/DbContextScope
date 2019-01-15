using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Mehdime.Entity
{

    public static class SiteSettingsExtension
    {
        private static SiteSettingsCollection _sites;

        public static SiteSettingsCollection Instance()
        {
            if (_sites== null || _sites.Count == 0)
            {
                _sites = new SiteSettingsCollection();
                var coll = (SiteSettingsSection)ConfigurationManager.GetSection("siteSettings");
                _sites = coll.KeyValues;
            }
            return _sites;
        }


    }

    public class SiteSettingsSection : ConfigurationSection  
    {
        private static readonly ConfigurationProperty s_property
            = new ConfigurationProperty(string.Empty, typeof(SiteSettingsCollection), null,
                                            ConfigurationPropertyOptions.IsDefaultCollection);

        [ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
        public SiteSettingsCollection KeyValues
        {
            get
            {
                return (SiteSettingsCollection)base[s_property];
            }
        }
    }


    [ConfigurationCollection(typeof(SiteSettings))]
    public class SiteSettingsCollection : ConfigurationElementCollection
    {
        private static Dictionary<string, string> _connDic;
        private static string _currentConn;
        public SiteSettingsCollection() : base(StringComparer.OrdinalIgnoreCase) 
        {
        }
        public new SiteSettings this[string name]
        {
            get
            {
                return (SiteSettings)base.BaseGet(name);
            }
        }

        public string GetCurrentConnect()
        {
            if (string.IsNullOrEmpty(_currentConn))
            {
                foreach (SiteSettings o in this)
                {
                    if (o.IsCurrent)
                    {
                        _currentConn = o.Connection;
                        break;
                    }
                }
            }

            return _currentConn;
        }

        public string GetDbConnect(string key)
        {
            string conn;
            if (_connDic == null)
            {
                _connDic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            if (_connDic.ContainsKey(key))
            {
                conn = _connDic[key];
            }
            else
            {
                if (this.HasSiteSettings(key))
                {
                    conn = this[key].Connection;
                    _connDic.Add(key, conn);
                }
                else
                {
                    throw new Exception($"Site:{key} does not exists");
                }
            }
            return conn;
        }

        public bool HasSiteSettings(string key)
        {
            var keys = base.BaseGetAllKeys();
            return keys.Contains(key);
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new SiteSettings();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((SiteSettings)element).Key;
        }

    }

    public class SiteSettings : ConfigurationElement 
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get { return this["key"].ToString(); }
            set { this["key"] = value; }
        }

        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get { return this["url"].ToString(); }
            set { this["url"] = value; }

        }
        [ConfigurationProperty("isCurrent", IsRequired = true)]
        public bool IsCurrent
        {
            get { return (bool)this["isCurrent"]; }
            set { this["isCurrent"] = value; }
        }
        [ConfigurationProperty("isClassicCba", IsRequired = true)]
        public bool IsClassicCba
        {
            get { return (bool)this["isClassicCba"]; }
            set { this["isClassicCba"] = value; }
        }
        [ConfigurationProperty("connection", IsRequired = true)]
        public string Connection
        {
            get { return this["connection"].ToString(); }
            set { this["connection"] = value; }
        }
        
    }
}
