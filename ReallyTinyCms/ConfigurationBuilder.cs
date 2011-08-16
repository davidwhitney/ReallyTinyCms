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

        public ConfigurationBuilder OnCacheRefresh(Action action)
        {
            _contentController.CacheRefreshCallback = action;
            return this;
        } 

        public ConfigurationBuilder OnContentFor(Action<string, string> action)
        {
            _contentController.ContentForCallback = action;
            return this;
        }  
    }
}