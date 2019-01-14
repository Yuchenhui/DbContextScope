using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Numero3.EntityFramework.Demo.DatabaseContext
{

    public class FocusSite
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string SubDomain { get; set; }
        public string Domain { get; set; }
        public string Protocol { get; set; }
        public string Server { get; set; }
        public bool IsClassicCba { get; set; }

        public string DbConnectMain { get; set; }
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
