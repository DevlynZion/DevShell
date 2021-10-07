using DevShell.Common.ConsoleUI;
using DevShell.Common.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FooConsole = System.Console;

namespace DevShell.Common.Extend
{
    public static class Console
    {
        public static void ListMenu(string description, IEnumerable<ConsoleMenuItem> menuItems)
        {
            ConsoleHelper.ListMenu(description, menuItems);
        }

        public static int ListMenuParams(string description, params string[] options)
        {
            return ConsoleHelper.ListMenuParams(description, options);
        }
    }
}
