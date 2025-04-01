using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNoteExtension.Pages
{
    internal sealed partial class CreateNoteFormPage : ContentPage
    {
        public CreateNoteFormPage() {
            Icon = IconHelpers.FromRelativePath("Assets\\StoreLogo.png");
            Title = "New Note";
            Name = "Create Note";
        }

        public override IContent[] GetContent()
        {
            return [formContent];
        }

        readonly FormContent formContent = new()
        {
            TemplateJson = @"
            {
                ""type"": ""AdaptiveCard"",
                ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
                ""version"": ""1.6"",
                ""body"": [
                    {
                        ""type"": ""TextBlock"",
                        ""text"": ""Title"",
                        ""wrap"": true
                    },
                    {
                        ""type"": ""Input.Text"",
                        ""id"": ""title"",
                        ""placeholder"": ""Note title""
                    },
                    {
                        ""type"": ""TextBlock"",
                        ""text"": ""Contents"", 
                        ""wrap"": true
                    },
                    {
                        ""type"": ""Input.Text"",
                        ""id"": ""content"",
                        ""placeholder"": ""Write your note contents..."",
                        ""isMultiline"": true
                    }
                ],
                ""actions"": [
                    {
                        ""type"": ""Action.Submit"",
                        ""title"": ""Save Note""
                    }
                ]
            }
        "
        };

    }
}
