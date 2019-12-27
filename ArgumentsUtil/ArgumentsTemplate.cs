using System;
using System.Collections.Generic;
using System.Reflection;

namespace ArgumentsUtil
{
    public class ArgumentsTemplate
    {
        private readonly List<ArgumentOption> options;
        private readonly bool optionRequired;
        private readonly List<ArgumentCommand> commands;
        private readonly bool commandRequired;
        private readonly List<ArgumentText> texts;
        private readonly string appName;
        private readonly string assemblyName;
        private readonly Version assemblyVersion;
        private readonly char keyChar;

        public ArgumentsTemplate(List<ArgumentOption> Options = null, bool optionRequired = false, List<ArgumentCommand> Commands = null, bool commandRequired = false, List<ArgumentText> Texts = null, string AppName = null, char keySelector = (char)KeySelector.CrossPlatformCompatible, Version version = null, bool showFileExt = false)
        {
            options = Options;
            this.optionRequired = optionRequired;
            commands = Commands;
            this.commandRequired = commandRequired;
            texts = Texts;
            assemblyName = Environment.GetCommandLineArgs()[0];
            assemblyName = assemblyName.Substring(assemblyName.LastIndexOf('\\') + 1, assemblyName.Length - assemblyName.LastIndexOf('\\') - 1);
            if (!showFileExt) assemblyName = assemblyName.Substring(0, assemblyName.LastIndexOf('.'));

            appName = AppName ?? (
                !showFileExt ? assemblyName :
                assemblyName.Substring(0, assemblyName.LastIndexOf('.'))
                );

            assemblyVersion = version ?? Assembly.GetEntryAssembly().GetName().Version;
            if (keySelector == (char)KeySelector.CrossPlatformCompatible)
            {
                keyChar = Environment.OSVersion.Platform == PlatformID.Win32NT ? '/' : '-';
            }
            else keyChar = keySelector;
            
        }

        public void ShowManual(HelpFormatting formatting = HelpFormatting.Default)
        {
            // Title
            Console.Write($"{appName} Command Line");
            if (formatting.HasFlag(HelpFormatting.ShowVersion)) Console.Write($" {assemblyVersion}");
            Console.Write(Environment.NewLine + Environment.NewLine);

            // Usage
            Console.Write($"Usage: {assemblyName} ");
            if (options != null) Console.Write(!optionRequired ? "(options) " : "[options] ");
            if (commands != null) Console.Write(!commandRequired ? "(commands)" : "[commands]");
            Console.Write(Environment.NewLine + Environment.NewLine);

            // Options
            if (options != null)
            {
                int indention = GetNeededIndent(options);

                Console.WriteLine($"Options:");
                if (formatting.HasFlag(HelpFormatting.TitleUnderlines)) Console.WriteLine("¨¨¨¨¨¨¨¨");
                for (int i = 0; i < options.Count; i++)
                {
                    string usage = options[i].GetManualUsage(keyChar);
                    Console.Write($"  {usage}");
                    
                    for (int j = 0; j < indention - GetOptionSize(options[i]); j++) Console.Write(' ');

                    if (options[i].Description != null) Console.Write(options[i].Description);
                    Console.Write(Environment.NewLine);
                }
                Console.Write(Environment.NewLine);
            }

            // Commands
            if (commands != null)
            {
                int indention = GetNeededIndent(commands) + 2;

                Console.WriteLine($"Commands:");
                if (formatting.HasFlag(HelpFormatting.TitleUnderlines)) Console.WriteLine("¨¨¨¨¨¨¨¨¨");
                for (int i = 0; i < commands.Count; i++)
                {
                    string usage = $"{commands[i].Name}";
                    Console.Write($"  {usage}");
                    
                    for (int j = 0; j < indention - GetCommandSize(commands[i]); j++) Console.Write(' ');

                    if (commands[i].Description != null) Console.Write(commands[i].Description);
                    Console.Write(Environment.NewLine);
                }
            }

            if (texts != null)
            {
                for (int i = 0; i < texts.Count; i++)
                {
                    Console.WriteLine();
                    texts[i].Show(formatting);
                }
            }
        }

        private int GetNeededIndent(List<ArgumentOption> options)
        {
            int max = 1;
            for (int i = 0; i < options.Count; i++)
            {
                int indent = GetOptionSize(options[i]) + 2;
                if (indent > max) max = indent;
            }
            return max;
        }
        private int GetNeededIndent(List<ArgumentCommand> commands)
        {
            int max = 1;
            for (int i = 0; i < commands.Count; i++)
            {
                int indent = GetCommandSize(commands[i]) + 2;
                if (indent > max) max = indent;
            }
            return max;
        }

        private int GetOptionSize(ArgumentOption option) => 2 + option.GetManualUsage(keyChar).Length;

        private int GetCommandSize(ArgumentCommand command) => 2 + command.Name.Length;
    }
}
