/*
    Copyright (c) 2021 Artur Denk
    
    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:
    
    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.
    
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using System;
using System.Text.RegularExpressions;

namespace Terminkalender.View
{
    /// <summary>
    /// This class contains methods used in all view classes.
    /// </summary>
    internal static class AppointmentViewGeneral
    {
        private const string REGEX_DATEFORMAT_DD_MM_YYYY = @"^[A-Za-z]{2}.[A-Za-z]{2}.[A-Za-z]{4}$";
        private const string REGEX_DATETIME_DD_MM_YYYY = @"^(\d{2}).(\d{2}).(\d{4})$";

        private const string REGEX_DATEFORMAT_TIME_DD_MM_YYYY_HH_MM = @"^[A-Za-z]{2}.[A-Za-z]{2}.[A-Za-z]{4} [A-Za-z]{2}:[A-Za-z]{2}$";
        private const string REGEX_DATETIME_TIME_DD_MM_YYYY_HH_MM = @"^(\d{2}).(\d{2}).(\d{4}) (\d{2}):(\d{2})$";

        /// <summary>
        /// Gets the user input as string.
        /// </summary>
        /// <returns>The input as text.</returns>
        internal static string GetUserInputString()
        {
            string input = Console.ReadLine();
            return input;
        }

        /// <summary>
        /// Gets the user input as integer.
        /// </summary>
        /// <returns>The input as number.</returns>
        internal static int GetUserInputInt()
        {
            try
            {
                int input = int.Parse(Console.ReadLine());
                return input;
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets the user input as a DateTime.
        /// </summary>
        /// <param name="dateFormat">The date format to be used.</param>
        /// <returns>The input as nullable DateTime.</returns>
        internal static DateTime? GetUserInputDateTime(string dateFormat)
        {
            try
            {
                string input = Console.ReadLine();
                DateTime date = Convert.ToDateTime(input);

                if (Regex.IsMatch(dateFormat, REGEX_DATEFORMAT_DD_MM_YYYY))
                {
                    if (Regex.IsMatch(input, REGEX_DATETIME_DD_MM_YYYY))
                        return date;
                }

                else if (Regex.IsMatch(dateFormat, REGEX_DATEFORMAT_TIME_DD_MM_YYYY_HH_MM))
                {
                    if (Regex.IsMatch(input, REGEX_DATETIME_TIME_DD_MM_YYYY_HH_MM))
                        return date;
                }

                return null;
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// Notifies the user of a canceled action.
        /// </summary>
        internal static void PrintCanceledAction()
        {
            Console.WriteLine("Der Vorgang wurde abgebrochen.");
        }

        /// <summary>
        /// Notifies the user of an invalid input.
        /// </summary>
        internal static void PrintInvalidInput()
        {
            Console.WriteLine("Die Eingabe ist ungültig.");
        }
    }
}
