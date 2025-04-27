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
        Title = "Quick Note";
        Icon = new("\uE70B");
        Name = "Open";
    }

    readonly static string defaultSubtext = "Type something...";
    ListItem quickNote = new(new NoOpCommand()) { Title = "Quick note", Subtitle = defaultSubtext, Icon = new IconInfo("\uE70B") };

    public override IListItem[] GetItems()
    {
        return [
            quickNote,
            new ListItem(new CreateNoteFormPage()) { Title = "Create note", Subtitle = "Create a new note" },
            new ListItem(new SaveClipboardCommand()) { Title = "Save clipboard", Subtitle = "Quickly save the clipboard contents" },
            new ListItem(new ViewNotesPage()) { Title = "View Notes", Subtitle = "View existing notes" },
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
