using Core.Results.Bases;

namespace Core.Services.Bases
{
    /// EN: An interface, which can use any type through TModel as an instantiable reference type, for all database service classes 
    /// that allow interactions with the user through models and perform transformations between models and database related entities. 
    /// It also provides the ability to perform data formatting and validation processes.
    public interface IService<TModel> : IDisposable where TModel : class, new()
    {
        /// EN: A query method definition that, through the injected repository retrieves records of the entity type and maps them to a model.
        IQueryable<TModel> Query();

        /// EN: A method definition that, after performing validations, data formatting, and other necessary operations based on 
        /// the provided model as a parameter, transforms it into an entity and performs an insertion operation into the database 
        /// through the injected repository. The method returns the result as either successful or unsuccessful operation.
        Result Add(TModel model);

        /// EN: A method definition that, after performing validations, data formatting, and other necessary operations based on 
        /// the provided model as a parameter, transforms it into an entity and performs an update operation into the database 
        /// through the injected repository. The method returns the result as either successful or unsuccessful operation.
        Result Update(TModel model);

        /// EN: A method definition that takes an id parameter based on the primary key value provided, performs a deletion operation 
        /// in the database through the injected repository. The method returns the result as either a successful or unsuccessful operation.
        Result Delete(int id);
    }
}
