using MarketingBox.Postback.Service.Postgres;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Postback.Service.Repositories
{
    public abstract class RepositoryBase<T>
    {
        protected readonly ILogger<T> _logger;
        protected readonly DatabaseContextFactory _factory;
        protected RepositoryBase(
            ILogger<T> logger,
            DatabaseContextFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }
    }
}
