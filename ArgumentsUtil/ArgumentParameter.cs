using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsUtil
{
    public class ArgumentParameter
    {
        public ArgumentParameter(string name, Type expectedType, string description = null, bool isOptional = false)
        {
            Name = name;
            ExpectedType = expectedType;
            Description = description;
            IsOptional = isOptional;
        }

        public string Name { get; }
        public Type ExpectedType { get; }
        public string Description { get; }
        public bool IsOptional { get; }

        public override string ToString()
        {
            return IsOptional ? $"({Name})" : $"[{Name}]";
        }
    }
}
