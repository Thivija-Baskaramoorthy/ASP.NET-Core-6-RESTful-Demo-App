namespace WebAppDemo.Helpers
{
    public class SupportUtils
    {
        public static string LoadStringFromFile(string fullPath)
        {
            var str = File.ReadAllText(fullPath); //read all the content inside the file

            return str;
        }
    }
}
