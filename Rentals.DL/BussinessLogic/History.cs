namespace Rentals.DL.Entities
{
	public partial class History
	{
		public static History CreateEntity(string content, int rentingId, int itemId)
		{
			var history = new History()
			{
				Content = content,
				RentingId = rentingId,
				ItemId = itemId,
			};

			return history;
		}
	}
}
