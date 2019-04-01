using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Mustache;
using Rentals.DL.Entities;
using Rentals.DL.Interfaces;
using Rentals.Web.Interfaces;
using Rentals.Web.Models.Email;
using Newtonsoft.Json;
using System.Net;
using Rentals.Common.Enums;
using Microsoft.AspNetCore.Http;
using Rentals.DL;
using Rentals.Common.Extensions;

namespace Rentals.Web.Code
{
	public class EmailSender : IEmailSender
	{
		private readonly IRepositoriesFactory factory;
		private readonly FormatCompiler compiler;
		private readonly User user;
		private readonly EntitiesContext context;

		public EmailSender(IRepositoriesFactory factory, IHttpContextAccessor accessor, EntitiesContext context)
		{
			this.compiler = new FormatCompiler
			{
				RemoveNewLines = false
			};

			this.context = context;
			this.factory = factory;
			this.user = factory.Users.GetByName(accessor.HttpContext.User.Identity.Name);
		}

		#region Specific mails

		public Task<bool> SendRentingCreated(Renting renting, string token, string url)
		{
			var content = this.Process(File.ReadAllText($"wwwroot/emailTemplates/{EmailType.RentingNew.ToString()}.txt", Encoding.UTF8), new { Renting = renting, Url = url });

			return SendMail(
				Message.CreateMessage(renting, EmailType.RentingNew.ToLocalizedEnum(typeof(EmailType)), content),
				token, EmailType.RentingNew
			);
		}

		public Task<bool> SendRentingEdited(Renting renting, string token)
		{
			var content = this.Process(File.ReadAllText($"wwwroot/emailTemplates/{EmailType.RentingEdit.ToString()}.txt", Encoding.UTF8), new { Renting = renting });

			return SendMail(
				Message.CreateMessage(renting, EmailType.RentingEdit.ToLocalizedEnum(typeof(EmailType)), content),
				token, EmailType.RentingEdit
			);
		}

		public Task<bool> SendRentingCanceled(Renting renting, User canceledBy, string token)
		{
			var content = this.Process(File.ReadAllText($"wwwroot/emailTemplates/{EmailType.RentingCalcelation.ToString()}.txt", Encoding.UTF8), new { Renting = renting, User = canceledBy });

			return SendMail(
				Message.CreateMessage(renting, EmailType.RentingCalcelation.ToLocalizedEnum(typeof(EmailType)), content),
				token, EmailType.RentingCalcelation
			);
		}

		public async Task<bool> SendRentingReminder(string token)
		{
			var rentings = await this.factory.Rentings.GetRentingsEndingToday();

			if (rentings.Length == 0)
				return true;

			var allsent = true;

			// Nechám projet všechny, aby bylo lognuté, že se někdo pokusil o poslání.
			foreach (var renting in rentings)
			{
				var content = this.Process(File.ReadAllText($"wwwroot/emailTemplates/{EmailType.RentingEndsReminder.ToString()}.txt", Encoding.UTF8), new { Renting = renting });

				var result = await SendMail(
					Message.CreateMessage(renting, EmailType.RentingEndsReminder.ToLocalizedEnum(typeof(EmailType)), content),
					token, EmailType.RentingEndsReminder
				);

				if (result)
				{
					renting.NotificationSent = true;
					this.factory.SaveChanges();
				}
				else
				{
					allsent = false;
				}
			}

			// Všechyn emaily byly odelány úspěšně.
			return allsent;
		}

		public async Task<bool> SendRentingNotReturned(string token)
		{
			var rentings = await this.factory.Rentings.GetNonRetruned();

			if (rentings.Length == 0)
				return true;

			var allsent = true;

			// Nechám projet všechny, aby bylo lognuté, že se někdo pokusil o poslání.
			foreach (var renting in rentings)
			{
				var content = this.Process(File.ReadAllText($"wwwroot/emailTemplates/{EmailType.RentingNotReturned.ToString()}.txt", Encoding.UTF8), new { Renting = renting });

				var result = await SendMail(
					Message.CreateMessage(renting, EmailType.RentingNotReturned.ToLocalizedEnum(typeof(EmailType)), content),
					token, EmailType.RentingNotReturned
				);

				if (!result)
				{
					allsent = false;
				}
			}

			// Všechyn emaily byly odelány úspěšně.
			return allsent;
		}

		#endregion

		public async Task<bool> SendMail(Message email, string token, EmailType type)
		{
			using (var client = new HttpClient())
			{
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
				string json = JsonConvert.SerializeObject(new { message = email });

				var response = await client.PostAsync(
					$"https://graph.microsoft.com/v1.0/me/sendMail",
					new StringContent(json, Encoding.UTF8, "application/json")
				);

				var result = response.StatusCode == HttpStatusCode.Accepted;

				// Zaznamenám email, ať už se poslání pomohlo či ne.
				var log = new EmailLog()
				{
					To = email.ToRecipients[0].EmailAddress.Address,
					From = this.user.Email ?? this.user.UserName,
					Type = type,
					Subject = email.Subject,
					Content = email.Body.Content,
					RentingId = email.RentingId,
					Sent = result,
					Error = result ? null : response.ReasonPhrase,
				};

				context.EmailLog.Add(log);
				context.SaveChanges();

				return result;
			}
		}

		#region Helpers

		private string Process(string template, object context)
		{
			Generator generator = this.compiler.Compile(template);
			var result = generator.Render(context);

			return result;
		}

		#endregion
	}
}
