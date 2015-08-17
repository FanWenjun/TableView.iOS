using UIKit;
using System.Collections.Generic;
using System;

namespace TableView.iOS
{
	public class TableItemGroup
	{
		public TableItemGroup ()
		{
		}
		public string Name { get; set; }

		public string Footer { get; set; }

		public List<TableItem> Items 
		{
			get { return Items; }
			set { Items = value; }
		}
		protected List<TableItem> items = new List<TableItem> ();
	}
}

