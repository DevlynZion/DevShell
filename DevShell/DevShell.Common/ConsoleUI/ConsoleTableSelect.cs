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
        public DataTable Data 
        { 
            get 
            { 
                return data; 
            }  
            set 
            { 
                data = value;
                CalculateColumnLength();
            } 
        }
        public int NumberOfRows { get; set; }
        public bool SelectWrapList { get; set; }


        private DataTable data;
        private int cursorLeft;
        private int cursorTop;
        private bool itemIsSelcted;
        private int selectedItemIndex;
        private int maxItemShift;
        private int itemsPerPages;
        private int itemStart;
        private List<int> columnsLength;
        private int columnTotal;

        public ConsoleTableSelect(DataTable data)
        {
            itemIsSelcted = false;
            selectedItemIndex = 0;
            itemStart = 0;
            columnsLength = new List<int>();
            columnTotal = 0;

            NumberOfRows = 20;

            Data = data;
        }

        public int GetSelection()
        {
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;
            int bottomOffset = 0;
            ConsoleKeyInfo kb;

            itemsPerPages = data.Rows.Count < NumberOfRows ? data.Rows.Count : NumberOfRows;
            maxItemShift = Data.Rows.Count - itemsPerPages; // Not sure what the right Calculation is, but seems to be working

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

        private void CalculateColumnLength()
        {
            var columns = Data.Columns;
            columnsLength = new List<int>();

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

            columnTotal = columnsLength.Sum();
        }

        private void DrawUpdate()
        {
            PositionCursor();

            var columns = Data.Columns;

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

                    if (selectedItemIndex == 0)
                    {
                        selectedItemIndex = Data.Rows.Count - 1;
                        itemStart = maxItemShift;
                    }
                    else
                    {
                        selectedItemIndex--;                        

                        if (selectedItemIndex < itemStart)
                            itemStart--;
                    }
                    break;

                case ConsoleKey.DownArrow:

                    if (selectedItemIndex == Data.Rows.Count - 1)
                    {
                        selectedItemIndex = 0;
                        itemStart = 0;
                    }
                    else
                    {
                        selectedItemIndex++;

                        if(selectedItemIndex == itemStart + itemsPerPages)
                            itemStart++;

                        if (itemStart > maxItemShift)
                            itemStart = maxItemShift;
                    }
                    break;

                case ConsoleKey.Home:
                    selectedItemIndex = 0;
                    itemStart = 0;
                    break;

                case ConsoleKey.End:
                    selectedItemIndex = Data.Rows.Count - 1;
                    itemStart = maxItemShift;
                    break;

                case ConsoleKey.PageDown:
                    // TODO: Implement Page Down
                    break;

                case ConsoleKey.PageUp:
                    // TODO: Implement Page Up
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
