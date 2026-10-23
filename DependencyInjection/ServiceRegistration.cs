using Microsoft.Extensions.DependencyInjection;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.TicketService.Service;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CategoryService.Service;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Interface;
using AI_Powered_Ticketing_and_Support_System.Feature.CommentService.Service;

namespace DockerDemo.DependencyInjection
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;

        }
    }
}
