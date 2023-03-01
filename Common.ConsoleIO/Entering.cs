using System;
using System.Text.RegularExpressions;

namespace Common.ConsoleIO
{
    public static class Entering
    {
        public static string format = "{0,40}: ";

        public static int EnterInt32(string prompt)
        {
            while (true)
            {
                Console.Write("\t{0}: ", prompt);
                string s = Console.ReadLine();
                int value;
                if (int.TryParse(s, out value))
                {
                    return value;
                }
                Console.WriteLine("\t\tпомилка введення цілого числа");
            }
        }

        public static int EnterInt32Cheking(string prompt)
        {
            while (true)
            {
                Console.Write("\t{0}: ", prompt);
                string str = Console.ReadLine();
                int value;
                if (int.TryParse(str, out value))
                {
                    if (value <= 0)
                        Console.WriteLine("Значення не може бути меньше або рівне 0");
                    value = EnterInt32("Спробуйте ще раз ввести сторону (має бути більше 0)");
                    return value;
                }
            }
        }

        public static int EnterInt32(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write("\t{0}: ", prompt);
                string s = Console.ReadLine();
                int value;
                if (int.TryParse(s, out value))
                {
                    if (value >= min && value <= max)
                        return value;
                }
                Console.WriteLine($"Потрібно ввести ціле число у межах" +
                       $" {min} - {max}");
            }
        }
        public static int? EnterNullableInt32(string prompt)
        {
            Console.Write("{0}: ", prompt);
            while (true)
            {
                Console.Write("\t{0}: ", prompt);
                string s = Console.ReadLine();
                int value;
                if (string.IsNullOrWhiteSpace(s) == true)
                {
                    return (int?)null;
                }
                if (int.TryParse(s, out value))
                {
                    return value;
                }
                Console.WriteLine("\t\tпомилка введення цілого числа");
            }
        }
        public static string EnterString(string prompt)
        {
            Console.Write("\t{0}: ", prompt);
            return Console.ReadLine().Trim();
        }

        public static string EnterString(string prompt, int min, int max)
        {
            Console.Write("\t{0}: ", prompt);
            while (true)
            {
                string str = Console.ReadLine().Trim();
                if (str.Length >= min && str.Length <= max) return str;
                Console.WriteLine($"Розмір введеного від {min} до {max}"); ;
            }
        }

        public static string EnterString(string prompt, int max)
        {
            Console.Write("\t{0}: ", prompt);
            while (true)
            {
                string str = Console.ReadLine().Trim();
                if (str.Length <= max) return str;
                Console.WriteLine("Розмір введеного завеликий");
            }
        }
        public static string EnterString(string prompt, string regex, RegexOptions regexOptions = RegexOptions.None)
        {
            Console.Write("\t{0}: ", prompt);
            while (true)
            {
                string str = Console.ReadLine().Trim();
                if (Regex.IsMatch(str, regex, regexOptions))
                    return str;
                Console.WriteLine("Не за форматом");
            }
        }

        public static string EnterStringOrNull(string prompt)
        {
            Console.Write(format, prompt);
            string s = Console.ReadLine();
            s = s.Trim();
            return s == "" ? null : s;
        }

        public static double EnterDouble(string prompt)
        {
            Console.Write(prompt);
            while (true)
            {
                string s = Console.ReadLine();
                s = s.Replace(".", ",");
                double value;

                if (double.TryParse(s, out value))
                {
                    return Convert.ToDouble(value);
                }
                Console.WriteLine("Некоректне значення");
            }
        }
        public static double EnterDouble(string prompt, double min, double max)
        {

            while (true)
            {
                double value = EnterDouble(prompt);
                if (value >= min && value <= max)
                {
                    return value;
                }

                Console.WriteLine($"Потрібно ввести ціле число у межах" +
                       $" {min} - {max}");
            }
        }

        public static bool EnterBool(string prompt)
        {
            string res;
            while (true)
            {
                res = EnterString(prompt).ToUpper();
                if (res == "ТАК")
                {
                    return true;
                }
                if (res == "НІ")
                {
                    return false;
                }
                Console.WriteLine(format, "Неправильне введення");


            }
        }
        public static ulong EnterUInt64(string prompt)
        {
            Console.Write("\t{0}: ", prompt);
            string str = Console.ReadLine();
            return ulong.Parse(str);
        }

        public static uint EnterUInt32InHex(string title)
        {
            while (true)
            {
                Console.Write("{0} :", title);
                string str = Console.ReadLine();
                if (UInt32.TryParse(str, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out uint value))
                {
                    return value;
                }
                Console.WriteLine("Неправильний формат");
            }

        }
    }
}
