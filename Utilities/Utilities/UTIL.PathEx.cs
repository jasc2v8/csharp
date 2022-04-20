//
//  OG
//      string path = @"C:\Path";
//      System.IO.Path.GetFullPath(path)
//
//  Custom Extension = UTIL.PathEx
//      string path = @"C:\Path";
//      path.GetFullPath()
//
using System.IO;
namespace UTIL
{
    public static class PathEx
    {
        public static string FullPath(this string path) { return Path.GetFullPath(path); }
        public static string Dir(this string path) { return Path.GetDirectoryName(path); }
        public static string ParentDir(this string path) { return Directory.GetParent(Path.GetDirectoryName(path)).ToString(); }
        public static string FileName(this string path) { return Path.GetFileName(path); }
        public static string NameNoExt(this string path) { return Path.GetFileNameWithoutExtension(path); }
        public static string Drive(this string path) { return Path.GetFullPath(path).Substring(0, 1); }
        public static string Ext(this string path) { return Path.GetExtension(path); }
        public static string Root(this string path) { return Path.GetPathRoot(path); }
        public static bool Exists(this string path) { return File.Exists(path); }
        public static bool IsRooted(this string path) { return Path.IsPathRooted(path); }
        public static bool HasExt(this string path) { return Path.HasExtension(path); }
        public static char PathSeparator(this string path) { return Path.PathSeparator; }
        public static char DirSeparator(this string path) { return Path.DirectorySeparatorChar; }
        public static char VolSeparator(this string path) { return Path.VolumeSeparatorChar; }

    }
}