﻿using System;
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

    public static (string filePath, string fileName) NotePath(string Title = "note")
    {
        string directory = NotesDirectory();
        string fileName = $"{Title}{Timestamp()}.{Extension()}";
        string filePath = Path.Combine(directory, fileName);
        return (filePath, fileName);
    }

    public static string Extension()
    {
        var extension = QuickNoteExtensionSettings.Instance.Extension.Value ?? "txt";
        if (extension.StartsWith('.'))
        {
            extension = extension.Substring(1);
        }
        return extension;
    }

    private static string Timestamp()
    {
        if (QuickNoteExtensionSettings.Instance.Timestamp.Value)
        {
            return $"__{DateTime.Now:yyyyMMdd_HHmmss}";
        } else
        {
            return "";
        }
    }
}

