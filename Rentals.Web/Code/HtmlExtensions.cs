using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rentals.Web.Code
{
	public static class HtmlExtensions
	{
		public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(this IHtmlHelper helper, TEnum except) where TEnum : struct
		{
			var @enum = (object)except;
			var selectedList = helper.GetEnumSelectList<TEnum>();
			selectedList = selectedList.Where(l => l.Value != ((int)@enum).ToString());

			return selectedList;
		}

		public static SelectList WorkingHourSelectList(this IHtmlHelper html, IEnumerable<TimeSpan> hours, TimeSpan selectedHour)
		{
			var hoursList = hours.Select(hour => new
			{
				Id = hour,
				Name = hour.ToString("hh':'mm")
			});

			return new SelectList(hoursList, "Id", "Name", selectedHour);
		}

	}
}
