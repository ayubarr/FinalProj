using FinalApp.ApiModels.DTOs.EntitiesDTOs.RequestsDTO;
using FinalApp.ApiModels.DTOs.EntitiesDTOs.UsersDTOs;
using FinalApp.DAL.Repository.Implemintations;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.DAL.SqlServer;
using FinalApp.Domain.Models.Entities.Persons.Users;
using FinalApp.Domain.Models.Entities.Requests.RequestsInfo;
using FinalApp.Services.Interfaces;
using FinalProj.Services.Implemintations.RequestServices;
using FinalProj.Services.Implemintations.UserServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

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

            #endregion

            #region Request_Services
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestHistoryService, RequestHistoryService>();
            services.AddScoped<IReviewService, ReviewService>();
            #endregion

            #region Identity_Services
            services.AddIdentity<Client, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<TechTeam, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddIdentity<SupportOperator, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<RoleManager<IdentityRole>>();
            #endregion
        }

    }
}
