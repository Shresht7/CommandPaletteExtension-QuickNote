using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using QuickNoteExtension.Assets;

namespace QuickNoteExtension.Pages
{
internal sealed partial class ViewNotesPage : DynamicListPage
{
    private readonly List<ListItem> _notes;
    private string _searchText = string.Empty;

    public ViewNotesPage()
    {
        Icon = Icons.TaskView;
        Title = "View Notes";
        Name = "View Notes";
        ShowDetails = true;

        try
        {
            _notes = Directory.EnumerateFiles(Utils.NotesDirectory(), $"*.{Utils.Extension()}", SearchOption.TopDirectoryOnly)
                .OrderByDescending(path => File.GetLastWriteTime(path))
                .Select(CreateNoteListItem)
                .ToList();
        }
        catch (Exception ex)
        {
            _notes =
            [
                new(new NoOpCommand())
                {
                    Title = "Error loading notes",
                    Subtitle = ex.Message,
                    Icon = Icons.ErrorBadge
                }
            ];
        }
    }

    public override IListItem[] GetItems()
    {
        if (string.IsNullOrWhiteSpace(_searchText))
        {
            return _notes.ToArray();
        }

        return _notes.Where(FilterNotesBySearch(_searchText)).ToArray();
    }

    public override void UpdateSearchText(string oldSearch, string newSearch)
    {
        if (oldSearch == newSearch)
        {
            return;
        }

        _searchText = newSearch;
        RaiseItemsChanged();
    }

    private static Func<ListItem, bool> FilterNotesBySearch(string newSearch)
    {
        return i =>
            i.Title.Contains(newSearch, StringComparison.CurrentCultureIgnoreCase)
            || i.Subtitle.Contains(newSearch, StringComparison.CurrentCultureIgnoreCase);
    }

    private static ListItem CreateNoteListItem(string path)
    {
        try
        {
            string contents = File.ReadAllText(path);
            string title = DetermineTitle(path, contents);

            return new ListItem(new ViewNoteContentPage(title, path))
            {
                Title = title,
                Subtitle = path,
                Icon = Icons.QuickNote,
                Details = new Details()
                {
                    Title = title,
                    Body = contents,
                },
                MoreCommands =
                [
                   new CommandContextItem(new CopyTextCommand(contents) { Name = "Copy Contents" }),
                   new CommandContextItem(new CopyTextCommand(path) { Name = "Copy Path" }),
                ]
            };
        }
        catch (Exception ex)
        {
            return new ListItem(new NoOpCommand())
            {
                Title = Path.GetFileName(path),
                Subtitle = $"Error reading file: {ex.Message}",
                Icon = Icons.ErrorBadge,
            };
        }
    }

    private static string DetermineTitle(string path, string contents)
    {
        var extension = Path.GetExtension(path).ToLowerInvariant();
        string? title = null;

        using (var reader = new StringReader(contents))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                if (extension == ".md" || extension == ".markdown")
                {
                    if (line?.StartsWith("# ", StringComparison.Ordinal) == true)
                    {
                        title = line.Substring(2).Trim();
                        break;
                    }
                }
                else
                {
                    title = line.Trim();
                    break;
                }
            }
        }

        return title ?? Path.GetFileNameWithoutExtension(path);
    }
}
}
