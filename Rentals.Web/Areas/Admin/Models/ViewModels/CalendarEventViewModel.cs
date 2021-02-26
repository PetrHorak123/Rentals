using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Web.Areas.Admin.Models.ViewModels
{
	/// <summary>
	/// Model, použíívaný pro zobrazení dostupnosti předmětů v měsíčním kalendáři.
	/// </summary>
	public class CalendarEventViewModel
    {
		public CalendarEventViewModel(string title, DateTime from, DateTime to, string url)
		{
			this.Title = title;
			this.Start = from;
			this.End = to;
			this.Url = url;
		}

		public string Title
		{
			get;
			set;
		}

		public DateTime Start
		{
			get;
			set;
		}

		public DateTime End
		{
			get;
			set;
		}

		public string Url
		{
			get;
			set;
		}

		//public string Rendering => "background";
	}
}
