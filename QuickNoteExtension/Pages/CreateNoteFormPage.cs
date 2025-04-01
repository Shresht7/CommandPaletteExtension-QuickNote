using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNoteExtension.Pages
{
    internal sealed partial class CreateNoteFormPage: ContentPage
    {
        public CreateNoteFormPage() {
            Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
            Title = "New Note";
            Name = "Create Note";
        }

        public override IContent[] GetContent()
        {
            return [
                new MarkdownContent("# Hello, world!\n This is a **markdown** page."),
            ];
        }
    }
}
