/* CodeParser
 * Parses C# source code to get a list of:
 *  - Methods and parameters
 *  - Types: "class", "enum", "struct", "record" 
 *  - Other: "namespace", "#region"
 * 
 * Known limitation:
 * If accessiblity word not present (private, public, etc.),
 *  then the return type must be void,
 *      Will Parse : void OnDragEnter(object sender, DragEventArgs e)
 *      Won't Parse: string Method(type, parameter)
 */

using System;
using System.Collections.Generic;
//using UTIL;

namespace UTIL
{

    public class ListItem
    {
        public string Text { get; set; }
        public int Number { get; set; }
    }

    public class CodeParser
    {
        LogFile Log = new LogFile(@"D:\CodeParser.log");


        private readonly string[] AccessibilityWords = 
            { "public", "private", "protected", "static", "internal", "void" };

        private readonly string[] TypeWords = 
            { "class", "enum", "struct", "record" };

        public CodeParser()
        {
            
        }
        private string _GetFirstWord(string line)
        {
            int i = line.IndexOf(' ');
            if (i == -1) return string.Empty;
            return line.Substring(0, i).Trim();
        }

        private bool _IsAccessiblityWord(string word)
        {
            foreach (string accessibilityWord in AccessibilityWords)
            {
                if (word == accessibilityWord)
                    return true;
            }
            return false;
        }
        private string _GetTypeLine(string line)
        {
            foreach (string typeWord in TypeWords)
            {
                if (line.Contains(typeWord + " "))
                {
                    int j = line.IndexOf(typeWord);
                    return line.Substring(j).Trim();
                }
            }
            return string.Empty;
        }

        private string[] _GetMethodAndParams(string line)
        {
            //methodArray[0] = methodName
            //methodArray[1] = params

            string[] methodArray = { string.Empty, string.Empty };

            //methods
            int p1 = line.IndexOf("(");
            int p2 = line.IndexOf(")");

            if (p1 > -1 & p2 > p1)
            {

                //if '=' or '.' before '(' then not a method
                string segment = line.Substring(0, p1);

                if (segment.Contains("=") || segment.Contains("."))
                    return methodArray;

                //get method name
                string methodName = string.Empty;

                int j = 0;

                for (j = segment.Length - 1; j > 1; j--)
                {
                    if (segment.Substring(j, 1) == " ")
                        break;
                }

                if (j > 0)
                    methodName = segment.Substring(j + 1, segment.Length - 1 - j);
                else
                    methodName = "ERROR";

                methodArray[0] = methodName;

                //parameters
                string paramTypes = "(";

                if (p2 - p1 == 1)
                {//Method()
                    paramTypes = "";
                }
                else
                {//Method(type, param)

                    //get param types, from: (string s, int i), to: (string, int)
                    string param = line.Substring(p1 + 1, p2 - p1 - 1);

                    string[] splitComma = param.Split(',');

                    for (int iComma = 0; iComma < splitComma.Length; iComma++)
                    {
                        string[] splitSpace = splitComma[iComma].Trim().Split(' ');

                        if (splitSpace[0] == "out")
                            paramTypes += splitSpace[1];
                        else
                            paramTypes += splitSpace[0];

                        paramTypes += ",";

                    }
                    paramTypes = paramTypes.Substring(0, paramTypes.Length - 1) + ")";
                    methodArray[1] = paramTypes;
                }
            }

            return methodArray;
        }
        public List<ListItem> ParseAllText(string buffer)
        {
            //Log.Clear();
            //Log.Info("Log cleared");

            //split at LF in case not CRLF format. Trim() will remove the trailing CR
            string[] lines = buffer.Split(new[] { '\n' }, StringSplitOptions.None);

            List<ListItem> codeList = new List<ListItem>();

            bool IsBlockComment = false;

            string savedLine = string.Empty;

            for (int i = 0; i < lines.Length; i++)
            {
                //remove leading space and trailing CR if any
                string line = lines[i].Trim();

                //comments and block comments
                if (line.StartsWith("/*"))
                {
                    IsBlockComment = true;
                    continue;
                }

                if (line.Contains("*/"))
                {
                    IsBlockComment = false;
                    continue;
                }

                if (IsBlockComment)
                    continue;

                if (line.StartsWith("//"))
                    continue;

                if (line == String.Empty)
                    continue;

                //parameter continuation lines
                if (line.Contains("(") & !line.Contains(")"))
                {
                    savedLine += line;
                    continue;
                }

                if (savedLine != String.Empty & line.EndsWith(")"))
                {
                    line = savedLine + line;
                    savedLine = String.Empty;
                }

                if (savedLine != String.Empty & !line.EndsWith(")"))
                {
                    savedLine += line;
                    continue;
                }

                string firstWord = _GetFirstWord(line);

                if (_IsAccessiblityWord(firstWord))
                {

                    string typeLine = _GetTypeLine(line);
                    if (typeLine != string.Empty)
                    {
                        ListItem itemType = new ListItem { Text = typeLine, Number = i };
                        codeList.Add(itemType);
                        continue;
                    }

                    string[] methodArray = _GetMethodAndParams(line);

                    string methodName = methodArray[0];
                    string paramTypes = methodArray[1];

                    if (!methodName.Equals(string.Empty))
                    {
                        ListItem item = new ListItem { Text = methodName + paramTypes, Number = i };
                        codeList.Add(item);
                    }
                }
                if (firstWord.Trim() == "namespace")
                {
                    ListItem item = new ListItem { Text = line, Number = i };
                    codeList.Add(item);
                    continue;
                }

                if (firstWord.Trim().StartsWith("#region"))
                {
                    ListItem item = new ListItem { Text = line, Number = i };
                    codeList.Add(item);
                    continue;
                }

            }
            return codeList;
        }
    }
}