using KafkaModels.Models.Customer;
using RepositoryPattern.Data.Types;

namespace RepositoryPattern.Data.Extensions
{
    public static class CustomerExtension
    {
        public static string GetCustomerId(this Customer model)
        {
            if (model == null)
                return string.Empty;

            // TODO: fix
            var dataStoreType = DataStoreType.MSSQL;

            if (dataStoreType == DataStoreType.MSSQL) {
                return model.CustomerId.ToString();
            } else if (dataStoreType == DataStoreType.MongoDB) {
                return model.CustomerIdMg.ToString();
            } else if (dataStoreType == DataStoreType.Postgres) {
                return model.CustomerId.ToString();
            }

            return model.CustomerId.ToString();
        }
    }
}
