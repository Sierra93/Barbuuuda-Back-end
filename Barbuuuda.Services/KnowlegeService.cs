using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Knowlege;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы БЗ.
    /// </summary>
    public sealed class KnowlegeService : IKnowlegeService
    {
        private readonly PostgreDbContext _postgre;

        public KnowlegeService(PostgreDbContext postgre)
        {
            _postgre = postgre;
        }

        /// <summary>
        /// Метод выгружает список категорий для БЗ.
        /// </summary>
        /// <returns>Список категорий.</returns>       
        public async Task<IEnumerable<KnowlegeCategoryEntity>> GetCategoryListAsync()
        {
            try
            {
                return await _postgre.KnowlegeCategories.ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод возвращает список статей.
        /// </summary>
        /// <returns>Список статей.</returns>
        public async Task<IEnumerable<KnowlegeArticleEntity>> GetCategoryArticles()
        {
            try
            {
                return await _postgre.KnowlegeArticles.ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод выгружает список популярных статей в порядке убывания HelpfulCount.
        /// </summary>  
        /// <returns>Список популярных статей.</returns>
        public async Task<IEnumerable<PopularArticleEntity>> GetPopularArticlesAsync()
        {
            try
            {            
                return await _postgre.PopularArticles
                    .OrderByDescending(a => a.HelpfulCount)
                    .ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

    }

}
