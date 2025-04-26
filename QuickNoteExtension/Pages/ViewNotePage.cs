using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

namespace QuickNoteExtension.Pages
{
    internal sealed partial class ViewNotePage : DynamicListPage
    {
        public ViewNotePage()
        {
            Title = "View Note";
            Icon = new("\uE70B");
            Name = "View";
            shownItems = items;
        }

        private ListItem[] items = new ListItem[]
        {
                   new ListItem(new NoOpCommand()) { Title = "One" },
                   new ListItem(new NoOpCommand()) { Title = "Two" },
                   new ListItem(new NoOpCommand()) { Title = "Three" },
        };

        private ListItem[] shownItems;

        public override IListItem[] GetItems()
        {
            return shownItems;
        }

        public override void UpdateSearchText(string oldSearch, string newSearch)
        {
            if (oldSearch == newSearch)
            {
                shownItems = items;
            }
            else
            {
                shownItems = items.Where(i => i.Title.Contains(newSearch)).ToArray();
            }

            RaiseItemsChanged(shownItems.Length);
        }
    }
}
