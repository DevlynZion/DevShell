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
    public class ConsoleMenu<T>
    {
        private ConsoleMenuItem[] _menuItems;
        readonly string _Description;
        private int _SelectedItemIndex = 0;
        private bool _ItemIsSelcted = false;

        public ConsoleMenu(string description, IEnumerable<ConsoleMenuItem> menuItems)
        {
            _menuItems = menuItems.ToArray();
            _Description = description;
        }

        public void RunConsoleMenu()
        {
            if (!string.IsNullOrEmpty(_Description))
            {
                Console.WriteLine($"{_Description}: {Environment.NewLine}");
            }

            StartConsoleDrawindLoopUntilInputIsMade();


            _ItemIsSelcted = false;
            _menuItems[_SelectedItemIndex].Execute();
        }

        private void StartConsoleDrawindLoopUntilInputIsMade()
        {
            int topOffset = Console.CursorTop;
            int bottomOffset = 0;
            ConsoleKeyInfo kb;
            Console.CursorVisible = false;


            while (!_ItemIsSelcted)
            {
                for (int i = 0; i < _menuItems.Length; i++)
                {
                    WriteConsoleItem(i, _SelectedItemIndex);
                }

                bottomOffset = Console.CursorTop;
                kb = Console.ReadKey(true);
                HandleKeyPress(kb.Key);

                //Reset the cursor to the top of the screen
                Console.SetCursorPosition(0, topOffset);
            }

            //set the cursor just after the menu so that the program can continue after the menu
            Console.SetCursorPosition(0, bottomOffset);
            Console.CursorVisible = true;
        }

        private void HandleKeyPress(ConsoleKey pressedKey)
        {
            switch (pressedKey)
            {
                case ConsoleKey.UpArrow:
                    _SelectedItemIndex = (_SelectedItemIndex == 0) ? _menuItems.Length - 1 : _SelectedItemIndex - 1;
                    CheckForUnselectable(pressedKey);
                    break;

                case ConsoleKey.DownArrow:
                    _SelectedItemIndex = (_SelectedItemIndex == _menuItems.Length - 1) ? 0 : _SelectedItemIndex + 1;
                    CheckForUnselectable(pressedKey);
                    break;

                case ConsoleKey.Enter:
                    _ItemIsSelcted = true;
                    break;
            }
        }
        private void CheckForUnselectable(ConsoleKey pressedKey)
        {
            if (_menuItems[_SelectedItemIndex].GetType() == typeof(ConsoleMenuSeperator))
            {
                HandleKeyPress(pressedKey);
            }
        }

        private void WriteConsoleItem(int itemIndex, int selectedItemIndex)
        {
            if (itemIndex == selectedItemIndex)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            string text = this._menuItems[itemIndex].Name;
            if (_menuItems[itemIndex].GetType() == typeof(ConsoleMenuSeperator))
            {
                text = text.PadRight(_menuItems.Max(x => x.Name.Length), _menuItems[itemIndex].Name[0]);
            }
            Console.WriteLine(" {0,-20}", text);
            Console.ResetColor();
        }
    }
}
