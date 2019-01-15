using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Numero3.EntityFramework.Demo.DatabaseContext
{

    public class FocusSite
    {
        [XmlAttribute("Key")]
        public string Key { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("Url")]
        public string Url { get; set; }
        [XmlAttribute("SubDomain")]
        public string SubDomain { get; set; }
        [XmlAttribute("Domain")]
        public string Domain { get; set; }
        [XmlAttribute("Protocol")]
        public string Protocol { get; set; }
        [XmlAttribute("Server")]
        public string Server { get; set; }
        [XmlAttribute("IsClassicCba")]
        public bool IsClassicCba { get; set; }
        [XmlElement("DbConnectMain")]
        public string DbConnectMain { get; set; }
        [XmlElement("DbConnectCache")]
        public string DbConnectCache { get; set; }

        public FocusSite Default()
        {
            return new FocusSite()
            {
                Key = "connect",
                Name = "Connect",
                Url = "http://dev.server:8888/",
                IsClassicCba = false,
                DbConnectMain = "Server=localhost;Database=DbContextScopeDemo;Trusted_Connection=true;",
                DbConnectCache = "server=(local);uid=sa;pwd=P@ss1234;Connection Timeout=120;database=cache"
            };
        }
    }

    public static class SiteDic
    {
        public static readonly Dictionary<string, FocusSite> sites
            = new Dictionary<string, FocusSite>
            {
                {
                    "demo",
                    new FocusSite()
                    {
                        Key = "demo", Name = "Demo", Url = "http://dev.server:8888/", IsClassicCba = false,
                        DbConnectMain ="Server=localhost;Database=DbContextScopeDemo;Trusted_Connection=true;",
                        DbConnectCache = "Server=localhost;Database=DbContextScopeDemo;Trusted_Connection=true;"
                    }
                },
                {
                    "bak",
                    new FocusSite()
                    {
                        Key = "bak", Name = "Backup", Url = "http://localhost/", IsClassicCba = true,
                        DbConnectMain = "Server=localhost;Database=DbContextScopeDemo_bak;Trusted_Connection=true;",
                        DbConnectCache ="Server=localhost;Database=DbContextScopeDemo_bak;Trusted_Connection=true;"
                    }
                }
            };

        public static FocusSite GetFocusSite(string key)
        {
            if (sites.ContainsKey(key))
            {
                return sites[key];
            }

            return new FocusSite().Default();
        }
    }
}
