using System.Collections.Generic;
using System.Linq;
using UIKit;
using Foundation;
using System;

namespace TableView.iOS
{
	public class IndexedTableSource : UITableViewSource 
	{
		string cellIdentifier = "TableCell";

		Dictionary<string, List<string>> indexedTableItems;

		string[] keys;

		/// <summary>
		/// Initialize the Constructor here
		/// </summary>
		/// <param name="items">Items.</param>
		public IndexedTableSource (string[] items)
		{

			indexedTableItems = new Dictionary<string, List<string>>();

			foreach (var t in items) {

				if (indexedTableItems.ContainsKey (t [0].ToString ())) 
				{
					indexedTableItems [t [0].ToString ()].Add (t);
				} 
				else 
				{
						indexedTableItems.Add (t[0].ToString (), new List<string> () { t });
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

		/// <summary> 		/// Sections the index titles. 		/// </summary>  		public override string[] SectionIndexTitles (UITableView tableView)
		{
			return indexedTableItems.Keys.ToArray (); 		}
		/// <summary>
		/// Called when a row is touched
		/// </summary>			
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				new UIAlertView("Row Selected"
								, indexedTableItems[keys[indexPath.Section]][indexPath.Row]
								, null, "OK", null).Show();
				tableView.DeselectRow (indexPath, true);
			}
		
			/// <summary>
			/// Gets the actual UITableViewCell to render for the particular row
			/// </summary>

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
					// request a recycled cell to save memory
					UITableViewCell cell = tableView.DequeueReusableCell (cellIdentifier);
					// if there are no cells to reuse, create a new one
					if (cell == null)
							cell = new UITableViewCell (UITableViewCellStyle.Default, cellIdentifier);
			
					cell.TextLabel.Text = indexedTableItems[keys[indexPath.Section]][indexPath.Row];
			
					return cell;
		}
	}
}