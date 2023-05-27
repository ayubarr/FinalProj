using FinalApp.DAL.Repository.Implemintations;
using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Services.Interfaces;

namespace FinalApp.Api
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            #region Base_Repositories 
            services.AddScoped(typeof(IBaseAsyncRepository<>), typeof(BaseAsyncRepository<>));
            #endregion
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            #region Base_Services
            services.AddScoped<IBaseRequestService<Client, ClientDTO>, BaseRequestService<Client, ClientDTO>>();
            services.AddScoped<IBaseRequestService<Request, RequestDTO>, BaseRequestService<Request, RequestDTO>>();
            services.AddScoped<IBaseRequestService<TechTeam, TechTeamDTO>, BaseRequestService<TechTeam, TechTeamDTO>>();
            services.AddScoped<IBaseRequestService<SupportOperator, SupportOperatorDTO>, BaseRequestService<SupportOperator, SupportOperatorDTO>>();
            services.AddScoped<IBaseRequestService<Review, ReviewDTO>, BaseRequestService<Review, ReviewDTO>>();
            #endregion

            #region User_Services
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBaseUserService<TechTeam>, BaseUserService<TechTeam>>();
            services.AddScoped<IBaseUserService<SupportOperator>, BaseUserService<SupportOperator>>();
            #endregion

            #region Request_Services
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestHistoryService, RequestHistoryService>();
            services.AddScoped<IReviewService, ReviewService>();
            #endregion

            #region Authorization_And_Authentication_Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IAuthManager<>), typeof(AuthManager<>));

            #endregion

            #region AutoMapper
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new MappingProfile());
            });
            #endregion
        }

    }
}
