using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNoteExtension;

internal class Utils
{
    public static string NotesDirectory()
    {
        return QuickNoteExtensionSettings.Instance.NotesPath.Value ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
    }

    public static (string filePath, string fileName) NotePath(string Title = "note", string Extension = "txt")
    {
        string directory = NotesDirectory();
        string fileName = $"{Title}-{Timestamp()}.{Extension}";
        string filePath = Path.Combine(directory, fileName);
        return (filePath, fileName);
    }

    private static string Timestamp()
    {
        return $"{DateTime.Now:yyyyMMdd - HHmmss}";
    }
}

