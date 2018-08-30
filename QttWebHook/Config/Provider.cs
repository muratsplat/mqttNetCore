
using DotNetEnv;
using System;
using System.IO;
using Serilog;

namespace QttWebHook.Config
{
    public class Provider 
    {
        private ILogger _logger;

        public Provider(ILogger logger)
        {
            _logger = logger;
        }
        public Model FromENV()
        {
            Model conf = new Model();
            try
            {
                DotNetEnv.Env.Load();
                conf.Port = System.Int32.Parse(GetOnENV("PORT"));
                conf.DefaultUser = GetOnENV("DEFAULT_USER");
                conf.DefaultPass = GetOnENV("DEFAULT_PASS");
                            _logger.Information($".env file found. Evironment variables are overwritten.");
             }
            catch (System.IO.FileNotFoundException e){
                _logger.Warning($".env file not found. Error: {e.Message}");
            }            
            return conf;
        }        

        public string GetOnENV(string key)
        {
            return System.Environment.GetEnvironmentVariable(key);
        }
    }

}