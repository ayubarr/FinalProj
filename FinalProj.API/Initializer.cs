using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.DAL.Repository.Implemintations;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.DAL.SqlServer;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Domain.Models.Enums;
using FinalApp.Services.Interfaces;
using FinalProj.API.Logs;
using FinalProj.Services.Implemintations.RequestServices;
using FinalProj.Services.Implemintations.UserServices;
using FinalProj.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinalApp.Api
{
    public static class Initializer
    {
        public static IServiceCollection InitializeRepositories(this IServiceCollection services)
        {
            #region Base_Repositories 
            services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
            services.AddScoped(typeof(UserManager<>));
            #endregion
            return services;
        }

        public static IServiceCollection InitializeServices(this IServiceCollection services)
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

            return services;

        }

        public static IServiceCollection InitializeIdentity(this IServiceCollection services, IConfiguration configuration)
        {
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
            }).AddJwtBearer(options =>
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

            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<AppDbContext>()
                 .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<Client>, UserStore<Client, IdentityRole, AppDbContext, string>>();
            services.AddScoped<IUserStore<SupportOperator>, UserStore<SupportOperator, IdentityRole, AppDbContext, string>>();
            services.AddScoped<IUserStore<TechTeam>, UserStore<TechTeam, IdentityRole, AppDbContext, string>>();


            services.AddScoped<IPasswordHasher<Client>, PasswordHasher<Client>>();
            services.AddScoped<IPasswordHasher<SupportOperator>, PasswordHasher<SupportOperator>>();
            services.AddScoped<IPasswordHasher<TechTeam>, PasswordHasher<TechTeam>>();

            //services.AddIdentity<Client, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();


            //services.AddIdentity<TechTeam, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();

            //services.AddIdentity<SupportOperator, IdentityRole>()
            //    .AddEntityFrameworkStores<AppDbContext>()
            //    .AddDefaultTokenProviders();

            return services;
        }

        public static async Task InitializeRoles(this IServiceCollection services)
        {
            var roleManager = services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole>>();
            await SeedRoles(roleManager);
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Enum.GetValues(typeof(Roles)).Cast<Roles>())
            {
                var roleName = role.ToString();

                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public async static Task SeedAdmins(this IServiceCollection services)
        {
            var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<SupportOperator>>();
            const string adminId = "1fd2c92f-1722-4b69-87db-f877b2656307";
            const string adminName = "Ayub";

            var user = await userManager.FindByIdAsync(adminId);

            if (user != null) return;

            var admin = new SupportOperator()
            {
                Id = adminId,
                Name = "Admin",
                Surname = "Admin",
                Login = "Ayub",
                Phone = "+996-999-999-999",
                Email = "admin@admin.com",
                Password = "P@ssw0rd!",
                UserName = adminName,
                NormalizedUserName = "Ayub".Normalize(),
                NormalizedEmail = "admin@admin.com".Normalize(),
            };

            var passwordHasher = new PasswordHasher<User>();
            admin.PasswordHash = passwordHasher.HashPassword(admin, "P@ssw0rd!");

      
           

            await userManager.CreateAsync(admin);
            await userManager.AddToRoleAsync(admin, Roles.Administrator.ToString());
        }


        public static void IntialiseLogger(this ILoggingBuilder loggingBuilder, Action<DbLoggerOptions> configure)
        {
            //loggingBuilder.Services.AddSingleton<ILoggerProvider, DbLoggerProvider>();
            //loggingBuilder.Services.Configure(configure);
        }
    }
}
