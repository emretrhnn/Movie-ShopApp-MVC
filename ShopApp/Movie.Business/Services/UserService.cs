using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using Movie.Business.Models;
using Movie.DataAccess.Entities;

namespace Movie.Business.Services
{
    //I am implementing this service from IService for both login and register operations for users via
    //AccountService and UserService where the administrator will do crud operations.
    public interface IUserService : IService<UserModel>
	{
		
	}

    public class UserService : IUserService
    {
        //user service will perform operations on the database through the injected user repostory
        private readonly RepoBase<User> _userRepo;

        public UserService(RepoBase<User> userRepo)
        {
            _userRepo = userRepo;
        }

        public Result Add(UserModel model)
        {
            if (_userRepo.Query().Any(u => u.UserName == model.UserName))
                return new ErrorResult("User can't be added because user with the same user name exists!");

            var entity = new User()
            {
                IsActive = model.IsActive,

                UserName = model.UserName,//username is sensitive data so we don't trim it

                Password = model.Password,//password is sensitive data so we don't trim it

                RoleId = model.RoleId,

                Email = model.Email
            };
            _userRepo.Add(entity);

            return new SuccessResult("User added successfully.");
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _userRepo.Dispose();
        }

        public IQueryable<UserModel> Query()
        {
            //first we sort the user's activity in descending order so that we can see the active ones at the top,
            //then we sort their names in ascending order according to their activity
            return _userRepo.Query()
                .OrderByDescending(u => u.IsActive)
                .ThenBy(u => u.UserName)
                .Select(u => new UserModel()
                {
                    Id = u.Id,
                    IsActive = u.IsActive,
                    Password = u.Password,
                    RoleId = u.RoleId,
                    UserName = u.UserName,
                    Role = new RoleModel()
                    {
                        Name = u.Role.Name,
                    }
                });
        }

        public Result Update(UserModel model)
        {
            throw new NotImplementedException();
        }
    }
}

