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
		public CalendarEventViewModel(string title, string desc, DateTime from, DateTime to, string url, string color)
		{
			this.Title = title;
			this.Description = desc;
			this.Start = from;
			this.End = to;
			this.Url = url;
			this.Color = color;
		}

		public string Title
		{
			get;
			set;
		}

		public string Description
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
		public string Color
		{
			get;
			set;
		}		
	}
}
