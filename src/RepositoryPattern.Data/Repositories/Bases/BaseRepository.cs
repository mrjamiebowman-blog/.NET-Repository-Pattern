using RepositoryPattern.Data.Configuration;

namespace RepositoryPattern.Data.Repositories.Bases;

public abstract class BaseRepository
{
    protected DatabaseConfiguration _databaseConfiguration;

    public BaseRepository(DatabaseConfiguration databaseConfiguration)
    {
        _databaseConfiguration = databaseConfiguration;
    }
}
