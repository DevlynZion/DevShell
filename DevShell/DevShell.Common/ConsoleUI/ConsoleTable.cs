using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Common.ConsoleUI
{
    public class ConsoleTable
    {
        public DataTable Data { get; set; }

        public ConsoleTable(DataTable data)
        {
            Data = data;
        }

        public void Draw()
        {
            var columns = Data.Columns;
            var columnsLength = new List<int>();

            foreach(DataColumn dataColumn in columns)
            {
                int colLength = dataColumn.ColumnName.Length + 1;

                foreach(DataRow dataRow in Data.Rows)
                {
                    var dataLength = dataRow[dataColumn].ToString().Length + 1;

                    if(dataLength > colLength)
                        colLength = dataLength;
                }

                columnsLength.Add(colLength);
            }

            if (Console.WindowWidth < columnsLength.Sum())
            {
                Console.WriteLine("Can't fit table into console.");
                return;
            }


            string headings = "";
            string breakLine = "";
            for(int k = 0; k < columns.Count;  k++)
            {
                DataColumn dataColumn = columns[k];
                var maxLength = columnsLength[k];
                var space = new string(' ', maxLength - dataColumn.ColumnName.Length);

                headings += string.Format("{0}{1}", dataColumn.ColumnName, space);
                breakLine += new string('-', maxLength - 1) + " ";
            }
            Console.WriteLine(headings);
            Console.WriteLine(breakLine);

            foreach(DataRow dataRow in Data.Rows)
            {
                var rowLine = "";
                for (int k = 0; k < columns.Count; k++)
                {
                    DataColumn dataColumn = columns[k];
                    var maxLength = columnsLength[k];
                    var value = dataRow[dataColumn].ToString();
                    var endSpace = new string(' ', maxLength - value.Length);

                    rowLine += value + endSpace;
                }
                Console.WriteLine(rowLine);
            }
        }
    }
}
