using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic; 
using Parse; 
using ikaraoke;

namespace TableView.iOS
{
	partial class ListController : UITableViewController
	{
		public ListController (IntPtr handle) : base (handle)
		{
		}
			
		public async override void ViewDidLoad ()
		{
				base.ViewDidLoad ();

		// change the Table View Style from "Plain" to "Grouped"
		table = new UITableView(View.Bounds, UITableViewStyle.Grouped); // change
		table.AutoresizingMask = UIViewAutoresizing.All;

		List<TableItem> tableItems = new List<TableItem>();

		// build a query to get a list of records from the SingSong class in Parse
		// and sort the results by the SongName column
			var query = from singingSong in ParseObject.GetQuery("SingSong")

				orderby singingSong["SongName"] ascending
				select singingSong;
		
		// make an asynchronous call to Parse to get the contents of the query above
		IEnumerable<ParseObject> singSongListResult = await query.FindAsync();

		// if the returned list from Parse is not empty
			if (singSongListResult != null)
		{
				// loop through the results and set the object properties
				foreach (var singingSongItem in singSongListResult)
				{
					var songItem = new SingingSong ()
					{
						SongName = singingSongItem.Get<string> ("SongName"), 
						SingerName = singingSongItem.Get<string> ("SingerName"), 
						SingerIcon = singingSongItem.Get<string> ("SingerIcon"),
						Album = singingSongItem.Get<string> ("Album"), 
						Singing = singingSongItem.Get<bool> ("Singing")
				} ;

				// assign the retrieved properties to the TableItem’s properties
				tableItems.Add 
				(
					new TableItem(songItem.SongName) 
					{
						SubHeading=songItem.SingerName, 
						ImageName= songItem.SingerIcon,
						Singing = songItem.Singing,
						Album = songItem.Album

					}
				);

				}
		}
			// set the table view’s source to that of the new ImageTableSource 
			table.Source = new ImageTableSource (tableItems); 

			Add (table);
		}
	}
}

