using Barbuuuda.Models.Entities.Knowlege;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Абстракция сервиса БЗ.
    /// </summary>
    public interface IKnowlege
    {
        /// <summary>
        /// Метод выгружает список категорий для БЗ.
        /// </summary>
        /// <returns>Список категорий.</returns>
        Task<IEnumerable<KnowlegeCategoryEntity>> GetCategoryListAsync();

        /// <summary>
        /// Метод выгружает список статей категорий
        /// </summary>
        /// <returns>Список категорий.</returns>
        Task<IEnumerable> GetCategoryArticles();

    }
}
