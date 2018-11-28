using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rentals.DL.Entities
{
	public partial class Rental
	{
		/// <summary>
		/// Vrací možné začátky výpůjčky.
		/// </summary>
		[NotMapped]
		public List<string> TimeList
		{
			get
			{
				var timeList = new List<string>();

				TimeSpan ts = this.StartsAt;
				TimeSpan tsMinTimeUnit = TimeSpan.FromMinutes(MinTimeUnit);
				TimeSpan tsLast = this.EndsAt;

				while (ts < tsLast)
				{
					var timeValue = ts.ToString("hh':'mm");
					timeList.Add(timeValue);
					ts += tsMinTimeUnit;
				}

				return timeList;
			}
		}

		/// <summary>
		/// Vrací, zda je v daný čas půjčovna otevřená.
		/// </summary>
		public bool IsInWorkingHours(DateTime time)
		{
			bool result = time.TimeOfDay >= this.StartsAt &&
							time.TimeOfDay < this.EndsAt &&
							(time.DayOfWeek != DayOfWeek.Saturday ||
							time.DayOfWeek != DayOfWeek.Sunday);

			return result;
		}
	}
}
