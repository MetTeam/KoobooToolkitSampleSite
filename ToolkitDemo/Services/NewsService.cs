using Kooboo.CMS.Content.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolkitDemo.Models;
using Kooboo.CMS.Content.Query;
using Kooboo.CMS.Toolkit;
using Kooboo.CMS.Content.Models;

namespace ToolkitDemo.Services
{
    public class InternationalNewsService : ArticleService
    {
        public override string FolderName
        {
            get
            {
                return "News~International";
            }
        }
    }

    public class DomesticNewsService : ArticleService
    {
        public override string FolderName
        {
            get
            {
                return "News~Domestic";
            }
        }

    }

    public class SportsNewsService : ArticleService
    {
        public override string FolderName
        {
            get
            {
                return "News~Sports";
            }
        }
    }

    public class NewsService : ArticleService
    {
        private const string cacheKey = "ToolkitDemo.Services.NewsService.GetCategories";

        public override string FolderName
        {
            get
            {
                return "News";
            }
        }

        public IEnumerable<Article> GetTop(int takeCount = 10)
        {
            var folderNames = GetCategories().Select(it => it.FullName).ToList();
            //folderNames.Add(Folder.FullName);  //Current Folder
            return this.Schema
                .CreateQuery()
                .WhereEquals("Published", true)
                .WhereIn("FolderName", folderNames.ToArray())
                .OrderByDescending(it => it["Readings"])
                .Take(takeCount)
                .MapTo<Article>();
        }

        public IEnumerable<TextFolder> GetCategories()
        {
            var obj = CacheUtility.Get<IEnumerable<TextFolder>>(cacheKey);
            if (null == obj)
            {
                obj = ServiceFactory.TextFolderManager.ChildFolders(Folder);
                CacheUtility.Add(cacheKey, obj);
            }
            return obj;
        }
    }
}
