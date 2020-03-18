using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication10.Properties.Config;

namespace WebApplication10.Services
{
    public class Singleton
    {
        private static Singleton _singletonObject;
        public static Singleton SingletonObj
        {
             
        get
            {
                if (_singletonObject == null)
                {
                    _singletonObject = new Singleton();
                };
                return _singletonObject;
            }
        }
        private Config _configService;
        public Config ConfigService
        {
            get
            {
                if (_configService == null)
                {
                    _configService = new Config();
                };
                return _configService;
            }
        }
    }
}
