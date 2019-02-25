using Newtonsoft.Json;
using Rentals.DL.Entities;
using System.Collections.Generic;

namespace Rentals.Web.Models.Email
{
	/// <summary>
	/// Reprezentuje zprávu, která se pošle jako email.
	/// </summary>
	/// <remarks>Mimikuje objektouvou strukture api od mirosoftu.</remarks>
	public class Message
	{
		private Message()
		{
			this.Body = new Body();
			this.ToRecipients = new List<ToRecipient>();
		}

		/// <summary>
		/// K jaké výpůjčce se vztahuje.
		/// </summary>
		[JsonIgnore]
		public int RentingId
		{
			get;
			set;
		}

		/// <summary>
		/// Předmět emailu.
		/// </summary>
		[JsonProperty("subject")]
		public string Subject
		{
			get;
			set;
		}

		/// <summary>
		/// Tělo emailu.
		/// </summary>
		[JsonProperty("body")]
		public Body Body
		{
			get;
			set;
		}

		/// <summary>
		/// Příjemci emailu.
		/// </summary>
		[JsonProperty("toRecipients")]
		public List<ToRecipient> ToRecipients
		{
			get;
			set;
		}

		public static Message CreateMessage(Renting renting, string subject, string content)
		{
			var message = new Message()
			{
				RentingId = renting.Id,
				Subject = subject
			};

			message.Body.Content = content;
			message.ToRecipients.Add(new ToRecipient()
			{
				EmailAddress = new EmailAddress()
				{
					Address = renting.User.Email
				}
			});

			return message;
		}
	}
}