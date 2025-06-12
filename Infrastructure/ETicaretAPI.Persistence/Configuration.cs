using Microsoft.Extensions.Configuration;

namespace ETicaretAPI.Persistence
{
    static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();

                //configurationManager.SetBasePath(Path.Combine("C:\\Users\\Emirhan\\Desktop\\ETicaretUygulamasi\\ETicaretAPI\\Prensentation\\ETicaretAPI.API"));

                try
                {
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory()));
                    configurationManager.AddJsonFile("appsettings.json");
                }
                catch
                {
                    configurationManager.AddJsonFile("appsettings.Production.json");
                }

                return configurationManager.GetConnectionString("PostgreSQL");
            }
        }
    }
}
