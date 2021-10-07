using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevShell.Common.ConsoleUI
{
    public class ProgressMeter
    {
        public double MaxValue { get; set; }
        public double CurrentValue { get; set; }
        public string PreText { get; set; }
        public string PostText { get; set; }

        private int cursorLeft;
        private int cursorTop;


        public void Start()
        {
            cursorLeft = Console.CursorLeft;
            cursorTop = Console.CursorTop;

            DrawUpdate();
        }

        public void UpdateProgress(double progressBy = 1)
        {
            CurrentValue += progressBy;

            DrawUpdate();
        }

        private void DrawUpdate()
        {
            Console.CursorLeft = cursorLeft;
            Console.CursorTop = cursorTop;

            var percentage = Math.Round((CurrentValue * 100) / MaxValue, 0);

            Console.Write(string.Format("{0} [{1} %] {2}", PreText, percentage, PostText));
        }

    }
}
