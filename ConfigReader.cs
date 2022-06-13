using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;


namespace TestProject2;

public class ConfigReader
{
    private static StreamReader _streamReader;
    public static Item Read(string file)
    {
        Item items = new Item();
        using (StreamReader r = new StreamReader(file))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<Item>(json);
        }
        return items;
    }
}

public class Item
{
    public string platformName;
    public string browser;
    public string browserVersion;
}
