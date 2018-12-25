namespace Rentals.DL.Entities
{
	/// <summary>
	/// Tabulka provazující typ itemu s jiným (dá se představit jako příslušenství).
	/// </summary>
	public partial class ItemTypeToItemType
	{
		public int AccesoryToId { get; set; }
		public int AccesoryId { get; set; }

		public virtual ItemType AccesoryTo { get; set; }
		public virtual ItemType Accesory { get; set; }
	}
}
