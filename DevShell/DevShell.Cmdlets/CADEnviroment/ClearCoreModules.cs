using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.CADEnviroment
{
    [Cmdlet(VerbsCommon.Clear, "CoreModules")]
    public class ClearCoreModules : Cmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public string Environment { get; set; } 
        protected override void ProcessRecord() 
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), @"MineRP\CAFModules");

            DirectoryInfo di = new DirectoryInfo(Path.Combine(path, Environment));

            di.Delete(true);
        }
    }
}
