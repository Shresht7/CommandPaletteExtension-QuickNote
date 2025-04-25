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
        LoadSettings();
        Settings.SettingsChanged += (s, a) => { };
    }

    internal static string GetSettingsPath()
    {
        var directory = Utilities.BaseSettingsPath("Shresht7.QuickNote");
        Directory.CreateDirectory(directory);
        return Path.Combine(directory, "settings.json");
    }
}
