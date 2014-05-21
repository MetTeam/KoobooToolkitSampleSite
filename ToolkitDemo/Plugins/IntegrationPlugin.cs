using System.Web;
using System.Web.Mvc;
using Kooboo.CMS.Sites.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kooboo.CMS.Sites.Models;
using Kooboo.CMS.Sites.View;
using Kooboo.CMS.Toolkit;
using Kooboo.CMS.Toolkit.Plugins;
using Kooboo.Reflection;
using ToolkitDemo.Models;

namespace ToolkitDemo.Plugins
{
    public class IntegrationPlugin : PluginBase
    {
        public override ActionResult Execute()
        {
            var modelState = ViewData.ModelState;
            if (IsPost)
            {
                try
                {
                    System.Web.Helpers.AntiForgery.Validate();
                }
                catch (Exception)
                {
                    throw new HttpException(500, "AntiForgery validate failed!");
                }
                var textContent = Request.Form.ToTextContent();
                var comment = new Comment(textContent) { Published = true };
                ViewData["Comment"] = comment;
                //TODO: validate content

                //TODO: Add Content
                if (modelState.IsValid)
                {
                    //...


                    //TODO: Update User

                    ViewData["Comment"] = null;
                    return null;
                }

            }
        }
    }
}
