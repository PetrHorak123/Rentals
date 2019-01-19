using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

public class DomainRequirementHandler : AuthorizationHandler<DomainRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
		DomainRequirement requirement)
	{
		foreach (var domain in requirement.Domain)
		{
			if (context.Resource != null && ((string)context.Resource).EndsWith($"@{domain}"))
			{
				context.Succeed(requirement);
			}
		}

		return Task.FromResult(0);
	}
}