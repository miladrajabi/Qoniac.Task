using System;
using System.Globalization;

namespace Qoniac.API.Convertors
{
    public static class CurrencyToWords
    {
        public static string CurrencyToWord(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                try
                {
                    if (value.Contains(","))
                        value = value.Replace(",", ".");

                    var decimalValue = Convert.ToDecimal(value);

                    var decimals = "";
                    var input = Math.Round(decimalValue, 2).ToString(CultureInfo.InvariantCulture);


                    if (input.Contains("."))
                    {
                        decimals = input.Substring(input.IndexOf(".", StringComparison.Ordinal) + 1);
                        input = input.Remove(input.IndexOf(".", StringComparison.Ordinal));
                    }
                    var strWords = GetNumberToWords(input) + " Dollars";
                    if (decimals.Length > 0)
                    {
                        strWords += " and " + GetNumberToWords(decimals) + " Cents";
                    }

                    return strWords;
                }
                catch (Exception e)
                {
                    throw new Exception("Invalid Format");
                }
            }
            return string.Empty;
        }
        private static string GetNumberToWords(string input)
        {
            var separators = new[] { "", " Thousand ", " Million ", " Billion " };
            var i = 0;

            var strWords = string.Empty;

            while (input.Length > 0)
            {
                var _3digits = input.Length < 3 ? input : input.Substring(input.Length - 3);
                input = input.Length < 3 ? "" : input.Remove(input.Length - 3);

                var no = int.Parse(_3digits);
                _3digits = GetNumberToWord(no);

                _3digits += separators[i];
                strWords = _3digits + strWords;
                i++;
            }
            return string.IsNullOrWhiteSpace(strWords) ? "Zero " : strWords;
        }

        private static string GetNumberToWord(int number)
        {
            string[] ones =
            {
                "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven",
                "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Ninteen"
            };

            string[] tens = { "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            var word = string.Empty;
            if (number > 99 && number < 1000)
            {
                var i = number / 100;
                word = $"{word} {ones[i - 1]} Hundred ";
                number = number % 100;
            }

            if (number > 19 && number < 100)
            {
                int i = number / 10;
                word = $"{word} {tens[i - 1]} ";
                number = number % 10;
            }

            if (number > 0 && number < 20)
                word = word + ones[number - 1];

            return word;
        }
    }
}