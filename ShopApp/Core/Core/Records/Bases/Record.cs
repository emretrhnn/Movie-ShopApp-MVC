namespace Core.Records.Bases
{
    /// EN: An abstract class that all entities and models, except for relationship entities, will inherit from, 
    /// and which contains properties to create columns in the corresponding tables for entities in the database.
    public abstract class Record
    {
        public int Id { get; set; } // required

        public string? Guid { get; set; } // ?: nullable
    }
}
