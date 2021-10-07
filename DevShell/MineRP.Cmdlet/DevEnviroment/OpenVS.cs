using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace MineRP.Cmdlet.DevEnviroment
{
    [Cmdlet(VerbsCommon.Open, "VS")]
    public class OpenVS : System.Management.Automation.Cmdlet
    {
        private const string VISUAL_STUDIO = @"C:\Program Files (x86)\Microsoft Visual Studio\2019\Professional\Common7\IDE\devenv.exe";

        protected override void ProcessRecord()
        {
            ProcessStartInfo info = new ProcessStartInfo(VISUAL_STUDIO);
            info.UseShellExecute = true;
            info.Verb = "runas";
            Process.Start(info);
        }
    }
}

