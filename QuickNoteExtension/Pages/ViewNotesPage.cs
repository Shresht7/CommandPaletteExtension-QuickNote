using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace QuickNoteExtension.Pages
{
    internal sealed partial class ViewNotesPage : DynamicListPage
    {
        private readonly ListItem[] notes;
        private ListItem[] filteredNotes;

        public ViewNotesPage()
        {
            Icon = new("\uE70B");
            Title = "View Notes";
            Name = "View Notes";
            ShowDetails = true;

            notes = Directory.EnumerateFiles(Utils.NotesDirectory(), $"*.{Utils.Extension()}", SearchOption.TopDirectoryOnly)
                .Select(CreateNoteListItem)
                .ToArray();
            filteredNotes = notes;
        }

        public override IListItem[] GetItems()
        {
            return filteredNotes;
        }

        public override void UpdateSearchText(string oldSearch, string newSearch)
        {
            if (oldSearch == newSearch)
            {
                filteredNotes = notes;
            }
            else
            {
                filteredNotes = notes.Where(FilterNotesBySearch(newSearch)).ToArray();
            }

            RaiseItemsChanged(filteredNotes.Length);
        }

        static Func<ListItem, bool> FilterNotesBySearch(string newSearch)
        {
            return i =>
                i.Title.Contains(newSearch, StringComparison.CurrentCultureIgnoreCase)
                || i.Subtitle.Contains(newSearch, StringComparison.CurrentCultureIgnoreCase);
        }

        static ListItem CreateNoteListItem(string path)
        {
            string contents = File.ReadAllText(path);
            string title = "Note";
            return new ListItem(new ViewNoteContentPage("Note", path))
            {
                Title = title,
                Subtitle = path,
                Icon = new IconInfo("\uE70B"),
                Details = new Details()
                {
                    Title = title,
                    Body = contents,
                }
            };
        }


    }
}
