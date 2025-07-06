using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;

using QuickNoteExtension.Assets;

namespace QuickNoteExtension.Pages
{
    internal sealed partial class ViewNoteContentPage : ContentPage
    {
        string Path { get; set; }   

        public ViewNoteContentPage(string title, string path)
        {
            Name = "View";
            Title = title;
            Icon = Icons.TaskView;
            Path = path;
        }

        public override IContent[] GetContent()
        {
            return [
                new MarkdownContent(System.IO.File.ReadAllText(Path))
            ];
        }
    }
}
