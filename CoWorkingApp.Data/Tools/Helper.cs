namespace CoWorkingApp.Data
{
    public class HelperDirectory<T>
    {
        public static string GetDirectoryWithCollection()
        {
            return $@"{Directory.GetCurrentDirectory()}/{typeof(T)}.json";;
        }
    }
    
    public class HelperStrings
    {
        public static string ReadInput(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return string.Empty;
            }
            return userInput;
        }


        public static string ReadPassword(string password)
        {
            Console.Write(password);

            string passwordInput = "";
            while (true)
            {
                var keyPress = Console.ReadKey(true);

                if (keyPress.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }
                else if (keyPress.Key == ConsoleKey.Backspace)
                {
                    // Borrar el último carácter de la contraseña.
                    if (passwordInput.Length > 0)
                    {
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        passwordInput = passwordInput.Remove(passwordInput.Length - 1);
                    }
                }
                else
                {
                    Console.Write("*");
                    passwordInput += keyPress.KeyChar;
                }
            }

            return passwordInput;
        }

        public static bool ReadBool(string prompt)
        {
            Console.Write(prompt);
            var res = Console.ReadLine();
            if (res == "Y" || res == "y") return true;
            if (res == "N" || res == "n") return false;
            return false;
        }
    }
}