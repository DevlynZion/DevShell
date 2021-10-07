using DevShell.Common.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.Output
{
    [Cmdlet(VerbsCommunications.Write, "CenterText")]
    public class WriteCenterText : Cmdlet
    {
        [Parameter(Position = 0)]
        public string text { get; set; } = "";
        [Parameter(Position = 1)]
        public string decorationString { get; set; } = "";
        protected override void ProcessRecord()
        {
            WriteObject(ConsoleHelper.CenterText(text, decorationString));
        }
    }
}
