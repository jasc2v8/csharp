//
// TexBoxEx tb = new TexBoxEx(testBoxName);
//
using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace UTIL
{
    public class TextBoxEx
    {
        private readonly TextBox textBox;

        public TextBoxEx(TextBox pTextBoxName)
        {
            textBox = pTextBoxName;
        }
        public void Append(string text)
        {
            textBox.AppendText(text + Environment.NewLine);
        }
        public void Clear()
        {
            textBox.Clear();
        }
        public void Write(string text)
        {
            textBox.Text = text;
        }
        public void WriteLine(string text)
        {
            textBox.AppendText(text + Environment.NewLine);
        }
    }
}
