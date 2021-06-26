using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Knowlege;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    [ApiController]
    [Route("knowlege")]
    public class KnowlegeController : BaseController
    {
        private readonly IKnowlegeService _knowlege;

        public KnowlegeController(IKnowlegeService knowlege)
        {
            _knowlege = knowlege;
        }

        /// <summary>
        /// Метод выгружает список категорий для БЗ.
        /// </summary>
        /// <returns>Список категорий.</returns>
        [HttpPost, Route("category")]
        public async Task<IActionResult> GetCategoryListAsync()
        {
            IEnumerable<KnowlegeCategoryEntity> categoryList = await _knowlege.GetCategoryListAsync();

            return Ok(categoryList);
        }

        /// <summary>
        /// Метод выгружает список популярных статей.
        /// </summary>
        /// <returns>Список статей.</returns>
        [HttpPost, Route("popular")]
        public async Task<IActionResult> GetPopularArticlesAsync()
        {
            IEnumerable<PopularArticleEntity> populArticlesList = await _knowlege.GetPopularArticlesAsync();           
            
            return Ok(populArticlesList);
        }

        /// <summary>
        /// Метод выгружает список статей категорий.
        /// </summary>
        /// <returns>Список статей.</returns>
        [HttpPost, Route("articles-list")]
        public async Task<IActionResult> GetCategoryArticles()
        {
            IEnumerable<KnowlegeArticleEntity> articleList = await _knowlege.GetCategoryArticles();

            return Ok(articleList);
        }
    }
} 
