using Microsoft.Extensions.Configuration;
using System.IO;

namespace RepositoryPattern.Data.Repositories
{
    public abstract class BaseRepository
    {
        private IConfigurationRoot root;
        
        public BaseRepository()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            root = configurationBuilder.Build();
        }

        public IConfigurationSection GetConfigurationSection(string sectionName)
        {
            IConfigurationSection section = root.GetSection(sectionName);
            return section;
        }
    }
}
