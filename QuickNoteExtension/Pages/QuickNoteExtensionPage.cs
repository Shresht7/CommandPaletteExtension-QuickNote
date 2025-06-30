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
        Icon = new("\uE70B");
        Title = "Quick Note";
        Name = "Open";
        PlaceholderText = "Type note contents here...";
    }

    private readonly ListItem quickNote = new(new NoOpCommand()) {
        Icon = new IconInfo("\uE70B"),
        Title = "Quick Note",
        Subtitle = "Type something...",
    };

    public override IListItem[] GetItems()
    {
        return [
            quickNote,
            new ListItem(new SaveClipboardCommand()) { Title = "Save Clipboard", Subtitle = "Quickly save the clipboard contents to a note" },
            new ListItem(new CreateNoteFormPage()) { Title = "Create Note", Subtitle = "Create a new note" },
            new ListItem(new ViewNotesPage()) { Title = "View Notes", Subtitle = "View existing notes" },
        ];
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        // Only update if there's a meaningful change
        if (oldSearch == newSearch) return;

        // Update the quickNote command subtitle
        quickNote.Subtitle = string.IsNullOrEmpty(newSearch) ? "Type something..." : "Quickly save the content to a note";

        // Update the quickNote command based on the query-text
        quickNote.Command = new SaveNoteCommand(newSearch);

        RaiseItemsChanged(); // Responsible for indicating that the item needs to re-rendered
    }
}
