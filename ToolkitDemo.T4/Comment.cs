using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Content.Query;
using Kooboo.CMS.Toolkit;  

namespace ToolkitDemo.Models
{
	public class Comment : TextContent
	{
		public Comment()
			: base(new TextContent())
		{ }

		public Comment(TextContent textContent)
			: base(textContent)
		{ }

		public class FieldNames
		{
			public const string Title = "Title";
			public const string Body = "Body";
			public const string Name = "Name";
			public const string Member = "Member";
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
			
		public string Body
		{
			get
			{
				return this.GetString(FieldNames.Body);
			}
			set
			{
				this[FieldNames.Body] = value;
			}
		}
			
		public string Name
		{
			get
			{
				return this.GetString(FieldNames.Name);
			}
			set
			{
				this[FieldNames.Name] = value;
			}
		}
			
		public string Member
		{
			get
			{
				return this.GetString(FieldNames.Member);
			}
			set
			{
				this[FieldNames.Member] = value;
			}
		}
	}
}
