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
        public string Case { get; set; }
    }

    public class NameNormalizeArgs : ReplaceArgs
    {

    }
    public class MoveArgs : ReplaceArgs
    {
        public string Index { get; set; }
    }
    public class UniqueArgs : ReplaceArgs
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
                PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs("Description"));
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
            var newcase = args.Case;
            var to = args.To;

            return origin.Replace(newcase,to);
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as NewCaseArgs;
            return new NewCaseOperation()
            {
                Args = new NewCaseArgs()
                {
                    Case = oldArgs.Case,
                }
            };
        }

        public override void Config()
        {
            var screen = new NewCaseDialog(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                   new PropertyChangedEventArgs("Description"));
            }
        }
        public override string Name => "New Case";
        public override string Description
        {
            get
            {
                var args = Args as NewCaseArgs;
                return $"Make string {args.Case}";
            }
        }
    }
    public class NameNormalizeOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as NameNormalizeArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as NameNormalizeArgs;
            return new NameNormalizeOperation()
            {
                Args = new NameNormalizeArgs()
            };
        }

        public override void Config()
        {
            var screen = new NameNormalizeDialog(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                 new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Name => "Name Normalize";
        public override string Description
        {
            get
            {
                var args = Args as NameNormalizeArgs;
                return "Name Normalize";
            }
        }
    }
    public class MoveOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as MoveArgs;
            var index1 = args.From;
            var index2 = args.To;
            var index3 = args.Index;

            return origin.Replace(index1, index2);
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as MoveArgs;
            return new MoveOperation()
            {
                Args = new MoveArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To,
                    Index = oldArgs.Index
                }
            };
        }

        public override void Config()
        {
            var screen = new MoveDialog(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Name => "Move";
        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                return $"Move {args.From} character(s) from index {args.To} to the {args.Index}";
            }
        }
    }
    public class UniqueOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as UniqueArgs;
            var from = args.From;
            var to = args.To;

            return origin.Replace(from, to);
        }

        public override StringOperation Clone()
        {
            var oldArgs = Args as UniqueArgs;
            return new UniqueOperation()
            {
                Args = new UniqueArgs()
                {
                    From = oldArgs.From,
                    To = oldArgs.To
                }
            };
        }

        public override void Config()
        {
            var screen = new UniqueNameDialog(Args);
            if (screen.ShowDialog() == true)
            {
                PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs("Description"));
            }
        }

        public override string Name => "Unique Name";
        public override string Description
        {
            get
            {
                var args = Args as UniqueArgs;
                return $"Make name unique";
            }
        }
    }






}
