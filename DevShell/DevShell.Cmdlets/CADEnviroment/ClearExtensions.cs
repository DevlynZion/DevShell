using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.CADEnviroment
{
    [Cmdlet(VerbsCommon.Clear, "Extensions")]
    public class ClearExtensions : Cmdlet
    {
        private const string EXTENSIONS_FOLDER = @"C:\Program Files\MineRP\Explorer 4\Extensions\";

        protected override void ProcessRecord()
        {
            DirectoryInfo di = new DirectoryInfo(EXTENSIONS_FOLDER);

            di.Delete(true);
        }
    }
}
