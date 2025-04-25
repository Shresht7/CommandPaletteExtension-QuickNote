using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

namespace QuickNoteExtension;

internal sealed partial class SaveClipboardCommand : InvokableCommand
{
    public SaveClipboardCommand()
    {
        Name = "Save Clipboard";
        Icon = new("\uF0E3");
    }

    public override ICommandResult Invoke()
    {
        string text = ClipboardHelper.GetText();
        if (string.IsNullOrEmpty(text))
        {
            return CommandResult.ShowToast("The clipboard is empty");
        }

        (string filePath, string fileName) = Utils.NotePath("clipboard");

        try
        {
            File.WriteAllText(filePath, text);
        }
        catch (Exception ex)
        {
            return CommandResult.ShowToast($"Error saving file: {ex.Message}");
        }

        return CommandResult.ShowToast($"Saved clipboard to {fileName}");
    }
}
