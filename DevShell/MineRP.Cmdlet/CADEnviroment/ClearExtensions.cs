using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace MineRP.Cmdlet.CADEnviroment
{
    [Cmdlet(VerbsCommon.Clear, "Extensions")]
    public class ClearExtensions : System.Management.Automation.Cmdlet
    {
        private const string EXTENSIONS_FOLDER = @"C:\Program Files\MineRP\Explorer 4\Extensions\";

        protected override void ProcessRecord()
        {
            DirectoryInfo di = new DirectoryInfo(EXTENSIONS_FOLDER);

            di.Delete(true);
        }
    }
}

