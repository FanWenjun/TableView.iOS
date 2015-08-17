using System.IO;
using System.Collections.Generic;
using System.Linq;
using UIKit;
using Foundation;
using System;

namespace TableView.iOS
{
	public class ImageTableSource : UITableViewSource 
	{
			protected string cellIdentifier	= "TableCell";

			Dictionary<string, List<TableItem>> indexedTableItems;
			string[] keys;

			public ImageTableSource (List<TableItem> items)
			{
					indexedTableItems = new Dictionary<string, List<TableItem>>();
					foreach (var t in items) {
							if (indexedTableItems.ContainsKey (t.SubHeading)) {
									indexedTableItems [t.SubHeading].Add (t);
			             	}   else {
									indexedTableItems.Add (t.SubHeading, new List<TableItem>() {t});
							}
					}
					keys = indexedTableItems.Keys.ToArray ();
			}

			/// <summary>
			/// Called by the TableView to determine how many sections(groups) there are.
			/// </summary>
			public override nint NumberOfSections (UITableView tableView)
			{
			return keys.Length; 
			}

			/// <summary>
			/// Determines how many cells to create for that particular section.
			/// </summary>
			public override nint RowsInSection (UITableView tableview, nint section)
			{
					return indexedTableItems[keys[section]].Count;
			}

			/// <summary>
			/// The string value to show in the section header
			/// </summary>
			public override string TitleForHeader (UITableView tableView, nint section)
			{
			return keys [section];
			}

			/// <summary>
			/// The string to show in the section footer
			/// </summary>
			public override string TitleForFooter (UITableView tableView, nint section)
			{
					return null; // turn off the footer by returning a null value
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
			new UIAlertView("Row Selected"
				, indexedTableItems[keys[indexPath.Section]][indexPath.Row].Heading
				, null, "OK", null).Show();
			tableView.DeselectRow (indexPath, true);
		}

		/// <summary>
		/// Gets the actual UITableViewCell to render for the particular section and row
		/// </summary>
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			//---- declare vars
			UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
			TableItem item = indexedTableItems[keys[indexPath.Section]][indexPath.Row];

			if (cell == null)
			{
				// use a Subtitle cell style here
				cell = new UITableViewCell (UITableViewCellStyle.Subtitle, cellIdentifier); 
			}

			//---- set the item text, subtitle and image/icon
			cell.TextLabel.Text = item.Heading;
			cell.DetailTextLabel.Text = item.Album;
			cell.ImageView.Image = UIImage.FromFile("Images/" + item.ImageName); 

			// if the item is marked as a favorite, use the CheckMark cell accessory
			// otherwise (i.e. when false) use the disclosure cell accessory
			if (item.Singing) {
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			} else {
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}
			return cell;
		}

	}
}

