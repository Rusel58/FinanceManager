using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting_for_finance.presentation.IOController
{
    /// <summary>
    /// A class for multi-colored input and output to the console.
    /// </summary>
    public static class ConsoleController
    {

        /// <summary>
        /// Analog of the "Console.Write" method with colored text.
        /// </summary>
        /// <param name="writeObject">Object to output.</param>
        /// <param name="color">Color which console output to write Object.</param>
        public static void Write(object writeObject, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(writeObject);
            Console.ResetColor();
        }

        /// <summary>
        /// Analog of the "Console.WriteLine" method with colored text.
        /// </summary>
        /// <param name="writeObject">Object to output.</param>
        /// <param name="color">Color which console output to write Object.</param>
        public static void WriteLine(object writeObject, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(writeObject);
            Console.ResetColor();
        }

        /// <summary>
        /// Analog of the "Console.ReadLine" method with colored text.
        /// </summary>
        public static string ReadLine()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string result = Console.ReadLine();
            Console.ResetColor();
            return result;
        }
    }
}
