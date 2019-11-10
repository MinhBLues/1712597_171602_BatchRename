using BatchRename.Dialog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
        public string Area { get; set; }

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

        public abstract string ActionProcess(string newFileName, bool isFileNAme);
    }

    public class ReplaceOperation : StringOperation, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public override string Operate(string origin)
        {
            var args = Args as ReplaceArgs;
            var from = args.From;
            var to = args.To;
            var index = args.Area;

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

        public override string ActionProcess(string inputString, bool isFilename)
        {
            var args = Args as ReplaceArgs;
            string from = args.From;
            string to = args.To;
            string index = args.Area;
            //string area = this.Area;

            // split name and extension
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }
            if (index == "Extension")
            {
                extension = extension.Replace(from, to);
            }
            else
            {
                name = name.Replace(from, to); 
            }
            // conbine and return
            string result = name + extension;
            return result;
        }
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

        static public string MakeStringLower(string inputString)
        {
            return inputString.ToLower();
        }

        static public string MakeStringUpper(string inputString)
        {
            return inputString.ToUpper();
        }

        static public string MakeStringCapitalized(string inputString)
        {
            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower());
            return result;
        }
        public delegate string TypeStringProcess(string inputString);

        private TypeStringProcess[] makeStringCase = new TypeStringProcess[]
        {
                MakeStringLower,
                MakeStringUpper,
                MakeStringCapitalized,

        };
        public override string ActionProcess(string inputString, bool isFilename)
        {
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }
            int caseIndex = -1;
            var args = Args as NewCaseArgs;
            if (args.Case == "LowerCase")
            {
                caseIndex = 0;
            }
            else
                if (args.Case == "UpperCase") caseIndex = 1;
            else
                if(args.Case == "CapitalizedCase") caseIndex = 2;
            name = makeStringCase[caseIndex](name);
            string result = name + extension;
            return result;
        }
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
        static public string MakeStringCapitalized(string inputString)
        {
            string result = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(inputString.ToLower());
            return result;
        }
        static public string[] SplitString(string inputString, string[] seperators)
        {
            string[] result = inputString.Split(seperators, StringSplitOptions.RemoveEmptyEntries);
            return result;
        }
        static public string MakeStringNormalized(string inputString)
        {
            string result = "";
            // capitalize
            string capitaliedString = MakeStringCapitalized(inputString);
            // split to tokens
            string[] tokens = SplitString(capitaliedString, new string[] { " " });

            // combine all token, one space between each two tokens
            for (int i = 0; i < tokens.Length; i++)
            {
                if (i == tokens.Length - 1)
                    result += tokens[i];
                else
                    result += tokens[i] + " ";
            }

            return result;
        }
        public override string ActionProcess(string inputString, bool isFilename)
        {
            var args = Args as NameNormalizeArgs;

            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            // process
            name = MakeStringNormalized(name);

            // combine and return
            string result = name + extension;
            return result;
        }
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

        static public string MoveString(string inputString, int startIndex, int length, int indexMoveTo)
        {
            inputString = inputString.TrimStart(' ');
            inputString = inputString.TrimEnd(' ');

            // find first-number-character string
            string part = inputString.Substring(startIndex, length);
            part = " " + part;
            string body = inputString.Substring(length);


            // move to destination
            string result = body.Insert(indexMoveTo, part);
            result = result.TrimStart(' ');
            result = result.TrimEnd(' ');
            return result;
        }

        public override string ActionProcess(string inputString, bool isFilename)
        {
            var args = Args as MoveArgs;

            int startAt = Int32.Parse(args.From);
            int length = Int32.Parse(args.To);
            string i = args.Index;
            int destination = -1;

            if (i == "Begin") destination = 0;
            if (i == "End") destination = 1;

            // split
            string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            // process
            int indexMoveTo = (destination == Begin) ? 0 : name.Length - length;
            name = MoveString(name, startAt, length, indexMoveTo);

            // combine and return
            string result = name + extension;
            return result;
        }
        public override string Description
        {
            get
            {
                var args = Args as MoveArgs;
                return $"Move {args.From} character(s) from index {args.To} to the {args.Index}";
            }
        }
        public static int ISBNLength => 13;
        public static int Begin => 0;

        public static int End => 1;
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

        static public string TransformString(string inputString)
        {
            // generate a GUID
            Guid g = Guid.NewGuid();
            string result = g.ToString();

            return result;
        }
        public override string ActionProcess(string inputString, bool isFilename)
        {
            var args = Args as UniqueArgs;
            string from = args.From;
            string to = args.To;
            //string area = this.Area;

             string name = inputString;
            string extension = "";
            if (isFilename)
            {
                name = Path.GetFileNameWithoutExtension(inputString);
                extension = inputString.Remove(0, name.Length);
            }

            // process
            name = TransformString(name);

            // combine and return
            string result = name + extension;
            return result;
        }
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
