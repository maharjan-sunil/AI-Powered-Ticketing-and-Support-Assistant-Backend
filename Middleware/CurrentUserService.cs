namespace AI_Powered_Ticketing_and_Support_System_Backend.Middleware
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
    }

    /*
     * to retrieve the logged in UserID
     * */
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;
                var oidClaim = user?.FindFirst("http://microsoft.com")?.Value
                               ?? user?.FindFirst("oid")?.Value;

                if (Guid.TryParse(oidClaim, out Guid parsedGuid))
                {
                    return parsedGuid;
                }

                //for now add dummy user id
                var userId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");
                return userId;
                //return Guid.Empty;
            }
        }

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }
}
