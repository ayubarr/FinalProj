using FinalApp.DAL.Repository.Interfaces;
using FinalApp.Domain.Models.Abstractions.BaseUsers;
using FinalApp.Services.Helpers;
using FinalApp.Services.Interfaces;
using FinallApp.ValidationHelper;
using Microsoft.EntityFrameworkCore;

namespace FinalApp.Services.Implemintations
{
    //public class AuthManager<T> : IAuthManager<T>
    //    where T : User
    //{
    //    private readonly IBaseAsyncRepository<T> _repository;

    //    public AuthManager(IBaseAsyncRepository<T> repository)
    //    {
    //        _repository = repository;
    //    }

    //    public async Task<T> FindByLoginAsync(string login)
    //    {
    //        try
    //        {
    //            StringValidator.CheckIsNotNull(login);
    //            var user = await _repository.ReadAll().FirstOrDefaultAsync(user => user.Login == login);

    //            ObjectValidator<T>.CheckIsNotNullObject(user);
    //            return user;
    //        }
    //        catch (ArgumentNullException argNullException)
    //        {
    //            throw new ArgumentException("no records found in the database.\n\r" +
    //                $"Error: {argNullException}");
    //        }
    //        catch (Exception exception)
    //        {
    //            throw new Exception(" internal server error.\n\r" +
    //                $"Error: {exception}");
    //        }
    //    }

    //    public async Task<bool> CheckPasswordAsync(T user, string password)
    //    {
    //        try
    //        {
    //            ObjectValidator<T>.CheckIsNotNullObject(user);
    //            StringValidator.CheckIsNotNull(password);

    //            var hashedPassword = HashHelper.HashPassword(password);

    //            return user.Password == hashedPassword;
    //        }
    //        catch (ArgumentNullException argNullException)
    //        {
    //            return false;
    //        }
    //        catch (Exception exception)
    //        {
    //            return false;
    //        }

    //    }
    //}
}
