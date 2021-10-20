using DevShell.Common.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.PowerShell.Commands;
using System.IO;
using DevShell.Common.Utility;
using System.Collections;

namespace DevShell.Cmdlets.DS
{
    [Cmdlet("DS", "Boot")]
    public class DSBoot : PSCmdlet
    {
        private const string APPLICATION_NAME = "Dev Shell";
        private const string DEVSHELL_CMDLETS_DLL_NAME = @"DevShell.Cmdlets.dll";
        private const string CMDLETS_FOLDER = @".\";


        protected override void ProcessRecord()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            ConsoleHelper.Header(APPLICATION_NAME, string.Format("version: {0}", version));

            DirectoryInfo cmdLetsFolder = new DirectoryInfo(CMDLETS_FOLDER);
            var cmdLets = cmdLetsFolder.GetFiles("*.Cmdlets.dll");

            var dsLoadedModules = cmdLets.Select(c => c.Name).ToArray();


            //Import-Module .\DevShell.Cmdlets.dll -Force -DisableNameChecking 
            //Get-Command -module DevShell.Cmdlets
            var importModuleParameters = new Hashtable();
            importModuleParameters.Add("Name", dsLoadedModules);
            importModuleParameters.Add("Force", true);
            importModuleParameters.Add("DisableNameChecking", true);
            PsInvoker.InvokeCommand("Import-Module", importModuleParameters);

            SessionState.PSVariable.Set("dsLoadedModules", dsLoadedModules);

            var getCommandParameters = new Hashtable();
            getCommandParameters.Add("Module", dsLoadedModules);
            var  a = PsInvoker.InvokeCommand("Get-Command", getCommandParameters);


            //foreach (PSObject module in getModule.)
            //{

            //}


        }
    }
}
