using UIKit;
using System;

namespace TableView.iOS
{
	public class TableItem
	{
		public TableItem ()
		{
		}

		public string Heading { get; set;}

		public string SubHeading { get; set; }
	
		public string ImageName { get; set; }

		public string Album { get; set; }

		public bool Singing { get; set; }

		public UITableViewCellStyle CellStyle 
		{
			get { return cellStyle; }
			set { cellStyle = value; }
		}

		protected UITableViewCellStyle cellStyle = UITableViewCellStyle.Default;

		public UITableViewCellAccessory CellAccessory 
		{
			get { return CellAccessory; }
			set { CellAccessory = value; }
		}

		protected UITableViewCellAccessory cellAccessory = UITableViewCellAccessory.None;

		public TableItem (string heading)
		{
			Heading = heading;
		}
	
	}
}

