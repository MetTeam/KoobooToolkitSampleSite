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
	public class CommentService : ServiceBase<Comment>
    {
        public override Comment Get(TextContent textContent)
        {
            if(textContent != null)
            {
                return new Comment(textContent);
            }
            return null;
        }
	}
}
