using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArgumentsUtil;

namespace ConsoleAppTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            Arguments a = Arguments.Parse(args);

            ArgumentsTemplate at = new ArgumentsTemplate(
                new List<ArgumentOption>
                {
                    new ArgumentOption("v", "verbose", "Toggle verbose mode.", new List<ArgumentParameter> {
                        new ArgumentParameter("mode", typeof(bool), "", true)
                    }),
                    new ArgumentOption("e", null, "Use encoding mode to encode a file.", new List<ArgumentParameter> {
                        new ArgumentParameter("lsb", typeof(int))
                    }),
                    new ArgumentOption("e", null, "Use extended encoding with specified filepath and output.", new List<ArgumentParameter> {
                        new ArgumentParameter("file", typeof(string)),
                        new ArgumentParameter("out", typeof(string)),
                        new ArgumentParameter("debug", typeof(bool), "", true)
                    })
                },
                false,
                new List<ArgumentCommand> {
                    new ArgumentCommand("encode", "Select mode to encoding. This will encode a message inside the selected file.")
                },
                true,
                new List<ArgumentText> {
                    new ArgumentText("Custom title:", new[] {"This text is completely customizable! And you can write anything you want here, isn't that amazing!!!"})
                },
                "My Custom Console App",
                (char)KeySelector.CrossPlatformCompatible
            );

            at.ShowManual(HelpFormatting.None);

            Console.Read();
        }
    }
}
