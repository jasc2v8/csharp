//
//  OG
//      string path = @"C:\Path";
//      System.IO.Path.GetFullPath(path);
//
//  Class Object = UTIL.SplitPath
//      path = new SplitPath(path)
//      string fullPath = path.FullPath;
//

using System.IO;
namespace UTIL
{
    public class SplitPath
    {
        private static string path;
        public SplitPath(string pPath)
        {
            path = pPath;
        }
        public string FullPath { get { return Path.GetFullPath(path); } }
        public string Dir { get { return Path.GetDirectoryName(path); } }
        public string ParentDir { get { return Directory.GetParent(Path.GetDirectoryName(path)).ToString(); } }
        public string FileName { get { return Path.GetFileName(path); } }
        public string NameNoExt { get { return Path.GetFileNameWithoutExtension(path); } }
        public string Drive { get { return Path.GetFullPath(path).Substring(0,1); } }
        public string Ext { get { return Path.GetExtension(path); } }
        public string Root { get { return Path.GetPathRoot(path); } }
        public bool Exists { get { return File.Exists(path); } }
        public bool IsRooted { get { return Path.IsPathRooted(path); } }
        public bool HasExt { get { return Path.HasExtension(path); } }
        public char PathSeparator { get { return Path.PathSeparator; } }
        public char DirSeparator { get { return Path.DirectorySeparatorChar; } }
        public char VolSeparator { get { return Path.VolumeSeparatorChar; } }
    }
}
