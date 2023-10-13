using Newtonsoft.Json;
using CoWorkingApp.Data;

namespace CoWorkingApp.Data
{
    public class JsonManager<T>
    {
        public List<T> GetCollection()
        {
            // Try this code, if doesnt works clean it and use the first line
            //string collectionPath = $@"{Directory.GetCurrentDirectory()}/{typeof(T)}.json";
            string collectionPath = HelperDirectory<T>.GetDirectoryWithCollection();
            List<T> myCollection = new List<T>();

            if(File.Exists(collectionPath))
            {
                var streamReader = new StreamReader(collectionPath);
                var currentContent = streamReader.ReadToEnd();
                myCollection = JsonConvert.DeserializeObject<List<T>>(currentContent) ?? new List<T>();
                streamReader.Close();
            }
            else
            {
                var streamWriter = new StreamWriter(collectionPath);
                var jsonCollection = JsonConvert.SerializeObject(myCollection, Formatting.Indented);
                streamWriter.WriteLine(jsonCollection);
                streamWriter.Close();
            }

            return myCollection;
        }

        public bool SaveCollection(List<T> collection)
        {
            // Try this code, if doesnt works clean it and use the first line
            //string collectionPath = $@"{Directory.GetCurrentDirectory()}/{typeof(T)}.json";
            string collectionPath = HelperDirectory<T>.GetDirectoryWithCollection();
            try
            {
                var streamWriter = new StreamWriter(collectionPath);
                var jsonCollection = JsonConvert.SerializeObject(collection, Formatting.Indented);
                streamWriter.WriteLine(jsonCollection);
                streamWriter.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}