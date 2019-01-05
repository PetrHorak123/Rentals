using System;

namespace Rentals.Web.Models
{
	/// <summary>
	/// Model, použíívaný pro zobrazení dostupnosti předmětů.
	/// </summary>
	public class AvaibilityViewModel
	{
		public AvaibilityViewModel(DateTime from, DateTime to)
		{
			this.Start = from;
			this.End = to;
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

		public string Rendering => "background";
	}
}
