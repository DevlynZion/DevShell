using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Common.Utility
{
    //https://stackoverflow.com/questions/46957866/invoke-a-pscmdlet-from-within-a-cmdlet
    public class PsInvoker
    {
        public static PSObject[] InvokeCommand(string commandName, Hashtable parameters)
        {
            var sb = ScriptBlock.Create("param($Command, $Params) & $Command @Params");
            return sb.Invoke(commandName, parameters).ToArray();
        }
  
        public static PsInvoker Create(string cmdletName)
        {
            return new PsInvoker(cmdletName);
        }
  
        private Hashtable Parameters { get; set; }


        public string CmdletName { get; }

        public bool Invoked { get; private set; }

        public PSObject[] Result { get; private set; }


        private PsInvoker(string cmdletName)
        {
            CmdletName = cmdletName;
            Parameters = new Hashtable();
        }


        public void AddArgument(string name, object value)
        {
            Parameters.Add(name, value);
        }

        public void AddArgument(string name)
        {
            Parameters.Add(name, null);
        }

        public PSObject[] Invoke()
        {
            if (Invoked)
                throw new InvalidOperationException("This instance has already been invoked.");
            var sb = ScriptBlock.Create("param($Command, $Params) & $Command @Params");
            Result = sb.Invoke(CmdletName, Parameters).ToArray();
            Invoked = true;
            return Result;
        }

    }
}
