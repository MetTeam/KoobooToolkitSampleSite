using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Content.Query;
using Kooboo.CMS.Toolkit;  

namespace ToolkitDemo.Models
{
	public class Category : TextContent
	{
		public Category()
			: base(new TextContent())
		{ }

		public Category(TextContent textContent)
			: base(textContent)
		{ }

		public class FieldNames
		{
			public const string Title = "Title";
		}
			
		public string Title
		{
			get
			{
				return this.GetString(FieldNames.Title);
			}
			set
			{
				this[FieldNames.Title] = value;
			}
		}
	}
}
