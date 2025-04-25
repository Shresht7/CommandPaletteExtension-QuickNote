using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNoteExtension;
public class QuickNoteExtensionSettings: JsonSettingsManager
{
    public static readonly QuickNoteExtensionSettings Instance = new();

    public QuickNoteExtensionSettings()
    {
        FilePath = GetSettingsPath();

        Settings.Add(NotesPath);

        LoadSettings();

        Settings.SettingsChanged += (s, a) => { };
    }

    public readonly TextSetting NotesPath = new(
        "path",
        "Path",
        "Path to the directory to save the notes",
        Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    );

    internal static string GetSettingsPath()
    {
        var directory = Utilities.BaseSettingsPath("Shresht7.QuickNote");
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, "settings.json");
    }
}
