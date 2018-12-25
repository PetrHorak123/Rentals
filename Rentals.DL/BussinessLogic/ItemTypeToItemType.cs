namespace Rentals.DL.Entities
{
	public partial class ItemTypeToItemType
	{
		/// <summary>
		/// Doporučený způsob pro vatváření příslušenství.
		/// </summary>
		/// <remarks>
		/// Donutí přemýšlet co přesně je co a je menší šance ztracení se při vytváření,
		/// lepší přidávat takto přímo to tabulky, EF to díky navigation props. sám prováže.
		/// </remarks>
		public static ItemTypeToItemType Create(int accessoryToId, int accessoryId)
		{
			var itemTypeToItemType = new ItemTypeToItemType()
			{
				AccesoryId = accessoryId,
				AccesoryToId = accessoryToId,
			};

			return itemTypeToItemType;
		}
	}
}
