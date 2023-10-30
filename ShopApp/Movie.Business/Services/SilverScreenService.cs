
using Core.Repositories.EntityFramework.Bases;
using Core.Results;
using Core.Results.Bases;
using Core.Services.Bases;
using Microsoft.EntityFrameworkCore;
using Movie.Business.Models;
using Movie.DataAccess.Entities;

namespace Movie.Business.Services
{
    //interface created for crud operations
    public interface ISilverScreenService : IService<SilverScreenModel>
	{
        List<SilverScreen> GetAll();
	}

    //concrete class to be used by injecting new to the related controllers via constructor
    public class SilverScreenService : ISilverScreenService
    {
        private readonly RepoBase<SilverScreen> _silverScreenRepo;
        private readonly RepoBase<SilverScreenCategory> _silverScreenCategoryRepo;

        public SilverScreenService(RepoBase<SilverScreen> silverScreenRepo, RepoBase<SilverScreenCategory> silverScreenCategoryRepo)
        {
            //I define the repository for crud operations and assign the object to be injected via constructor with IoC Container to this repository
            _silverScreenRepo = silverScreenRepo;
            _silverScreenCategoryRepo = silverScreenCategoryRepo;
        }

        //create operation: the model is the object that the user fills and sends through the view
        public Result Add(SilverScreenModel model)
        {
            if (Query().Any(s => s.Name.ToLower() == model.Name.ToLower().Trim()))
                return new ErrorResult("Movie can't be added because product with the same name exists!");

            SilverScreen screenEntity = new SilverScreen()
            {
                Name = model.Name.Trim(),
                Description = model.Description?.Trim(),
                Price = model.Price,
                Actors = model.Actors?.Trim(),
                Director = model.Director?.Trim(),
                Year = model.Year?.Trim(),
                ImageUrl = model.ImageUrl,
                SilverScreenCategories = model.CategoryIds?.Select(categoryId => new SilverScreenCategory
                {
                    CategoryId = categoryId
                }).ToList()
            };

            _silverScreenRepo.Add(screenEntity);

             return new SuccessResult("Movie added successfully.");
        }

        //Delete operation
        public Result Delete(int id)
        {
            //first delete the product's associated category record from the repository
            _silverScreenCategoryRepo.Delete(sc => sc.CategoryId == id);

            _silverScreenRepo.Delete(m => m.Id == id);

            return new SuccessResult("Movie deleted successfully.");
        }

        //when we are done with this service, the object we specified in the IoC Container will be disposed through the lifecycle
        public void Dispose()
        {
            _silverScreenRepo.Dispose();
        }

        public List<SilverScreen> GetAll()
        {
            return _silverScreenRepo.GetAll();
        }

        //Read operation: I create our database query by converting the data we receive with the entity from the repository into a model
        public IQueryable<SilverScreenModel> Query()
        {
            IQueryable<SilverScreenModel> query = _silverScreenRepo.Query().Select(s => new SilverScreenModel()
            {
                //mapping process
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                Actors = s.Actors,
                Director = s.Director,
                Year = s.Year,
                ImageUrl = s.ImageUrl,
                CategoryNameDisplay = string.Join(" || ", s.SilverScreenCategories.Select(sc => sc.Category.Name)),
                CategoryIds = s.SilverScreenCategories.Select(sc => sc.CategoryId).ToList()
            });

            query = query.OrderBy(s => s.Name);
            return query;
        }

        //Update operation: the model is the object that the user fills and sends through the view
        public Result Update(SilverScreenModel model)
        {
            if(_silverScreenRepo.Query().Any(m => m.Id != model.Id && m.Name.ToLower().Trim() == model.Name.ToLower().Trim()))
            {
                return new ErrorResult("Movie with the same name exists!");
            }

            var entity = _silverScreenRepo.Query().Include(b => b.SilverScreenCategories).SingleOrDefault(m => m.Id == model.Id);
            var categoryIds = entity.SilverScreenCategories.Select(sc => sc.SilverScreenId);

            //first delete the product's associated category record from the repository
            _silverScreenCategoryRepo.Delete(sc => sc.SilverScreenId == model.Id);

            //I update the properties of the product pulled from the database over the properties in the model
            entity.Name = model.Name.Trim();
            entity.Description = model.Description.Trim();
            entity.Actors = model.Actors.Trim();
            entity.Director = model.Director.Trim();
            entity.Price = model.Price;
            entity.ImageUrl = model.ImageUrl;
            entity.SilverScreenCategories = model.CategoryIds?.Select(categoryId => new SilverScreenCategory()
            {
                CategoryId = categoryId
            }).ToList();

            _silverScreenRepo.Update(entity);

            return new SuccessResult("Movie updated successfully.");

        }
    }
}

