using DevShell.Cmdlets.Common;
using DevShell.Common.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.DS
{
    [Cmdlet("DS", "Select")]
    public class DSSelect : PSCmdlet
    {
        private List<string> loadedCmdlets;
        protected override void BeginProcessing()
        {
            loadedCmdlets = new List<string>();

            var dsLoadedModules = SessionState.PSVariable.GetValue(DSVariable.dsLoadedModules); // TODO: Get this to work

            var dsLoadedModules2 = dsLoadedModules;

            var dsLoadedModules3 = dsLoadedModules as string[];

            foreach (var module in dsLoadedModules3)
            {
                var getCommandParameters = new Hashtable();
                getCommandParameters.Add("Module", module);
                var commands = PsInvoker.InvokeCommand("Get-Command", getCommandParameters);
                loadedCmdlets.Add(commands.ToString());
            }
        }
        protected override void ProcessRecord()
        {

        }
    }
}
