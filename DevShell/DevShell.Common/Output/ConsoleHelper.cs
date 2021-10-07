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
    }
}
