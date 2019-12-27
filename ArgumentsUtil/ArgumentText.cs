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

            for (int i = 0; i < Body.Length; i++)
            {
                if (!string.IsNullOrEmpty(Title)) Console.Write("  ");
                Console.WriteLine(Body[i]);
            }
        }
    }
}
