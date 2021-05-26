using Barbuuuda.Models.Entities.Knowlege;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция сервиса БЗ.
    /// </summary>
    public interface IKnowlegeService
    {
        /// <summary>
        /// Метод выгружает список категорий для БЗ.
        /// </summary>
        /// <returns>Список категорий.</returns>
        Task<IEnumerable<KnowlegeCategoryEntity>> GetCategoryListAsync();

        /// <summary>
        /// Метод выгружает список популярных статей.
        /// </summary>
        /// <returns>Список статей.</returns>
        Task<IEnumerable<PopularArticleEntity>> GetPopularArticlesAsync();

        /// <summary>
        /// Метод выгружает список статей категорий.
        /// </summary>
        /// <returns>Список статей.</returns>
        Task<IEnumerable<KnowlegeArticleEntity>> GetCategoryArticles();
    }    
}
