using Core.Repositories.EntityFramework.Bases;
using Movie.DataAccess.Contexts;

namespace Movie.DataAccess.Repositories
{
    //Concrete class that inherits from RepoBase abstract class and performs database operations
    public class Repo<TEntity> : RepoBase<TEntity> where TEntity : class, new()
    {
        public Repo(ShopAppContext dbContext) : base(dbContext) 
        {
        }
    }
}

