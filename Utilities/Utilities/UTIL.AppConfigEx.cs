//
//  https://yizeng.me/2013/08/31/update-appsettings-and-custom-configuration-sections-in-appconfig-at-runtime/ 
//
//  config.Save() writes temp file so user must have R/W access
//  this will FAIL if app.exe.config installed in ProgramFiles\app.exe
//
//  AppConfigEx config = new AppConfigEx();
//
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;

namespace UTIL
{
    public class AppConfigEx
    {
        private readonly Configuration config;

        public AppConfigEx()
        {
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        }
        public bool Exists()
        {
            if (File.Exists(config.FilePath)) { return true; } else { return false; } 
        }
        public string Get(string key)
        {
            return config.AppSettings.Settings[key].Value;
        }
        public void Set(string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
            config.Save();
        }
        public NameValueCollection GetAppSettings()
        {
            return ConfigurationManager.AppSettings;
        }
        public SortedDictionary<string, string> GetDictionary()
        {
            NameValueCollection appSettings = GetAppSettings();

            SortedDictionary<string, string> dict = new SortedDictionary<string, string>();

            foreach (var key in appSettings.AllKeys)
            {
                dict.Add(key, appSettings[key]);
            }

            return dict;
        }
        public string GetPath()
        {
            return config.FilePath;
        }
    }
}
