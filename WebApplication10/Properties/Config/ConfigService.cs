using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication10.Properties.Config
{
    public class Config
    {
        public readonly IConfigurationRoot AppSettings;
        public Config()
        {
            AppSettings = GetConfigurationRoot();
        }

        private IConfigurationRoot GetConfigurationRoot()
        {
            IConfigurationRoot configuration = null;
            var basePath = System.AppContext.BaseDirectory;
            var configurationBuilder = new ConfigurationBuilder().SetBasePath(basePath).AddJsonFile($"appsettings.json");
            configuration = configurationBuilder.Build();
            return configuration;
            throw new NotImplementedException();
        }
    }
}
