using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using ReallyTinyCms.Core.Model;

namespace ReallyTinyCms.Core.Storage
{
    public class ReadOnlyHttpContentRepository : ICmsContentRepository
    {
        private readonly string _baseUrl;

        public ReadOnlyHttpContentRepository(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');
        }

        public IList<CmsContentItem> RetrieveAll()
        {
            var xml = XDocument.Load(_baseUrl+"/all");
            return (from c in xml.Element("content").Elements("contentItem")
                    select new CmsContentItem(c.Element("slug").Value)
                    {
                        Content = c.Element("content").Value
                    }
                   ).ToList();

        }

        public CmsContentItem Retrieve(string contentItemName)
        {
            string content = string.Empty;
            try
            {
                content = new WebClient().DownloadString(_baseUrl + contentItemName);
            } finally
            {
                content = "";
            }
            return new CmsContentItem(contentItemName)
            {
                Content = content
            };
        }

        public void SaveOrUpdate(CmsContentItem item)
        {
            throw new NotImplementedException();
        }

        public void Delete(string contentItemName)
        {
            throw new NotImplementedException();
        }

        public bool StorageExists()
        {
            var xml = XDocument.Load(_baseUrl + "/all");
            return xml.Element("content") != null;
        }

        public void CreateStorage()
        {
            throw new NotImplementedException();
        }
    }
}