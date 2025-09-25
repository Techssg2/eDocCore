using eDocCore.Application.Interfaces;
using eDocCore.Domain.Interfaces;
using eDocCore.Infrastructure.Persistence;
using eDocCore.Infrastructure.Persistence.Repositories;
using eDocCore.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDocCore.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Cấu hình DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Đăng ký Repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Đăng ký các dịch vụ khác
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }

}
