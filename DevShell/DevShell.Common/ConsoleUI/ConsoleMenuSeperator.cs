using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://github.com/arthurvaverko/ConsoleMenu
/// </summary>
namespace DevShell.Common.ConsoleUI
{
    public class ConsoleMenuSeperator : ConsoleMenuItem
    {
        public ConsoleMenuSeperator(Char seperatorChar = '-')
            : base(seperatorChar.ToString())
        {
        }
    }
}
