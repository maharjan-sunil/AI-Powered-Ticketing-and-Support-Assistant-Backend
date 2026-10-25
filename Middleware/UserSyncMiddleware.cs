using AI_Powered_Ticketing_and_Support_System.Data;
using AI_Powered_Ticketing_and_Support_System.Feature.UserService.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AI_Powered_Ticketing_and_Support_System_Backend.Middleware
{
    public class UserSyncMiddleware
    {
        private readonly RequestDelegate _next;

        public UserSyncMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Only attempt to sync if the user has been authenticated by MSAL/JWT
            if (context.User.Identity?.IsAuthenticated == true)
            {
                // Extract the unique Entra ID Guid from the token claims
                var oidClaim = context.User.FindFirst("http://microsoft.com")?.Value
                               ?? context.User.FindFirst("oid")?.Value;

                if (Guid.TryParse(oidClaim, out Guid userGuid))
                {
                    // Since middleware is a singleton/scoped lifecycle bridge, we safely resolve DbContext per request
                    var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>();

                    var email = context.User.FindFirst(ClaimTypes.Upn)?.Value
                                ?? context.User.FindFirst(ClaimTypes.Email)?.Value
                                ?? "unknown@domain.com";

                    var name = context.User.Identity?.Name ?? "Entra ID User";

                    // Check if they are already registered in our DB
                    var localUser = await dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == userGuid);

                    if (localUser == null)
                    {
                        // First time visit -> Create user row
                        localUser = new User
                        {
                            Id = userGuid,
                            Email = email,
                            DisplayName = name,
                            CreatedAt = DateTime.UtcNow
                        };
                        dbContext.Set<User>().Add(localUser);
                    }
                    else
                    {
                        // Returning user -> Update metadata safely without duplicating
                        localUser.DisplayName = name;
                        localUser.Email = email;
                        localUser.CreatedAt = DateTime.UtcNow;
                        dbContext.Set<User>().Update(localUser);
                    }

                    await dbContext.SaveChangesAsync();
                }
            }

            await _next(context);
        }
    }
}
