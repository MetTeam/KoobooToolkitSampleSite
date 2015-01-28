using System.Linq;
using System.Web.Mvc;
using Kooboo.CMS.Common.Persistence.Non_Relational;
using Kooboo.CMS.Common.Runtime.Dependency;
using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Content.Services;
using Kooboo.CMS.Content.Query;
using Kooboo.CMS.Web.Areas.Contents.Controllers;

namespace Kooboo.Extensions.ContentFolder
{
    [Dependency(typeof(TextFolderController))]
    public class EmptyContentWhenFolderDeleted : TextFolderController
    {
        public EmptyContentWhenFolderDeleted(TextFolderManager manager)
            : base(manager)
        {}

        [HttpPost]
        public override ActionResult Delete(TextFolder[] model)
        {
            if (model != null && model.Length > 0)
            {
                foreach (var textFolder in model)
                {
                    textFolder.Repository = Repository.Current;
                    var items = textFolder.AsActual().CreateQuery().ToList();
                    foreach (var item in items)
                    {
                        ServiceFactory.TextContentManager.Delete(textFolder.Repository, textFolder, item.UUID);
                    }
                }
            }
            return base.Delete(model);
        }
    }
}
