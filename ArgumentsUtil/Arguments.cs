using System;
using System.Collections.Generic;

namespace ArgumentsUtil
{
    public struct Arguments
    {
        public static Arguments Parse(string[] args, char keySelector = (char)KeySelector.CrossPlatformCompatible)
        {
            Arguments arguments = new Arguments();
            arguments._dictionary = new Dictionary<string, List<string>>();
            arguments.Keyless = new List<string>();
            bool isKeyless = true;
            for(int i = 0; i < args.Length; i++)
            {
                if (args[i].Length > 0) {
                    char s = args[i][0]; // Selector
                    if (keySelector == (char)KeySelector.CrossPlatformCompatible && (s == '/' || s == '-') ||
                        s == keySelector)
                    {
                        isKeyless = false;
                        string key = args[i].Remove(0, 1);
                        List<string> values = new List<string>();
                        while (i < args.Length - 1)
                        {
                            i++;
                            s = args[i][0]; // Selector
                            if (keySelector == (char)KeySelector.CrossPlatformCompatible && (s == '/' || s == '-') ||
                        s == keySelector)
                            {
                                // continue with next key argument
                                i--;
                                break;
                            }
                            else
                            {
                                values.Add(args[i]);
                            }
                        }
                        if (!arguments._dictionary.ContainsKey(key)) arguments._dictionary.Add(key, values);
                    }
                }

                if (isKeyless)
                {
                    arguments.Keyless.Add(args[i]);
                }
            }

            return arguments;
        }

        public List<string> Keyless;
        private Dictionary<string, List<string>> _dictionary;
        
        public string[] this[string key]
        {
            get
            {
                return _dictionary[key].ToArray();
            }
            set
            {
                
                _dictionary[key] = new List<string>(value);
            }
        }
        public string this[int key]
        {
            get
            {
                return Keyless[key];
            }
        }

        public int Length => Keyless.Count + _dictionary.Count;
        public bool ContainsKey(string key) => _dictionary.ContainsKey(key);

        public bool ContainsPattern(string key, params Type[] types)
        {
            if (!_dictionary.ContainsKey(key)) return false;
            List<string> keyValues = _dictionary[key];
            if (types.Length > keyValues.Count) return false;
            for (int i = 0; i < keyValues.Count; i++)
            {
                for (int j = 0; j < types.Length; j++)
                {
                    if (types[j] == typeof(string)) continue; // A string is always convertable to a string.

                    object typeVal = null;
                    try
                    {
                        typeVal = Convert.ChangeType(keyValues[i], types[j]);
                    }
                    catch { }
                    if (typeVal == null) return false;
                }
            }
            return true;
        }
    }
}
