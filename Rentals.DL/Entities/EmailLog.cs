using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rentals.Common.Enums;

namespace Rentals.DL.Entities
{
	/// <summary>
	/// Představuje tabulku, kde se logují všechny emaily.
	/// </summary>
	[Table("EmailLog")]
	public class EmailLog
	{
		[Key]
		public int Id
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje, komu se email poslal.
		/// </summary>
		[Required]
		public string To
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje, kdo email posílal.
		/// </summary>
		[Required]
		public string From
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuju typ emailu.
		/// </summary>
		[Required]
		public EmailType Type
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje předmět emailu.
		/// </summary>
		[Required]
		public string Subject
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje text emailu.
		/// </summary>
		[Required]
		public string Content
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje k jaké výpůjčce se email vztahuje.
		/// POZOR: není provázaný!!!
		/// </summary>
		[Required]
		public int RentingId
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací, nebo nastavuje, zda byl email odeslán.
		/// </summary>
		public bool Sent
		{
			get;
			set;
		}

		/// <summary>
		/// Vrací nebo nastavuje error, který mohl nastat při odesílání.
		/// </summary>
		public string Error
		{
			get;
			set;
		}
	}
}