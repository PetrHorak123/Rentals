using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

public class DomainRequirementHandler : AuthorizationHandler<DomainRequirement>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
		DomainRequirement requirement)
	{
		if (context.Resource != null && ((string)context.Resource).EndsWith($"@{requirement.Domain}"))
		{
			context.Succeed(requirement);
		}

		return Task.FromResult(0);
	}
}