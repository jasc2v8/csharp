//
//adapted from https://github.com/marcopacini/INI-Reader
//
// reads keys into string or entire file into dictionary, ignores [SECTIONS]
//

using System.Collections.Generic;
using System.IO;

namespace UTIL
{
    public class IniReader
    {
        private Dictionary<string,string> dictionary;
        private string iniFilePath;

        public IniReader(string filepath)
        {
            iniFilePath = filepath;
            dictionary = new Dictionary<string,string>();
            dictionary = IniReader.Getdictionary(filepath);
        }
        public bool Exists()
        {
            if (File.Exists(iniFilePath)) { return true; } else { return false; }
        }
        public string GetValue(string key)
        {
            if (dictionary.ContainsKey(key)) {
                return dictionary[key];
            } else
            {
                return null;
            }
        }
        public Dictionary<string,string> GetDictionary()
        {
            return dictionary;
        }
        private static Dictionary<string,string> Getdictionary(string filepath)
        {
            var data = new Dictionary<string,string>();

            if (File.Exists(filepath))
            {
                foreach (var row in File.ReadAllLines(filepath))
                {
                    string r = row.Trim();

                    if ((!r.StartsWith("[")) && (!r.StartsWith(";")))
                    {
                        data.Add(r.Split('=')[0].Trim(), r.Split('=')[1].Trim());
                    }
                }
            }
            return data;
        }           
    }
}
