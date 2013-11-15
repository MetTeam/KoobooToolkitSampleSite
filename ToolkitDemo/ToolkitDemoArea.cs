using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace ToolkitDemo
{
    public class ToolkitDemoArea : AreaRegistration
    {
        public const string Name = "ToolkitDemo";
        public override string AreaName
        {
            get { return Name; }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "ToolkitDemo.Default",
                "_ToolkitDemo/{Controller}/{action}/{siteName}",
                new { controller = "Home", action = "Index", siteName = UrlParameter.Optional },
                new[] { "ToolkitDemo.Controllers" }
                );
        }
    }
}
