using Microsoft.CommandPalette.Extensions.Toolkit;
using Microsoft.CommandPalette.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace QuickNoteExtension.Commands
{
    internal sealed partial class SaveNoteCommand : InvokableCommand 
    {
        /// <summary>
        /// The text contents of the note
        /// </summary>
        string Text { get; set; } = "";

        public SaveNoteCommand(string? contents = "")
        {
            Name = "Save Note";
            Icon = new("\uE70B");
            Text = contents ?? "";
        }

        public override ICommandResult Invoke()
        {
            string directory = QuickNoteExtensionSettings.Instance.NotesPath.Value ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filename = $"Note-{DateTime.Now:yyyyMMdd-HHmmss}.txt";
            string filePath = Path.Combine(directory, filename);

            try
            {
                File.WriteAllText(filePath, Text);
            }
            catch (Exception ex)
            {
                return CommandResult.ShowToast($"Error saving file: {ex.Message}");
            }

            return CommandResult.ShowToast($"Saved note to {filename}");
        }
    }
}
