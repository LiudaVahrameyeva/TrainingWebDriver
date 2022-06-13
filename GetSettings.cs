using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestProject2;

public class GetSettings
{
    private static string config_dir = "conf";
    //private string[] fileEntries = Directory.GetFiles(config_dir);
    

    public Item GetItem()
    {
        string file = GetConfigFile();
        //string[] fileName = Directory.GetFiles(config_dir);
        var setupSettings  = ConfigReader.Read(file);

       return setupSettings;
    }

    private string GetConfigFile()
    {
   
    var setupSettings  = ConfigReader.Read("Config\\Settings.json");
    string file = "";
    
    switch (setupSettings.platformName)
    {
      
        case "Mac":
            file = "Config\\Settings.Mac.json";
            break;
        case "Win10":
            file = "Config\\Settings.Win10.json";
            break;
        case "Win8":
            file = "Config\\Settings.Win8.json";
            break;
        default:
            Console.WriteLine($"File with {setupSettings.platformName} name wasn't found");
            break;
    }


    return file;
    }


    public string PlatformName { get; set; }
    public string Browser { get; set; }
    public string BrowserVersion { get; set; }
}