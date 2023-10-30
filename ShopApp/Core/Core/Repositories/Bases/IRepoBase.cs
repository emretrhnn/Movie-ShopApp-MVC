using System.Linq.Expressions;

namespace Core.Repositories.Bases
{
    //An interface that can use any type through TEntity as an instantiable reference type.
    /// This interface will be the base for all repositories which may be created in the future and
    /// implements the IDisposable interface for Dispose operations.
    public interface IRepoBase<TEntity> : IDisposable where TEntity : class, new()
    {
        /// EN: A query method definition that retrieves records of type entity.
        IQueryable<TEntity> Query(bool isNoTracking = false);

        /// EN: A query method definition that retrieves records of type entity.
        List<TEntity> GetAll();

        /// EN: A method definition for adding records of type Entity to the relevant table. 
        /// If the save parameter is set to true, the entity parameter is added to the corresponding entity set 
        /// and the insert operation is reflected to the database. If the save parameter is set to false, 
        /// in case of multiple entity insertions, entities can first be added to the relevant entity set 
        /// and then the Save method can be called to reflect all the insertions to the database at once.
        void Add(TEntity entity, bool save = true);

        /// EN: A method definition for updating records of type Entity in the relevant table. 
        /// If the save parameter is set to true, the entity parameter is updated in the corresponding entity set 
        /// and the update operation is reflected to the database. If the save parameter is set to false, 
        /// in case of multiple entity updates, entities can first be updated in the relevant entity set
        /// and then the Save method can be called to reflect all the updates to the database at once.
        void Update(TEntity entity, bool save = true);

        /// EN: A method definition for deleting records of type Entity from the relevant table based on the provided 
        /// predicate parameter. If the save parameter is set to true, the entity is deleted from the corresponding entity set 
        /// and the deletion operation is reflected to the database. If the save parameter is set to false, 
        /// in case of multiple entity deletions, entities that match the predicate parameter are first deleted from the 
        /// relevant entity set, and then the Save method can be called to reflect all the deletions to the database at once.
        void Delete(Expression<Func<TEntity, bool>> predicate, bool save = true);

        /// EN: A method definition that allows all operations performed on entity sets, such as Add, Update, or Delete, 
        /// to be reflected to the database at once (Unit of Work). This method returns the number of affected rows 
        /// in the relevant entity tables as a result of the performed operation or operations. If necessary, 
        /// exception logging can be performed within this method.
        int Save();
    }
}
