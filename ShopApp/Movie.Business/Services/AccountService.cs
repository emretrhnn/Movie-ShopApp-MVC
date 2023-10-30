using Core.Results;
using Core.Results.Bases;
using Movie.Business.Models;
using Movie.Business.Models.Account;
using Movie.DataAccess.Enums;

namespace Movie.Business.Services
{
    //We don't inherit from IServis because this service will do its work through userservis injection
    public interface IAccountService
    {
        Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel);//for user login for users
        Result Register(AccountRegisterModel accountRegisterModel);//for new user registration of users
    }

    public class AccountService : IAccountService
    {
        private readonly IUserService _userService;//I inject the userservis object that I will do crud operations to this service

        public AccountService(IUserService userService)
        {
            _userService = userService;
        }

        //user login
        public Result Login(AccountLoginModel accountLoginModel, UserModel userResultModel)
        {
            //I pull the data from the active user query with the username and password entered by the user via accountloginmodel and assign it to the user
            var user = _userService.Query().SingleOrDefault(u => u.UserName == accountLoginModel.UserName && u.Password == accountLoginModel.Password && u.IsActive);

            if (user is null)//if there are no users
                return new ErrorResult("Invalid user name or password!");

            //If the user is found, we fill the userresultmodel according to the user
            userResultModel.UserName = user.UserName;
            userResultModel.Role.Name = user.Role.Name;
            userResultModel.Id = user.Id;//for use in cart operations
            userResultModel.Email = user.Email;

            return new SuccessResult();
        }

        //new user registration
        public Result Register(AccountRegisterModel accountRegisterModel)
        {
            var user = new UserModel()
            {
                IsActive = true,
                Password = accountRegisterModel.Password,
                UserName = accountRegisterModel.UserName,
                RoleId = (int)Roles.User,
            };
            return _userService.Add(user);
        }
    }
}