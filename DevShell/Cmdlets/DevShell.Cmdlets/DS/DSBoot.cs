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
        private const string CMDLETS_FOLDER = @".\";
        private const string SCRIPTS_FOLDERNAME = @"Script";
        private const string MODULE_FOLDERNAME = @"Module";
        private readonly List<string> SCRIPTS_TO_LOAD = new List<string>() { "EnvironmentVariables.ps1",
                                                                             "Alias.ps1",
                                                                             "SetPrompt.ps1"};


        protected override void ProcessRecord()
        {
            GetVersion();
            LoadModules();
            LoadScriptModules();
            LoadScripts();            
        }

        private void GetVersion()
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            ConsoleHelper.Header(APPLICATION_NAME, string.Format("version: {0}", version));
            
            SessionState.PSVariable.Set("dsVersion", version);

        }

        private void LoadModules()
        {
            DirectoryInfo cmdLetsFolder = new DirectoryInfo(CMDLETS_FOLDER);
            var cmdLets = cmdLetsFolder.GetFiles("*.Cmdlets.dll");

            var dsLoadedModules = cmdLets.Select(c => c.Name.Substring(0, c.Name.LastIndexOf('.'))).ToArray();
            var loadedModulesFullName = cmdLets.Select(c => c.FullName).ToArray();

            var importModuleParameters = new Hashtable();
            importModuleParameters.Add("Name", loadedModulesFullName);
            importModuleParameters.Add("Force", true);
            importModuleParameters.Add("DisableNameChecking", true);
            PsInvoker.InvokeCommand("Import-Module", importModuleParameters);

            foreach (var module in dsLoadedModules)
            {
                var getCommandParameters = new Hashtable();
                getCommandParameters.Add("Module", module);
                var commands = PsInvoker.InvokeCommand("Get-Command", getCommandParameters);
                WriteObject(commands);
            }

            SessionState.PSVariable.Set("dsLoadedModules", dsLoadedModules);
        }

        private void LoadScripts()
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            foreach (var script in SCRIPTS_TO_LOAD)
            {
                var parameters = new Hashtable();
                WriteObject(PsInvoker.InvokeCommand(Path.Combine(workingDirectory, SCRIPTS_FOLDERNAME, script), parameters));
            }
        }

        private void LoadScriptModules()
        {
            var workingDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo moduleFolder = new DirectoryInfo(Path.Combine(workingDirectory, MODULE_FOLDERNAME));

            foreach (var scriptModule in moduleFolder.GetFiles("*.psm1"))
            {
                var importModule = PsInvoker.Create("Import-Module");
                importModule.AddArgument("Name", scriptModule.FullName);
                importModule.AddArgument("Force", true);
                importModule.AddArgument("DisableNameChecking", true);
                WriteObject(importModule.Invoke());
            }
        }
    }
}
