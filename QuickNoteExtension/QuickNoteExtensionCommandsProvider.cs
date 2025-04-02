// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace QuickNoteExtension;

public partial class QuickNoteExtensionCommandsProvider : CommandProvider
{
    private readonly ICommandItem[] _commands;

    public QuickNoteExtensionCommandsProvider()
    {
        DisplayName = "Quick Note";
        Icon = IconHelpers.FromRelativePath("Assets\\StickyNoteIcon.png");
        _commands = [
            new CommandItem(new QuickNoteExtensionPage()) { Title = DisplayName },
        ];
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
