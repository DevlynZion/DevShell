using DevShell.Common.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.Output
{
    [Cmdlet(VerbsCommunications.Write, "Header")]
    public class WriteHeader : Cmdlet
    {
        [Parameter(Position = 0)]
        public string Text { get; set; } = "";
        [Parameter(Position = 1)]
        public string Subtitle { get; set; } = "";
        [Parameter(Position = 2)]
        public string ForeGroundColor { get; set; } = "White";

        protected override void ProcessRecord()
        {
            var foreGroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), ForeGroundColor);

            ConsoleHelper.Header(Text, Subtitle, foreGroundColor);
        }
    }
}
