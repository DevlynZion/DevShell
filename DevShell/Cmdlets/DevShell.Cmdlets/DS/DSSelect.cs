using DevShell.Cmdlets.Common;
using DevShell.Common.ConsoleUI;
using DevShell.Common.Output;
using DevShell.Common.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.DS
{
    [Cmdlet("DS", "Select")]
    public class DSSelect : PSCmdlet
    {
        private Dictionary<string, List<string>> loadedCmdlets;
        private DataTable modulesTable;
        protected override void BeginProcessing()
        {
            loadedCmdlets = new Dictionary<string, List<string>>();

            var dsLoadedModules = SessionState.PSVariable.GetValue(DSVariable.dsLoadedModules) as string[];

            foreach (var module in dsLoadedModules)
            {
                loadedCmdlets.Add(module, new List<string>());

                var getCommandParameters = new Hashtable();
                getCommandParameters.Add("Module", module);
                var commands = PsInvoker.InvokeCommand("Get-Command", getCommandParameters);

                foreach (var command in commands)                
                {
                    var commandName = command.ToString();
                    loadedCmdlets[module].Add(commandName);                    
                }
            }
        }
        protected override void ProcessRecord()
        {
            modulesTable = CreateModuleDataTable();

            HandleModules();
        }

        private void HandleModules()
        {
            ConsoleHelper.Header("Select Assembly");
            var modulesTableSelect = new ConsoleTableSelect(modulesTable);
            var selection = modulesTableSelect.GetSelection();

            if (selection == -1)
                return;

            var moduleName = modulesTable.Rows[selection]["Module"].ToString();
            Console.WriteLine();
            HandleCmdlets(moduleName);
        }

        private void HandleCmdlets(string module)
        {
            ConsoleHelper.Header("Select Cmdlet");
            var cmdletDataTable = CreateCmdletDataTable(module);
            var cmdletTableSelect = new ConsoleTableSelect(cmdletDataTable);
            var selection = cmdletTableSelect.GetSelection();

            if (selection == -1)
                HandleModules();
            else
            {
                var cmdletName = cmdletDataTable.Rows[selection]["Cmdlet"].ToString();

                Console.WriteLine();
                RunCmdlet(cmdletName);
                Console.WriteLine();
                HandleModules();
            }
        }

        private void RunCmdlet(string cmdletName)
        {
            // TODO: Implement 
            // https://devblogs.microsoft.com/scripting/use-the-get-command-powershell-cmdlet-to-find-parameter-set-information/
            // Use to get the right parameter set


            var getProcess = PsInvoker.Create("Get-Command");
            getProcess.AddArgument("Name", cmdletName);
            var parameters = getProcess.Invoke();


            var cmdlet = PsInvoker.Create(cmdletName);
            //cmdlet.AddArgument("Name", DSVariable.dsLoadedModules);
            //cmdlet.AddArgument("Value", dsLoadedModules);
            //cmdlet.AddArgument("Scope", "Global");
            WriteObject(cmdlet.Invoke());
        }

        private DataTable CreateModuleDataTable()
        {
            var moduleDataTable = new DataTable();
            moduleDataTable.Columns.Add("Module");

            foreach (var module in loadedCmdlets)
                NewRow(moduleDataTable, module.Key);

            return moduleDataTable;
        }

        private DataTable CreateCmdletDataTable(string module)
        {
            var cmdletDataTable = new DataTable();
            cmdletDataTable.Columns.Add("Cmdlet");

            foreach (var cmdlet in loadedCmdlets[module])
                NewRow(cmdletDataTable, cmdlet);

            return cmdletDataTable;
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
