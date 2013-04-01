using System;

namespace Log4Net.Winforms.Service
{
    public class CommandArgument
    {
        private string name = string.Empty;

        public CommandArgument() { }

        /// <summary>
        /// parse a command argument if in the form /name:value then split out
        /// if just /name then just capture the name
        /// otherwise just capture the value
        /// </summary>
        /// <param name="argument"></param>
        public CommandArgument(string argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException("argument");
            }

            if (argument.StartsWith("/", StringComparison.Ordinal))
            {
                int position = argument.IndexOf(':');
                if (position > 0)
                {
                    name = argument.Substring(1, position - 1);
                    Value = argument.Substring(position + 1);
                }
                else
                {
                    name = argument.Substring(1);
                }
            }
            else
            {
                Value = argument;
                Name = argument;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Value { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(name))
            {
                return "/" + name + ":" + Value;
            }

            return Value;
        }
    }
}