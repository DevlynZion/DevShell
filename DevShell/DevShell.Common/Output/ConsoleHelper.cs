using DevShell.Common.ConsoleUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Common.Output
{
    public class ConsoleHelper
    {
        public static void ListMenu(string description, IEnumerable<ConsoleMenuItem> menuItems)
        {
            var menu = new ConsoleMenu<string>(description, menuItems.ToArray());
            menu.RunConsoleMenu();
        }

        private static string itemSelected;
        public static int ListMenuParams(string description, params string[] options)
        {
            var menuItems = options.Select(o => new ConsoleMenuItem<string>(o, itemCallback, o));

            ListMenu(description, menuItems);

            return options.ToList().IndexOf(itemSelected);
        }

        private static void itemCallback(string itemClicked)
        {
            itemSelected = itemClicked;
        }

        /// <summary>
        /// Align content to center for console. Can be used with decoration if used inside menu or header
        /// </summary>
        /// <param name="content">Content to center</param>
        /// <param name="decorationString">Left and right decoration, default is empty/none</param>
        /// <returns>Center aligned text</returns>
        public static string CenterText(string content, string decorationString = "")
        {
            int windowWidthSize = Console.WindowWidth;
            int windowWidth = windowWidthSize - (2 * decorationString.Length);
            return String.Format(decorationString + "{0," + ((windowWidth / 2) + (content.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (content.Length / 2) + decorationString.Length) + "}", content, decorationString);
        }

        /// <summary>
        /// Application header, also sets the console title
        /// </summary>
        /// <param name="title">Title of application</param>
        /// <param name="subtitle">Subtitle of application</param>
        /// <param name="foreGroundColor">Foreground color</param>
        public static void Header(string title, string subtitle = "", ConsoleColor foreGroundColor = ConsoleColor.White)
        {
            int windowWidthSize = Console.WindowWidth;
            string titleContent = CenterText(title, "║");
            string subtitleContent = CenterText(subtitle, "║");
            string borderLine = new String('═', windowWidthSize - 2);

            Console.ForegroundColor = foreGroundColor;
            Console.WriteLine($"╔{borderLine}╗");
            Console.WriteLine(titleContent);
            if (!string.IsNullOrEmpty(subtitle))
            {
                Console.WriteLine(subtitleContent);
            }
            Console.WriteLine($"╚{borderLine}╝");
            Console.ResetColor();
        }
    }
}
