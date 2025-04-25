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
        Icon = new("\uE70B");
        _commands = [
            new CommandItem(new QuickNoteExtensionPage()) { Title = DisplayName, Subtitle = "Take a quick note" },
        ];
        Settings = QuickNoteExtensionSettings.Instance.Settings;
    }

    public override ICommandItem[] TopLevelCommands()
    {
        return _commands;
    }

}
