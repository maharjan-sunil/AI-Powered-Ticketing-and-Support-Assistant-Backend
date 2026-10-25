using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Service;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Service;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Service;
using AI_Powered_Ticketing_and_Support_System_Backend.Middleware;

namespace DockerDemo.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            // 1. Core .NET engine required for services to inspect incoming HTTP/Token data
            services.AddHttpContextAccessor();

            // 2. Register your current user context resolver
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;

        }
    }
}
