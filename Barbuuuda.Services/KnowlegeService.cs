﻿using Barbuuuda.Core.Data;
using Barbuuuda.Core.Interfaces;
using Barbuuuda.Models.Entities.Knowlege;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис реализует методы БЗ.
    /// </summary>
    public sealed class KnowlegeService : IKnowlege
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
                //throw new NotImplementedException();
                return await _postgre
                    .KnowlegeCategories                    
                    .ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод связывает внешним ключом статьи с определенной категорией в таблице dbo.KnowlegeCategories.
        /// </summary>
        /// <returns>Список статей.</returns>
        public async Task<IEnumerable> GetCategoryArticles()
        {

        }
    }
}
