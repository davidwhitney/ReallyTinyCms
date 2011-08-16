using System;
using ReallyTinyCms.Core;

namespace ReallyTinyCms
{
    public class ConfigurationBuilder
    {
        private readonly ContentController _contentController;

        internal ConfigurationBuilder(ContentController contentController)
        {
            _contentController = contentController;
        }

        public ConfigurationBuilder WhenCacheRefreshes(Action action)
        {
            _contentController.CacheRefreshCallback = action;
            return this;
        } 

        public ConfigurationBuilder WhenContentIsRequested(Action<string, string> action)
        {
            _contentController.ContentForCallback = action;
            return this;
        }  

        public ConfigurationBuilder AndRefreshInterval(int refreshInterval)
        {
            _contentController.ContentRegistration.DesiredRefreshInterval = refreshInterval;
            return this;
        }  
    }
}