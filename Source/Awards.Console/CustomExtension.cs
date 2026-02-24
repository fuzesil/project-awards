namespace Awards
{
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
                throw new System.ArgumentNullException(nameof(input), " was null.");
            }

            System.Console.ForegroundColor = System.ConsoleColor.DarkYellow;
            System.Console.WriteLine("\n  BEGIN: " + title);
            System.Console.ResetColor();

            foreach (var item in input)
            {
                System.Console.WriteLine(item.ToString());
            }

            System.Console.ForegroundColor = System.ConsoleColor.DarkCyan;
            System.Console.WriteLine((string.IsNullOrWhiteSpace(title) ? "  " : $" {title} ") + "END.\t(Press a key)");
            System.Console.ResetColor();
            System.Console.ReadKey();
        }
    }
}
