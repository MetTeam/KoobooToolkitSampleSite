using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.CMS.Toolkit;

using ToolkitDemo.Services;

namespace ToolkitDemo
{
    public class ToolkitDemoContext : ContextBase
    {
        public static ToolkitDemoContext Current
        {
            get
            {
                return ContextUtility.GetOrAdd<ToolkitDemoContext>("ToolkitDemoContext", () => new ToolkitDemoContext());
            }
        }

        public ArticleService ArticleService
        {
            get { return GetService<ArticleService>(); }
        }

        public CategoryService CategoryService
        {
            get { return GetService<CategoryService>(); }
        }

        public CommentService CommentService
        {
            get { return GetService<CommentService>(); }
        }

        public TestTreeService TestTreeService
        {
            get
            {
                return GetService<TestTreeService>();
            }
        }

        public NewsService NewsService
        {
            get
            {
                return GetService<NewsService>();
            }
        }
    }
}
