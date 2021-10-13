using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Common.ConsoleUI
{
    public class ConsoleTableSelect
    {
        public DataTable Data { get; set; }

        private int cursorLeft;
        private int cursorTop;
        private bool itemIsSelcted;
        private int selectedItemIndex;
        private int maxItemShift;
        private int itemsPerPages;
        private int itemStart;

        public ConsoleTableSelect(DataTable data)
        {
            Data = data;
            itemIsSelcted = false;
            selectedItemIndex = 0;
            itemStart = 0;
        }

        public int GetSelection()
        {
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;
            int bottomOffset = 0;
            ConsoleKeyInfo kb;

            itemsPerPages = 20;/*Console.WindowHeight - cursorTop - 3*/;
            maxItemShift = Data.Rows.Count - itemsPerPages; // Not sure what the right Calculation is

            Console.CursorVisible = false;

            while (!itemIsSelcted)
            {
                DrawUpdate();
                bottomOffset = Console.CursorTop;
                kb = Console.ReadKey(true);
                HandleKeyPress(kb.Key);
            }

            Console.CursorVisible = true;
            Console.SetCursorPosition(0, bottomOffset);

            return selectedItemIndex;
        }

        private void PositionCursor()
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop;
        }

        private void DrawUpdate()
        {
            PositionCursor();

            var columns = Data.Columns;
            var columnsLength = new List<int>();

            foreach (DataColumn dataColumn in columns)
            {
                int colLength = dataColumn.ColumnName.Length + 1;

                foreach (DataRow dataRow in Data.Rows)
                {
                    var dataLength = dataRow[dataColumn].ToString().Length + 1;

                    if (dataLength > colLength)
                        colLength = dataLength;
                }

                columnsLength.Add(colLength);
            }

            var columnTotal = columnsLength.Sum();

            if (Console.WindowWidth < columnTotal)
            {
                Console.WriteLine("Can't fit table into console.");
                return;
            }


            var spaceLeft = Console.WindowWidth - columnTotal;
            var spaceToAdd = spaceLeft / columns.Count;

            string headings = "";
            string breakLine = "";
            for (int k = 0; k < columns.Count; k++)
            {
                DataColumn dataColumn = columns[k];
                var maxLength = columnsLength[k];
                var space = new string(' ', maxLength - dataColumn.ColumnName.Length + spaceToAdd);

                headings += string.Format("{0}{1}", dataColumn.ColumnName, space);
                breakLine += new string('-', maxLength - 1) + new string(' ', 1 + spaceToAdd);
            }
            Console.WriteLine(headings);
            Console.WriteLine(breakLine);


            for (int r = itemStart; r < itemsPerPages + itemStart; r++)
            {
                DataRow dataRow = Data.Rows[r];
                var rowLine = "";
                for (int k = 0; k < columns.Count; k++)
                {
                    DataColumn dataColumn = columns[k];
                    var maxLength = columnsLength[k];
                    var value = dataRow[dataColumn].ToString();
                    var endSpace = new string(' ', maxLength - value.Length + spaceToAdd);

                    rowLine += value + endSpace;
                }
                WriteConsoleItem(rowLine, r, selectedItemIndex);
            }
        }

        private void WriteConsoleItem(string item, int itemIndex, int selectedItemIndex)
        {
            if (itemIndex == selectedItemIndex)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(item);
            Console.ResetColor();
        }

        private void HandleKeyPress(ConsoleKey pressedKey)
        {
            switch (pressedKey)
            {
                case ConsoleKey.UpArrow:
                    selectedItemIndex = (selectedItemIndex == 0) ? Data.Rows.Count - 1 : selectedItemIndex - 1;
                    if (selectedItemIndex <= itemStart + itemsPerPages)
                    {
                        itemStart--;

                        if (itemStart == 0)
                        {
                            itemStart = maxItemShift;
                            selectedItemIndex = Data.Rows.Count - 1;
                        }
                    }
                    break;

                case ConsoleKey.DownArrow:
                    selectedItemIndex = (selectedItemIndex == Data.Rows.Count - 1) ? 0 : selectedItemIndex + 1;
                    if (selectedItemIndex >= itemStart + itemsPerPages)
                    {
                        itemStart++;

                        if (itemStart > maxItemShift)
                        {
                            itemStart = 0;
                            selectedItemIndex = 0;
                        }
                    }
                    else if (selectedItemIndex >= itemStart + itemsPerPages) // TODO: fix not right, not wraping to top again
                    {
                        itemStart = 0;
                        selectedItemIndex = 0;
                    }
                    break;

                case ConsoleKey.Enter:
                    itemIsSelcted = true;
                    break;

                case ConsoleKey.Escape:
                    itemIsSelcted = true;
                    selectedItemIndex = -1;
                    break;
            }
        }
    }
}
