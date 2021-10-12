using DevShell.Common.ConsoleUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.Test
{
    [Cmdlet(VerbsDiagnostic.Test, "Table")]
    public class TestTable : Cmdlet
    {
        protected override void ProcessRecord()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("string", typeof(string));
            dt.Columns.Add("int", typeof(int));
            dt.Columns.Add("double", typeof(double));
            dt.Columns.Add("float", typeof(float));

            NewRow(dt, "Row 1", 2334, 232332.222, 521156.23f);
            NewRow(dt, "Row 2", 6335, 3.222, 552226.23211f);
            NewRow(dt, "Row 3", 3, 3.2233332, 53356.2223f);
            NewRow(dt, "Row 4", 44556565, 44.23322, 53356.2223f);
            NewRow(dt, "Row 5", 234343434, 232.24422, 51156.23333f);


            ConsoleTable table = new ConsoleTable(dt);

            table.Draw();
        }

        private void NewRow(DataTable dt, string c1, int c2, double c3, float c4)
        {
            var r1 = dt.NewRow();
            r1[0] = c1;
            r1[1] = c2;
            r1[2] = c3;
            r1[3] = c4;
            dt.Rows.Add(r1);
        }
    }    
}
