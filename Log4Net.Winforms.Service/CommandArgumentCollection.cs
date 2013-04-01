using System;
using System.Collections.Generic;
using System.Linq;

namespace Log4Net.Winforms.Service
{
    public class CommandArgumentCollection : List<CommandArgument>
    {

        public CommandArgumentCollection()
        {

        }
        public CommandArgumentCollection(string[] args)
        {
            Load(args);
        }
        public void Load(string[] args)
        {
            if (args == null || args.Length <= 0) return;

            foreach (string item in args)
            {
                Add(new CommandArgument(item));
            }
        }

        public CommandArgument GetFirstArgument(string name)
        {
            return this.FirstOrDefault(item => item.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}