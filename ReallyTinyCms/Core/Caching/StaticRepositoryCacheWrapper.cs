using System;
using System.Collections.Generic;
using System.Linq;
using ReallyTinyCms.Core.Model;
using ReallyTinyCms.Core.Storage;

namespace ReallyTinyCms.Core.Caching
{
	public class StaticRepositoryCacheWrapper : ICmsContentRepository
	{
		private readonly Func<ICmsContentRepository> _realContentRepository;

		private DateTime _lastUpdated;
		private readonly int _cacheDurationInMilliseconds;
		private readonly Dictionary<string, CmsContentItem> _cachedItems; 

		public StaticRepositoryCacheWrapper(Func<ICmsContentRepository> realContentRepository, int cacheDurationInMilliseconds)
		{
			_realContentRepository = realContentRepository;
			_cacheDurationInMilliseconds = cacheDurationInMilliseconds;
			_cachedItems = new Dictionary<string, CmsContentItem>();
		}

		public IList<CmsContentItem> RetrieveAll()
		{
			UpdateCacheIfRequired();
			return _cachedItems.Values.ToList();
		}

		public CmsContentItem Retrieve(string contentItemName)
		{
			UpdateCacheIfRequired();
			return _cachedItems[contentItemName];
		}

		public void SaveOrUpdate(CmsContentItem item)
		{
			_realContentRepository().SaveOrUpdate(item);
			UpdateCache();
		}

		public void Delete(string contentItemName)
		{
			_realContentRepository().Delete(contentItemName);
			UpdateCache();
		}

		private void UpdateCacheIfRequired()
		{
			if (_lastUpdated.AddMilliseconds(_cacheDurationInMilliseconds) > DateTime.Now)
			{
				return; // Cache still within expiry
			}

			UpdateCache();
		}

		private void UpdateCache()
		{

			lock(_cachedItems)
			{
				var items = _realContentRepository().RetrieveAll();
				_cachedItems.Clear();

				foreach(var item in items)
				{
					_cachedItems.Add(item.Name, item);
				}

				_lastUpdated = DateTime.Now;
			}
		}
	}
}
