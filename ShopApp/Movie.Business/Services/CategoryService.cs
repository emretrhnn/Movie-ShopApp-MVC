using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using Movie.Business.Models;
using Movie.DataAccess.Entities;

namespace Movie.Business.Services
{
    public interface ICategoryService : IService<CategoryModel>
    {
        List<CategoryModel> GetList();
        CategoryModel GetItem(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly RepoBase<Category> _categoryRepo;

        public CategoryService(RepoBase<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        public Result Add(CategoryModel model)
        {
            throw new NotImplementedException();
        }

        public Result Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _categoryRepo.Dispose();
        }

        public CategoryModel GetItem(int id)
        {
            var model = Query().Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name,

            }).SingleOrDefault(c => c.Id == id);

            return model;
        }

        public List<CategoryModel> GetList()
        {
            return Query().OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Id = c.Id,
                Name = c.Name,

            }).ToList();
        }

        public IQueryable<CategoryModel> Query()
        {
            //I create the query with the query method and sort ascending by name with the OrderBy LINQ method
            return _categoryRepo.Query().OrderBy(c => c.Name).Select(c => new CategoryModel()
            {
                Name = c.Name,
                Id = c.Id,
            });
        }

        public Result Update(CategoryModel model)
        {
            throw new NotImplementedException();
        }
    }
}

