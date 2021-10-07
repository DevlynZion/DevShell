using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.CADEnviroment
{
    [Cmdlet(VerbsCommon.Clear, "CoreModules")]
    public class ClearCoreModules : Cmdlet
    {
        private const string CAFMODULES_FOLDER = @"MineRP\CAFModules";

        [Parameter(Position = 0, Mandatory = false)]
        public string Environment { get; set; } 
        protected override void ProcessRecord() 
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), CAFMODULES_FOLDER);

            var environmentToDelete = "";
            if (string.IsNullOrEmpty(Environment))
            {
                DirectoryInfo environments = new DirectoryInfo(path);
                var environmentNames = environments.GetDirectories().Select(e => e.Name);

                var choice = ConsoleHelper.MultipleChoice(true, environmentNames.ToArray());

                environmentToDelete = environmentNames.ElementAt(choice);
            }
            else
            {
                environmentToDelete = Environment;
            }

            DirectoryInfo di = new DirectoryInfo(Path.Combine(path, environmentToDelete));

            di.Delete(true);            
        }
    }

    public class ConsoleHelper
    {
        public static int MultipleChoice(bool canCancel, params string[] options)
        {
            const int startX = 15;
            const int startY = 8;
            const int optionsPerLine = 3;
            const int spacingPerLine = 14;

            int currentSelection = 0;

            ConsoleKey key;

            Console.CursorVisible = false;

            do
            {
                Console.Clear();

                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(startX + (i % optionsPerLine) * spacingPerLine, startY + i / optionsPerLine);

                    if (i == currentSelection)
                        Console.ForegroundColor = ConsoleColor.Red;

                    Console.Write(options[i]);

                    Console.ResetColor();
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        {
                            if (currentSelection % optionsPerLine > 0)
                                currentSelection--;
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            if (currentSelection % optionsPerLine < optionsPerLine - 1)
                                currentSelection++;
                            break;
                        }
                    case ConsoleKey.UpArrow:
                        {
                            if (currentSelection >= optionsPerLine)
                                currentSelection -= optionsPerLine;
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            if (currentSelection + optionsPerLine < options.Length)
                                currentSelection += optionsPerLine;
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            if (canCancel)
                                return -1;
                            break;
                        }
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return currentSelection;
        }
    }
}
