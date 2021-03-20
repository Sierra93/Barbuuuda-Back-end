using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Knowlege;
using Barbuuuda.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Barbuuuda.Controllers
{
    [ApiController]
    [Route("knowlege")]
    public class KnowlegeController : BaseController
    {
        public static string Module => "Barbuuuda.knowlege";
        private readonly IKnowlege _knowlege;

        public KnowlegeController(IKnowlege knowlege) : base(Module)
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
    }
}
