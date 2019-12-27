using System;

namespace ArgumentsUtil
{
    public enum KeySelector : short
    {
        Windows = (short)'/',
        Linux = (short)'-',
        CrossPlatformCompatible = 0
    }

    [Flags]
    public enum HelpFormatting
    {
        None = 0,
        ShowVersion = 1 << 0,
        TitleUnderlines = 1 << 1,

        Default = ShowVersion | TitleUnderlines
    }
}
