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
            Icon = new("\uE7C4");
            Title = "View Notes";
            Name = "View Notes";
            ShowDetails = true;

            try
            {
                notes = Directory.EnumerateFiles(Utils.NotesDirectory(), $"*.{Utils.Extension()}", SearchOption.TopDirectoryOnly)
                    .OrderByDescending(path => File.GetLastWriteTime(path))
                    .Select(CreateNoteListItem)
                    .ToArray();
            }
            catch (Exception ex)
            {
                notes =
                [
                    new(new NoOpCommand())
                    {
                        Title = "Error loading notes",
                        Subtitle = ex.Message,
                        Icon = new IconInfo("\uEA39")
                    }
                ];
            }
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
            try
            {
                string contents = File.ReadAllText(path);
                string title = DetermineTitle(path, contents);

                return new ListItem(new ViewNoteContentPage(title, path))
                {
                    Title = title,
                    Subtitle = path,
                    Icon = new IconInfo("\uE70B"),
                    Details = new Details()
                    {
                        Title = title,
                        Body = contents,
                    },
                    MoreCommands = [
                       new CommandContextItem("Copy") {
                           Command = new AnonymousCommand(() => ClipboardHelper.SetText(contents)) {
                               Name = "Copy Contents",
                               Result = CommandResult.ShowToast("Copied contents to clipboard")
                           }
                       }
                    ]
                };
            }
            catch (Exception ex)
            {
                return new ListItem(new NoOpCommand())
                {
                    Title = Path.GetFileName(path),
                    Subtitle = $"Error reading file: {ex.Message}",
                    Icon = new IconInfo("\uEA39"),
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
