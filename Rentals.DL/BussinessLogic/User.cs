using Rentals.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.RegularExpressions;

namespace Rentals.DL.Entities
{
	public partial class User
	{
		/// <summary>
		/// Vrací všechny nezrošené výpůjčky.
		/// </summary>
		[NotMapped]
		public ICollection<Renting> ActualRentings
		{
			get
			{
				return this.Rentings.Where(r => !r.IsCanceled).ToList();
			}
		}

		/// <summary>
		/// Vrací třídu zákazníka, které je už zformátovaná, aby byla dobře čitelná.
		/// </summary>
		[NotMapped]
		public string ActualClass
		{
			get
			{
				if (this.Class.IsNullOrEmpty())
					return this.Class;

				string classLetter = string.Empty;
				int startingYear = 0;

				var numberRegex = new Regex(@"\d{4}");
				var yearResult = numberRegex.Match(this.Class);

				if (yearResult.Success)
				{
					startingYear = int.Parse(yearResult.Value);
				}

				var classRegex = new Regex(@"[A-Z]");
				var classResult = classRegex.Matches(this.Class);

				var now = DateTime.Now;

				var grade = now.Year - startingYear;

				// Pokud je zaří až prosinec, musím přičíst rok.
				if (now.Month > 8)
				{
					grade++;
				}

				if (classResult.Count == 2)
				{
					return classResult[0].Value + grade.ToString() + classResult[1].Value;
				}
				else if(classResult.Count == 1)
				{
					classLetter = classResult[0].Value;
				}

				return classLetter + grade;
			}
		}
	}
}
