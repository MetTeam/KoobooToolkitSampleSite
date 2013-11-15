using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using Kooboo.CMS.Common;
using Kooboo.CMS.Sites.Membership;
using Kooboo.CMS.Sites.Models;
using Kooboo.CMS.Toolkit;
using Kooboo.CMS.Toolkit.Filters;
using Kooboo.Web.Mvc;
using ToolkitDemo.Models;

namespace ToolkitDemo.Controllers
{
    public class CommentController : Controller
    {
        [CMSPageExecutionFilter]
        [HttpPost]
        public ActionResult AddComment(FormCollection formCollection)
        {
            AntiForgery.Validate();
            var textContent = formCollection.ToTextContent();
            var comment = new Comment(textContent) { Published = true };
            var data = new JsonResultData(ModelState);
            data.RunWithTry((resultData) =>
            {
                ToolkitDemoContext.Current.CommentService.Add(comment);
                resultData.Model = ControllerContext.RenderView("Article.CommentItem", comment);
                resultData.AddMessage("Add comment success!");
                resultData.ReloadPage = false;
            });
            return Json(data);
        }

        [CMSPageExecutionFilter]
        [HttpPost]
        public ActionResult DeleteComment(string uuid)
        {
            var data = new JsonResultData(ModelState);
            data.RunWithTry((resultData) =>
            {
                if (User.Identity.IsAuthenticated)
                {
                    ToolkitDemoContext.Current.CommentService.Remove(uuid);
                    resultData.Model = uuid;
                    resultData.AddMessage("Delete success!");
                }
                else
                {
                    resultData.AddErrorMessage("Your handle is forbidden!");
                }
            });
            return Json(data);
        }
    }
}
