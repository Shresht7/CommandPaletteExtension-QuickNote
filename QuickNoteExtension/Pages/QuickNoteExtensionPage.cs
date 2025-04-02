// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using QuickNoteExtension.Pages;

namespace QuickNoteExtension;

internal sealed partial class QuickNoteExtensionPage : ListPage
{
    public QuickNoteExtensionPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StickyNoteIcon.png");
        Title = "Quick Note";
        Name = "Open";
    }

    public override IListItem[] GetItems()
    {
        return [
            new ListItem(new CreateNoteFormPage()) { Title = "Create a new note", Subtitle = "Quickly save a note to desktop" },
            new ListItem(new SaveClipboardCommand()) { Title = "Save clipboard", Subtitle = "Quickly create a note using the clipboard contents" }
        ];
    }
}
