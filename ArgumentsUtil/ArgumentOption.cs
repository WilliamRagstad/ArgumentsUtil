using System.Collections.Generic;

namespace ArgumentsUtil
{
    public class ArgumentOption
    {
        public ArgumentOption(string ShortName, string Description) : this(ShortName, null, Description, null) { }
        public ArgumentOption(string ShortName, string Description, List<ArgumentParameter> Params = null) : this(ShortName, null, Description, Params) { }
        public ArgumentOption(string ShortName, string LongName, string Description, List<ArgumentParameter> Params = null)
        {
            this.ShortName = ShortName;
            this.LongName = LongName;
            this.Description = Description;
            this.Params = Params;
        }

        public string ShortName { get; }
        public string LongName { get; }
        public string Description { get; }
        public List<ArgumentParameter> Params { get; }

        public string GetManualUsage(char Selector)
        {
            string usage = "";
            if (!string.IsNullOrEmpty(ShortName)) usage += Selector + ShortName;
            if (!string.IsNullOrEmpty(LongName) && !string.IsNullOrEmpty(ShortName)) usage += '|';
            if (!string.IsNullOrEmpty(LongName)) usage += Selector + LongName;


            if (Params != null)
            {
                for (int j = 0; j < Params.Count; j++) usage += $" {Params[j]}";
            }

            return usage;
        }
    }
}
