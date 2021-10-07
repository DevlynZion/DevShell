using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Cmdlets.DevEnviroment
{
    [Cmdlet(VerbsCommon.Open, "VPN")]
    public class OpenVPN : Cmdlet
    {
        private const string VPN_CLIENT = @"C:\Program Files (x86)\Sophos\Sophos SSL VPN Client\bin\openvpn-gui.exe";
        private const string VPN_CONFIG = @"C:\Program Files (x86)\Sophos\Sophos SSL VPN Client\config";
        private const string VPN_CONFIG_FILE = @"dvanderwalt@minerp.com_ssl_vpn_config.ovpn";

        protected override void ProcessRecord()
        {
            // https://www.avanet.com/en/kb/add-sophos-ssl-vpn-client-to-startup-with-automatic-login/
            ProcessStartInfo info = new ProcessStartInfo(VPN_CLIENT);
            info.Arguments = "--config_dir \"" + VPN_CONFIG + "\" --connect \"" + VPN_CONFIG_FILE + "\"";
            info.UseShellExecute = true;
            info.Verb = "runas";
            Process.Start(info);
        }
    }
}
