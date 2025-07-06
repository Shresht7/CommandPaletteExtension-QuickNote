using System;
using System.IO;
using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;

using QuickNoteExtension.Assets;

namespace QuickNoteExtension.Commands
{
    internal sealed partial class SaveNoteCommand : InvokableCommand 
    {
        string Content { get; set; } = "";

        public SaveNoteCommand(string? contents = "")
        {
            Name = "Save Note";
            Icon = Icons.QuickNote;
            Content = contents ?? "";
        }

        public override ICommandResult Invoke()
        {
            (string filePath, string fileName) = Utils.NotePath("note");

            try
            {
                File.WriteAllText(filePath, Content);
            }
            catch (Exception ex)
            {
                return CommandResult.ShowToast($"Error saving file: {ex.Message}");
            }

            return CommandResult.ShowToast($"Saved note to {fileName}");
        }
    }
}
