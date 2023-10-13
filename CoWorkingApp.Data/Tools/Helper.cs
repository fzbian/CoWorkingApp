namespace CoWorkingApp.Data
{
    public class Helper<T>
    {
        public static string GetDirectoryWithCollection()
        {
            return $@"{Directory.GetCurrentDirectory()}/{typeof(T)}.json";;
        }
    }
}