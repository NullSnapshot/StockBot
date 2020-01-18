using System;
using System.IO;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using System.Collections.Generic;
using YamlDotNet;


namespace BotConfig
{
    public static class Config
    {
        public static string BotToken { get; private set; } = "";
        private const string CONFIG_FILEPATH = @"config/config.yml";

        static Config()
        {
            FileStream configFile = new FileStream(CONFIG_FILEPATH, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            //empty file
            if (configFile.Length == 0)
            {
                string configFileString = "";
                StreamWriter sw = new StreamWriter(configFile);
                configFileString = Serialize();
                Console.WriteLine(configFileString);
                sw.WriteLine(configFileString);
                sw.Close();
            }
            //read file
            else
            {
                string configFileString = "";
                StreamReader sr = new StreamReader(configFile);
                configFileString = sr.ReadToEnd();
                DeSerialize(configFileString);
                sr.Close();
            }
            configFile.Close();
        }

        private static string Serialize()
        {
            YamlStream yaml = new YamlStream(
                new YamlDocument(
                    new YamlMappingNode(
                        new YamlScalarNode("BotToken"), new YamlScalarNode(BotToken)
                    )
                )
            );

            StringWriter sw = new StringWriter();
            yaml.Save(sw);

            return sw.ToString();
        }

        private static void DeSerialize(string configString)
        {
            Deserializer de = new Deserializer();
            Dictionary<string, string> configs = de.Deserialize<Dictionary<string, string>>(configString);

            BotToken = configs["BotToken"];
        }
    }
}