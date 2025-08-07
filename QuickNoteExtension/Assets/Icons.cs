using Microsoft.CommandPalette.Extensions.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickNoteExtension.Assets
{
    internal static class Icons
    {
        public static IconInfo QuickNote { get; } = new IconInfo("\uE70B");
        public static IconInfo ClipboardList { get; } = new IconInfo("\uF0E3");
        public static IconInfo Preview { get; } = new IconInfo("\uE8FF");
        public static IconInfo TaskView { get; } = new IconInfo("\uE7C4");
        public static IconInfo ErrorBadge { get; } = new IconInfo("\uEA39");
        public static IconInfo Delete { get; } = new IconInfo("\uE74D");
    }
}
