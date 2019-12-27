using System.Collections.Generic;

namespace ArgumentsUtil
{
    public class ArgumentCommand
    {
        public ArgumentCommand(string Name, string Description, List<ArgumentParameter> Params = null)
        {
            this.Name = Name;
            this.Description = Description;
            this.Params = Params;
        }
        
        public string Name { get; }
        public string Description { get; }
        public List<ArgumentParameter> Params { get; }
    }
}
