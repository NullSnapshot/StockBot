using System;
using System.IO;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using YamlDotNet;
using Logger;



namespace StockBot.Configuration
{
    public static class Config
    {

        //config filepath
        private const string CONFIG_FILEPATH = @"config/config.yml";

        private static FileStream configFile;

        //config values
        private static int ConfigVersion = 1;
        public static string BotToken { get; private set; } = "";
        public static char CommandPrefix { get; private set; } = '!';

        public static bool Initialize()
        {
            configFile = new FileStream(CONFIG_FILEPATH, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //empty file
            if (configFile.Length == 0)
            {
                configFile.Close();
                WriteFile();
            }
            //read file
            else
            {
                string configFileString = "";
                StreamReader sr = new StreamReader(configFile);
                configFileString = sr.ReadToEnd();
                bool fileBuildNeeded = DeSerialize(configFileString);
                sr.Close();
                if (fileBuildNeeded)
                {
                    Log?.Invoke(new StandardLog(LogSeverity.Info, "Config", "Building new config file."));
                    WriteFile();
                }
            }
            configFile.Close();
            return true;
        }

        private static string Serialize()
        {
            YamlStream yaml = new YamlStream(
                new YamlDocument(
                    new YamlMappingNode(
                        new YamlScalarNode("ConfigVersion"), new YamlScalarNode(ConfigVersion.ToString()),
                        new YamlScalarNode("BotToken"), new YamlScalarNode(BotToken),
                        new YamlScalarNode("CommandPrefix"), new YamlScalarNode(CommandPrefix.ToString())
                    )
                )
            );

            StringWriter sw = new StringWriter();
            yaml.Save(sw);

            return sw.ToString();
        }

        private static bool DeSerialize(string configString)
        {
            Deserializer de = new Deserializer();
            Dictionary<string, string> configs = de.Deserialize<Dictionary<string, string>>(configString);
            int tempConfigVersion = -1;
            string tempversionString = "";

            //attempt to read config file version
            bool configInvalid = !configs.TryGetValue("ConfigVersion", out tempversionString);

            if (configInvalid) //config file does not contain version param.
            {
                Log?.Invoke(new StandardLog(LogSeverity.Warning, "Config", $"Config file does not specify version. " +
                 $"Expected Version: {ConfigVersion}"));
            }
            else //config file contains version param. Attempt to validate latest version.
            {
                configInvalid |= !Int32.TryParse(tempversionString, out tempConfigVersion);
                if (configInvalid)
                {
                    Log?.Invoke(new StandardLog(LogSeverity.Warning, "Config", "Unable to parse config file version. " +
                        $"Expected Version: {ConfigVersion}"));
                }
            }
            if (tempConfigVersion < ConfigVersion) //file out of date.
            {
                configInvalid = true;
                Log?.Invoke(new StandardLog(LogSeverity.Warning, "Config", $"Config File Version out of Date. " +
                    $"Current Version: {tempConfigVersion} | " +
                    $"Expected Version: {ConfigVersion}"));
            }

            if (!validateFilePair(configs, "BotToken")) configInvalid = true;
            else BotToken = configs["BotToken"];

            if (!validateFilePair(configs, "CommandPrefix")) configInvalid = true;
            else CommandPrefix = configs["CommandPrefix"].ToCharArray()[0];



            //return true if file build is necessary.
            return configInvalid;
        }

        private static void WriteFile()
        {
            FileStream configFile = new FileStream(CONFIG_FILEPATH, FileMode.Create, FileAccess.Write);
            string configFileString = "";
            StreamWriter sw = new StreamWriter(configFile);
            configFileString = Serialize();
            sw.WriteLine(configFileString);
            sw.Close();
        }

        private static bool validateFilePair(Dictionary<string, string> dictPool, string key)
        {
            string keyValue = "";
            bool valid = dictPool.TryGetValue(key, out keyValue);
            if (String.IsNullOrEmpty(keyValue)) return false;
            return valid;
        }

        public static event Func<StandardLog, Task> Log;
    }
}