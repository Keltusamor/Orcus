using System;
using System.Collections.Generic;
using System.Text;
using Gtk;

namespace Orcus.GtkSharp3.Extensions
{
    public static class VBoxExtensions
    {
        public static void AddLabelRow(this VBox parent, string label, Label labelWidget, int spacing = 5)
        {
            var row = new HBox(false, spacing);
            parent.PackStart(row, false, false, 0);

            row.PackStart(new Label(label), false, false, 0);
            row.PackStart(labelWidget, false, false, 0);
        }

        public static void AddEntryRow(this VBox parent, string label, Entry entry, EventHandler sendClickEventHandler = null, string buttonLabel = "Send", int spacing = 5)
        {
            var row = new HBox(false, spacing);
            parent.PackStart(row, false, false, 0);

            row.PackStart(new Label(label), false, false, 0);
            row.PackStart(entry, true, true, 0);

            if (sendClickEventHandler != null)
            {
                var button = new Button
                {
                    Label = buttonLabel
                };
                button.Clicked += sendClickEventHandler;
                row.PackStart(button, false, false, 0);
            }
        }

        public static void AddCheckButton(this VBox parent, CheckButton checkButton, EventHandler eventHandler = null, int spacing = 5, string label = "")
        {
            var row = new HBox(false, spacing);
            parent.PackStart(row, false, false, 0);

            row.Add(checkButton);

            checkButton.Label = label;

            if (eventHandler != null)
                checkButton.Clicked += eventHandler;
        }
    }
}
