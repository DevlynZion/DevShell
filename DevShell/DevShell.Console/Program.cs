using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessStartInfo ps = new ProcessStartInfo(@"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe", @"-NoExit -ExecutionPolicy Bypass .\Script\Start.ps1");

            Process p = new Process();
            p.StartInfo = ps;
            p.Start();
        }
    }
}
