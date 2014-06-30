using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IDS.DynamicConfiguration;

namespace RateIt.Common
{
    public class Configuration
    {

#region Constants

        public const string PRODUCT_NAME = "RateIt!";
        public const string CONFIGURATION_FILE_NAME = "RateIt.Common.config";

#endregion

#region Static class methods

        private static string GetAssemblyPath(Assembly asm)
        {
            string cfgName = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (Path.GetFileName(cfgName) == "web.config")
            {
                string dirName = Path.Combine(Path.GetDirectoryName(cfgName) ?? "", "bin");
                return Path.Combine(dirName, Path.GetFileName(asm.Location));
            }
            return asm.Location;
        }

        private static string GetCurrentAssemblyPath()
        {
            Assembly asm = Assembly.GetAssembly(typeof(Configuration));
            return Path.GetDirectoryName(GetAssemblyPath(asm));
        }

        private static string GetConfigurationFileName()
        {
            return Path.Combine(GetCurrentAssemblyPath(), CONFIGURATION_FILE_NAME);
        }

        static Configuration()
        {
            string configFile = GetConfigurationFileName();
            string appVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Dictionary<string, string> param = new Dictionary<string, string> { { "FileName", configFile } };
            Config.Init(new Configuration_local(), param, PRODUCT_NAME, appVersion);
        }

#endregion

#region Properties

        public static string DBHost
        {
            get { return Config.Instance["DBHost"]; }
        }

        public static int DBPort
        {
            get { return Config.Instance.ToInt("DBPort"); }
        }

#endregion

    }
}
