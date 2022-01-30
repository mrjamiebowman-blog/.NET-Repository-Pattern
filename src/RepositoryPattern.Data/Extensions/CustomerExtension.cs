using KafkaModels.Models.Customer;
using RepositoryPattern.Data.Services;
using RepositoryPattern.Data.Types;

namespace RepositoryPattern.Data.Extensions
{
    public static class CustomerExtension
    {
        public static string GetCustomerId(this Customer model)
        {
            if (model == null)
                return string.Empty;

            var providerType = DataService.GetProviderType();

            if (providerType == DataProviderType.MSSQL) {
                return model.CustomerId.ToString();
            } else if (providerType == DataProviderType.MongoDB) {
                return model.CustomerIdMg.ToString();
            } else if (providerType == DataProviderType.Postgres) {
                return model.CustomerId.ToString();
            }

            return model.CustomerId.ToString();
        }
    }
}
