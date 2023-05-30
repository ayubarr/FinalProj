using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.DAL.Repository.Implemintations;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.DAL.SqlServer;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Services.Interfaces;
using FinalProj.Services.Implemintations.RequestServices;
using FinalProj.Services.Implemintations.UserServices;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinalApp.Api
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            #region Base_Repositories 
            services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped(typeof(UserManager<>));
            #endregion
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            #region Base_Services
            services.AddScoped<IBaseRequestService<Request, RequestDTO>, BaseRequestService<Request, RequestDTO>>();
            services.AddScoped<IBaseRequestService<Review, ReviewDTO>, BaseRequestService<Review, ReviewDTO>>();
            #endregion

            #region User_Services
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBaseUserService<TechTeam>, BaseUserService<TechTeam>>();
            services.AddScoped<IBaseUserService<SupportOperator>, BaseUserService<SupportOperator>>();
            services.AddScoped<IBaseUserService<Client>, BaseUserService<Client>>();
            services.AddScoped(typeof(AuthManager<>));
            #endregion

            #region Request_Services
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestHistoryService, RequestHistoryService>();
            services.AddScoped<IReviewService, ReviewService>();
            #endregion
        }

        public static void InitializeIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<Client, IdentityRole>()
                           .AddEntityFrameworkStores<AppDbContext>()
                           .AddDefaultTokenProviders();

            services.AddIdentity<TechTeam, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<SupportOperator, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<RoleManager<IdentityRole>>();

            services.AddScoped<IAuthManager<Client>>(provider =>
            {
                var userManager = provider.GetRequiredService<UserManager<Client>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                return new AuthManager<Client>(userManager, roleManager, configuration);
            });

            services.AddScoped<IAuthManager<SupportOperator>>(provider =>
            {
                var userManager = provider.GetRequiredService<UserManager<SupportOperator>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                return new AuthManager<SupportOperator>(userManager, roleManager, configuration);
            });

            services.AddScoped<IAuthManager<TechTeam>>(provider =>
            {
                var userManager = provider.GetRequiredService<UserManager<TechTeam>>();
                var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
                return new AuthManager<TechTeam>(userManager, roleManager, configuration);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
               .AddJwtBearer(options =>
               {
                   options.SaveToken = true;
                   options.RequireHttpsMetadata = false;
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ClockSkew = TimeSpan.Zero,

                       ValidAudience = configuration["JWT:ValidAudience"],
                       ValidIssuer = configuration["JWT:ValidIssuer"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                   };
               });

        }
    }
}
