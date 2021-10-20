using DevShell.Common.ConsoleUI;
using DevShell.Common.Output;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.Select
{
    [Cmdlet(VerbsCommon.Select, "Process")]
    public class SelectProcess : Cmdlet
    {
        private DataTable processesData;

        public SelectProcess()
        {
            processesData = new DataTable();
        }


        protected override void BeginProcessing()
        {
            //processesData.Columns.Add("#");
            processesData.Columns.Add("Image Name");
            processesData.Columns.Add("PID");
            processesData.Columns.Add("Session#");
            processesData.Columns.Add("Mem Usage");

            var processes = Process.GetProcesses().ToList();            
            int count = 0;
            foreach (var proces in processes)
            {
                NewRow(processesData, /*count,*/ proces.ProcessName, proces.Id, proces.SessionId, ConsoleHelper.SizeSuffix(proces.WorkingSet64));
                count++;
            }

            processesData.DefaultView.Sort = "Image Name ASC";
            processesData = processesData.DefaultView.ToTable();
        }

        protected override void ProcessRecord()
        {
            ConsoleTableSelect consoleTableSelect = new ConsoleTableSelect(processesData);
            ConsoleHelper.Header("Select Process to Kill");


            var index = consoleTableSelect.GetSelection();
            if (index != -1)
            {
                var pid = Convert.ToInt32(processesData.Rows[index]["PID"]);

                var procesToKill = Process.GetProcesses().FirstOrDefault(p => p.Id == pid);

                if (procesToKill != null)
                    procesToKill.Kill();
            }
        }

        private void NewRow(DataTable dt, params object[] cell)
        {
            var r1 = dt.NewRow();
            for (int k = 0; k < dt.Columns.Count; k++)
                r1[k] = cell[k];
            dt.Rows.Add(r1);
        }
    }
}
