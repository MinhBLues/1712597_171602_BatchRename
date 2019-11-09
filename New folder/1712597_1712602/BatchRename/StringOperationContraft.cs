using BatchRename.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchRename
{
    public class StringArgs
    {
    }

    public class ReplaceArgs : StringArgs, INotifyPropertyChanged
    {
        public string From { get; set; }
        public string To { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class NewCaseArgs : ReplaceArgs
    {
       
    }

    public abstract class StringOperation : INotifyPropertyChanged
    {
        public StringArgs Args { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract string Operate(string origin);

        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract StringOperation Clone();

        public abstract void Config();
    }

    public class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as ReplaceArgs;
            return new ReplaceOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To
                }
            };
        }

        public override void Config()
        {
            var screen = new ReplaceDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
        }

        public override string Name => "Replace";
        public override string Description
        {
            get
            {
                var args = Args as ReplaceArgs;
                return $"Replace from {args.From} to {args.To}";
            }
        }
    }
    public class NewCaseOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as NewCaseArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as NewCaseArgs;
            return new NewCaseOperation()
            {
                Args = new ReplaceArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To
                }
            };
        }

        public override void Config()
        {
            var screen = new NewCaseDialog(Args);
            if (screen.ShowDialog() == true)
            {

            }
        }

        public override string Name => "New Case";
        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                return $"Replace from {args.From} to {args.To}";
            }
        }
    }
}
