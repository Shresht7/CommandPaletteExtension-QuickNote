using System;
using System.IO;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using QuickNoteExtension.Assets;

namespace QuickNoteExtension.Commands;

internal sealed partial class DeleteNoteCommand : InvokableCommand
{
    private readonly string _filePath;

    public DeleteNoteCommand(string filePath)
    {
        _filePath = filePath;
        Name = "Delete";
        Icon = Icons.Delete;
    }

    public override ICommandResult Invoke()
    {
        var confirmationArgs = new ConfirmationArgs()
        {
            Title = "Delete Note",
            Description = $"Are you sure you want to delete the note: {Path.GetFileName(_filePath)}?",
            PrimaryCommand = new PerformDeleteCommand(_filePath),
            IsPrimaryCommandCritical = true,
        };
        return CommandResult.Confirm(confirmationArgs);
    }
}

internal sealed partial class PerformDeleteCommand : InvokableCommand
{
    private readonly string _filePath;

    public PerformDeleteCommand(string filePath)
    {
        _filePath = filePath;
        Name = "Delete";
    }

    public override ICommandResult Invoke()
    {
        try
        {
            File.Delete(_filePath);
            return CommandResult.ShowToast($"Deleted note: {Path.GetFileName(_filePath)}");
        }
        catch (Exception ex)
        {
            return CommandResult.ShowToast($"Error deleting note: {ex.Message}");
        }
    }
}