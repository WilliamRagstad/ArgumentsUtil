using System;
using System.Collections.Generic;
using System.Reflection;

namespace ArgumentsUtil
{
    public class Help
    {
        private readonly List<ArgumentOption> options;
        private readonly List<ArgumentCommand> commands;
        private readonly string appName;
        private readonly string assemblyName;
        private readonly Version assemblyVersion;
        private readonly char keyChar;

        public Help(List<ArgumentOption> Options = null, List<ArgumentCommand> Commands = null, string AppName = null, char keySelector = (char)KeySelector.CrossPlatformCompatible)
        {
            options = Options;
            commands = Commands;
            appName = AppName ?? "App";
            assemblyName = Environment.GetCommandLineArgs()[0];
            assemblyVersion = Assembly.GetEntryAssembly().GetName().Version;
            if (keySelector == (char)KeySelector.CrossPlatformCompatible)
            {
                keyChar = Environment.OSVersion.Platform == PlatformID.Win32NT ? '/' : '-';
            }
            else keyChar = keySelector;
            
        }

        public void Show(HelpFormatting formatting = 0)
        {
            // Title
            Console.Write($"{appName} Command Line");
            if (formatting.HasFlag(HelpFormatting.ShowVersion)) Console.WriteLine($", Version {assemblyVersion}");
            Console.Write(Environment.NewLine);

            // Usage
            Console.Write($"Usage: {assemblyName} ");
            if (options != null) Console.Write("[options] ");
            if (commands != null) Console.Write("[commands]");
            Console.Write(Environment.NewLine);

            // Options
            if (options != null)
            {
                int tabs = 1;
                tabs = GetTotalNeededTabs(options);

                Console.WriteLine($"Options:");
                for (int i = 0; i < options.Count; i++)
                {
                    Console.WriteLine($"  {keyChar}{options[i].ShortName}");


                }
            }

            // Commands
            if (commands != null)
            {
                Console.WriteLine($"Commands:");
            }
        }

        private int GetTotalNeededTabs(List<ArgumentOption> options)
        {
            int max = 1;
            for (int i = 0; i < options.Count; i++)
            {
                int t = (int)Math.Ceiling(options[i].GetHelpKeyNames(keyChar).Length / 10f);
                if (t > max) max = t;
            }
            return max;
        }
    }
}
