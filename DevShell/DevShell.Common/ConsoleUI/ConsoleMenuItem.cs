using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// https://github.com/arthurvaverko/ConsoleMenu
/// </summary>
namespace DevShell.Common.ConsoleUI
{
    public abstract class ConsoleMenuItem
    {
        public ConsoleMenuItem(string label)
        {
            Name = label;
        }
        public string Name { get; set; }


        public virtual void Execute() { }
    }

    public class ConsoleMenuItem<T> : ConsoleMenuItem
    {
        public Action<T> CallBack { get; set; }
        public T UnderlyingObject { get; set; }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ UnderlyingObject.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            // Check for null values and compare run - time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            var item = (ConsoleMenuItem<T>)obj;
            return item.GetHashCode() == this.GetHashCode();
        }
        public override string ToString()
        {
            return $"{Name} (data: {UnderlyingObject.ToString()})";
        }

        public ConsoleMenuItem(string label, Action<T> callback, T underlyingObject)
            : base(label)
        {
            CallBack = callback;
            UnderlyingObject = underlyingObject;
        }

        public override void Execute()
        {
            CallBack(UnderlyingObject);
        }
    }
}
