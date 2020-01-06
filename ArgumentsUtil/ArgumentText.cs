using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentsUtil
{
    public class ArgumentText
    {
        public ArgumentText(string title, string[] body)
        {
            Title = title;
            Body = body;
        }

        public string Title { get; }
        public string[] Body { get; }

        public void Show(HelpFormatting formatting = HelpFormatting.Default)
        {
            if (!string.IsNullOrEmpty(Title))
            {
                Console.WriteLine(Title);
                if (formatting.HasFlag(HelpFormatting.TitleUnderlines))
                {
                    for (int i = 0; i < Title.Length; i++)
                    {
                        Console.Write('¨');
                    }
                    Console.Write(Environment.NewLine);
                }
            }

            bool indent = !string.IsNullOrEmpty(Title);
            string indents = "  ";

            for (int i = 0; i < Body.Length; i++)
            {
                if (indent) Console.Write(indents);
                string[] words = Body[i].Split(' ');
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j].Length > Console.BufferWidth - Console.CursorLeft)
                    {
                        Console.Write(Environment.NewLine);
                        Console.Write(indents);
                    }

                    Console.Write(words[j]);
                    if (j != words.Length - 1) Console.Write(' ');
                }
                if (i != Body.Length - 1) Console.Write(Environment.NewLine + Environment.NewLine);
            }
        }
    }
}
