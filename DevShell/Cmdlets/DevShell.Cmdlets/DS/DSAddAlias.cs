using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using DevShell.Common.Utility;
using Microsoft.PowerShell.Commands;

namespace DevShell.Cmdlets.DS
{
    [Cmdlet("DS", "AddAlias")]
    [OutputType(typeof(DSAddAliasOutput))]
    public class DSAddAlias : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Name;
        [Parameter(Position = 1, Mandatory = true)]
        public object Value;

        protected override void ProcessRecord()
        {
            var setAlias = PsInvoker.Create("Set-Alias");
            setAlias.AddArgument("Name", Name);
            setAlias.AddArgument("Value", Value);
            setAlias.AddArgument("Scope", "Global");
            setAlias.Invoke();

            //WriteObject(new DSAddAliasOutput() { Name = Name , Value = Value}); TODO: Fix so it displays better

            WriteObject(string.Format("{0} - {1}", Name, Value));
        }
    }

    public class DSAddAliasOutput
    {
        public string Name;
        public object Value;
    }
}
