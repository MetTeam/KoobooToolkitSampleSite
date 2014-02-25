using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kooboo.CMS.Content.Models;
using Kooboo.CMS.Content.Query;
using Kooboo.CMS.Toolkit;  

namespace ToolkitDemo.Models
{
	public class TestTree : TextContent
	{
		public TestTree()
			: base(new TextContent())
		{ }

		public TestTree(TextContent textContent)
			: base(textContent)
		{ }

		public class FieldNames
		{
			public const string Title = "Title";
			public const string Summary = "Summary";
			public const string Description = "Description";
			public const string Readings = "Readings";
			public const string ParentUUID = "ParentUUID";
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
			
		public string Summary
		{
			get
			{
				return this.GetString(FieldNames.Summary);
			}
			set
			{
				this[FieldNames.Summary] = value;
			}
		}
			
		public string Description
		{
			get
			{
				return this.GetString(FieldNames.Description);
			}
			set
			{
				this[FieldNames.Description] = value;
			}
		}
			
		public int Readings
		{
			get
			{
				return this.GetInt(FieldNames.Readings);
			}
			set
			{
				this[FieldNames.Readings] = value;
			}
		}
			
		public string ParentUUID
		{
			get
			{
				return this.GetString(FieldNames.ParentUUID);
			}
			set
			{
				this[FieldNames.ParentUUID] = value;
			}
		}
	}
}