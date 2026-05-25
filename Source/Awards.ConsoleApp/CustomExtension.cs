namespace Awards
{
    using System;

    /// <summary>
    /// A helper class for displaying data line by line on the console.
    /// </summary>
    public static class CustomExtension
    {
        /// <summary>
        /// Loops through the <see cref="System.Collections.Generic.IEnumerable{T}"/> parametre and prints each line to the console.
        /// </summary>
        /// <typeparam name="T">The generic type.</typeparam>
        /// <param name="input">A set of data to be printed.</param>
        /// <param name="title">The optional title to be printed.</param>
        public static void PrintToConsole<T>(this System.Collections.Generic.IEnumerable<T> input, string title = "")
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input), " was null.");
            }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n  BEGIN: " + title);
            Console.ResetColor();

            foreach (var item in input)
            {
                Console.WriteLine(item.ToString());
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine((string.IsNullOrWhiteSpace(title) ? "  " : $" {title} ") + "END.\t(Press a key)");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
