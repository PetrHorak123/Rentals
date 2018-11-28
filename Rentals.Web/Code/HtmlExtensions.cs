using Microsoft.AspNetCore.Mvc.Rendering;
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
	}
}
