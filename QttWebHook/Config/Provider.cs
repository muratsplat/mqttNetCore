
using DotNetEnv;
using System;

namespace QttWebHook.Config
{
    public class Provider 
    {
        public Model FromENV()
        {
            DotNetEnv.Env.Load();
            Model conf = new Model();
            try
            {
                conf.Port = System.Int32.Parse(GetOnENV("PORT"));
                conf.DefaultUser = GetOnENV("DEFAULT_USER");
                conf.DefaultPass = GetOnENV("DEFAULT_PASS");
            }
            catch (System.Exception)
            {
                // TODO: handlig
                throw;
            }
            
            return conf;
        }        

        public string GetOnENV(string key)
        {
            return System.Environment.GetEnvironmentVariable(key);
        }
    }

}