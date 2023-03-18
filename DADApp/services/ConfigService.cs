using DADApp.forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DADApp.services
{
    class ConfigService
    {
        public static Dictionary <String, String> configDict = new Dictionary<String, String>();

        public static String version = "0.0";

        public static void readConf()
        {
            configDict = new Dictionary<string, string>();
            if (!File.Exists(DADConstants.FILE_CONFIG_NAME))
            {
                File.Create(DADConstants.FILE_CONFIG_NAME).Close();
            }
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(DADConstants.FILE_CONFIG_NAME);
                String temp = reader.ReadLine();
                while (temp != null)
                {
                    String[] key_value_str = temp.Split('=');
                    configDict.Add(key_value_str[0], key_value_str[1]);
                    temp = reader.ReadLine();
                }
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        public static String getConfig(String key)
        {
            if(configDict.ContainsKey(key))
            {
                return configDict[key];
            }
            return null;
            
        }

        public static void setConfig(String key, String value)
        {
            String configStr = key + "=" + value + "\n"; ;
            foreach (var a in configDict)
            {
                if (!a.Key.Equals(key))
                {
                    configStr += a.Key + "=" + a.Value + "\n";
                }
            }
            if (configStr.EndsWith("\n")) {
                configStr = configStr.TrimEnd('\n');
            }

            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(DADConstants.FILE_CONFIG_NAME);
                writer.Write(configStr);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
            readConf();
        }

        public static void firstRun()
        {
            if (!configDict.ContainsKey("isFirstStart") 
                || Boolean.Parse(configDict["isFirstStart"]))
            {
                setConfig("isFirstStart", false.ToString());
            }
        }

    }
}
