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
            Title = "View Notes";
            Icon = new("\uE70B");
            Name = "View Notes";

            notes = Directory.EnumerateFiles(Utils.NotesDirectory(), $"*.{Utils.Extension()}", SearchOption.TopDirectoryOnly)
                .Select(path => new ListItem(new NoOpCommand()) { Title = "Note", Subtitle = path, Icon = new IconInfo("\uE70B") })
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
                filteredNotes = notes.Where(i => i.Title.Contains(newSearch)).ToArray();
            }

            RaiseItemsChanged(filteredNotes.Length);
        }
    }
}
