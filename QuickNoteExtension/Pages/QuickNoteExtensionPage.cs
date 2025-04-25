// Copyright (c) Microsoft Corporation
// The Microsoft Corporation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using QuickNoteExtension.Commands;
using QuickNoteExtension.Pages;

namespace QuickNoteExtension;

internal sealed partial class QuickNoteExtensionPage : DynamicListPage
{
    public QuickNoteExtensionPage()
    {
        Icon = IconHelpers.FromRelativePath("Assets\\StickyNoteIcon.png");
        Title = "Quick Note";
        Name = "Open";
    }

    readonly static string defaultSubtext = "Type something...";
    ListItem quickNote = new(new NoOpCommand()) { Title = "Save note", Subtitle = defaultSubtext };

    public override IListItem[] GetItems()
    {
        return [
            quickNote,
            new ListItem(new CreateNoteFormPage()) { Title = "Create a new note", Subtitle = "Quickly save a note to desktop" },
            new ListItem(new SaveClipboardCommand()) { Title = "Save clipboard", Subtitle = "Quickly create a note using the clipboard contents" },
        ];
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        // Only update if there's a meaningful change
        if (oldSearch == newSearch) return;

        // Update the Text contents
        quickNote.Subtitle = string.IsNullOrEmpty(newSearch) ? defaultSubtext : newSearch;

        // Update the command based on the query-Text
        quickNote.Command = new SaveNoteCommand(newSearch);

        RaiseItemsChanged(); // Responsible for indicating that the item needs to re-rendered
    }
}
