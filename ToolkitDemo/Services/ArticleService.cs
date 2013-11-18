
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Content.Query;
using Kooboo.CMS.Toolkit;
using Kooboo.CMS.Toolkit.Services;
using ToolkitDemo.Models;

namespace ToolkitDemo.Services
{
    public class ArticleService : ServiceBase<Article>
    {
        public override string FolderName
        {
            get { return FolderNames.Article; }
        }

        public override Article Get(TextContent textContent)
        {
            if (textContent != null)
            {
                return new Article(textContent);
            }
            return null;
        }

        public void Read(Article article, string userName = "")
        {
            if (null != article)
            {
                var readings = article.Readings + 1;
                Kooboo.CMS.Content.Services.ServiceFactory.TextContentManager.Update(Repository.Current, this.Schema, article.UUID, Article.FieldNames.Readings, readings, userName, false);
                //Update(article); //this can work too,but not recommend because a content version will be added.
            }
        }

        public IEnumerable<Comment> GetComments(Article article)
        {
            if (null == article)
            {
                return Enumerable.Empty<Comment>();
            }
            return article.Children(FolderNames.Comment)
                .Published()
                .DefaultOrder()
                .MapTo<Comment>();
        }

        public IEnumerable<Article> GetList(string categoryUserKey)
        {
            var uuid = string.Empty;
            if (String.IsNullOrEmpty(categoryUserKey))
            {
                var firstCategory = ToolkitDemoContext.Current.CategoryService.GetAll().FirstOrDefault();
                if (null == firstCategory)
                {
                    return Enumerable.Empty<Article>();
                }
                uuid = firstCategory.UUID;
            }
            else
            {
                uuid = ToolkitDemoContext.Current.CategoryService.Get(SystemFieldNames.UserKey, categoryUserKey).UUID;
            }
            return this.CreateQuery()
                .Published()
                .WhereCategory(FolderNames.Category, uuid)
                .DefaultOrder()
                .MapTo<Article>();
        }
    }
}
