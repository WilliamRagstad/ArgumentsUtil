using System;
using System.Collections.Generic;

namespace ArgumentsUtil
{
    public class ArgumentOption
    {

        public ArgumentOption(string ShortName, string Description) : this(ShortName, null, Description) { }
        public ArgumentOption(string ShortName, string LongName, string Description)
        {
            this.ShortName = ShortName;
            this.LongName = LongName;
            this.Description = Description;
        }

        public string ShortName { get; }
        public string LongName { get; }
        public string Description { get; }

        public string GetHelpKeyNames(char Selector)
        {
            string keys = "";
            if (!string.IsNullOrEmpty(ShortName)) keys += Selector + ShortName;
            if (!string.IsNullOrEmpty(LongName) && !string.IsNullOrEmpty(ShortName)) keys += '|';
            if (!string.IsNullOrEmpty(LongName)) keys += Selector + LongName;
            return keys;
        }
    }
}
