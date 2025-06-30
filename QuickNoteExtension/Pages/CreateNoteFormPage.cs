using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.CommandPalette.Extensions;
using Microsoft.CommandPalette.Extensions.Toolkit;


namespace QuickNoteExtension.Pages
{
    internal sealed partial class CreateNoteFormPage : ContentPage
    {
        public CreateNoteFormPage() {
            Title = "New Note";
            Icon = new("\uE8FF");
            Name = "Create Note";
        }

        public override IContent[] GetContent()
        {
            return [new CreateNoteForm()];
        }
    }

    internal sealed partial class CreateNoteForm : FormContent
    {
        public CreateNoteForm() {
            TemplateJson = $$"""
            {
                "type": "AdaptiveCard",
                "$schema": "http://adaptivecards.io/schemas/adaptive-card.json",
                "version": "1.6",
                "body": [
                    {
                        "type": "Input.Text",
                        "placeholder": "Note title",
                        "label": "Title",
                        "isRequired": true,
                        "id": "title",
                        "errorMessage": "Title is required"
                    },
                    {
                        "type": "Input.Text",
                        "placeholder": "Write your note contents here...",
                        "isMultiline": true,
                        "label": "Content",
                        "id": "content",
                        "height": "stretch",
                        "spacing": "large",
                        "isRequired": true,
                        "errorMessage": "Contents are required"
                    },
                    {
                        "type": "ActionSet",
                        "actions": [
                            {
                                "type": "Action.Submit",
                                "title": "Save",
                                "id": "submit",
                                "tooltip": "Create Note",
                                "role": "Button"
                            }
                        ],
                        "spacing": "large",
                        "separator": true
                    }
                ]
            }
            """;
        }

        public override CommandResult SubmitForm(string payload)
        {
            if (string.IsNullOrWhiteSpace(payload))
            {
                return CommandResult.ShowToast("No data received from the form.");
            }

            // Parse the JSON payload
            var formInput = JsonNode.Parse(payload);
            if (formInput == null)
            {
                return CommandResult.ShowToast("Failed to parse form data.");
            }

            // Retrieve the title and contents
            var title = formInput["title"]?.GetValue<string>() ?? "Untitled";
            var contents = formInput["content"]?.GetValue<string>() ?? "";

            // Generate the filePath for the note
            (string filePath, string fileName) = Utils.NotePath(title);

            // Save the quick-note on disk
            try
            {
                File.WriteAllText(filePath, contents);
            }
            catch (Exception ex)
            {
                return CommandResult.ShowToast($"Error saving note: {ex.Message}");
            };

            // Show a toast to confirm saving the note
            return CommandResult.ShowToast($"Note saved: {fileName}");
        }
    }
}
