using DevShell.Common.Output;
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
        private const string CAFMODULES_FOLDER = @"MineRP\CAFModules";

        [Parameter(Position = 0, Mandatory = false)]
        public string Environment { get; set; } 
        protected override void ProcessRecord() 
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData), CAFMODULES_FOLDER);

            var environmentToDelete = "";
            if (string.IsNullOrEmpty(Environment))
            {
                DirectoryInfo environments = new DirectoryInfo(path);
                var environmentNames = environments.GetDirectories().Select(e => e.Name).ToList();

                var choice = ConsoleHelper.ListMenuParams("Select Enviroment", environmentNames.ToArray());                

                environmentToDelete = environmentNames[choice];
            }
            else
            {
                environmentToDelete = Environment;
            }

            DirectoryInfo di = new DirectoryInfo(Path.Combine(path, environmentToDelete));

            di.Delete(true);            
        }
    }    
}
