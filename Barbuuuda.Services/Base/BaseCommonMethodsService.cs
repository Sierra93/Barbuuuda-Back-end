using Barbuuuda.Core.Data;

namespace Barbuuuda.Services.Base
{
    /// <summary>
    /// Базовый класс всех общих методов для сервисов.
    /// </summary>
    public abstract class BaseCommonMethodsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly PostgreDbContext _postgreDbContext;

        public BaseCommonMethodsService(ApplicationDbContext dbContext, PostgreDbContext postgreDbContext)
        {
            _dbContext = dbContext;
            _postgreDbContext = postgreDbContext;
        }
    }
}
