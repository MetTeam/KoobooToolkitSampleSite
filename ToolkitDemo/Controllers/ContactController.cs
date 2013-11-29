using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;
using Kooboo.CMS.Common;
using Kooboo.CMS.Sites.Globalization;
using Kooboo.CMS.Sites.Models;
using Kooboo.CMS.Toolkit;
using Kooboo.CMS.Toolkit.Filters;
using ToolkitDemo.Models;

namespace ToolkitDemo.Controllers
{
    public class ContactController : Controller
    {
        [CMSPageExecutionFilter]
        [HttpPost]
        public ActionResult Submit(Contact contact)
        {
            AntiForgery.Validate();
            var data = new JsonResultData(ModelState);
            data.RunWithTry((resultData) =>
            {
                var body = ControllerContext.RenderView("Contact.EmailTemplate", contact);
                var smtp = Site.Current.Smtp;
                if (null != smtp && !String.IsNullOrEmpty(smtp.From) && null != smtp.To && smtp.To.Any())
                {
                    foreach (var mail in smtp.To.Select(to => new MailMessage(smtp.From, to, contact.Subject + "(From:{0})".RawLabel("SendEmailFrom").ToString().FormatWith(contact.From), body)))
                    {
                        EmailUtility.Send(mail);
                    }
                    resultData.AddMessage("Send email to us success!We will reply you ASAP".RawLabel("SendEmailSuccess").ToString());
                    resultData.ReloadPage = true;
                }
                else
                {
                    resultData.AddErrorMessage("Smtp was not configured in Site setting".RawLabel("SmtpRequired").ToString());
                }
            });
            return Json(data);
        }
    }
}
