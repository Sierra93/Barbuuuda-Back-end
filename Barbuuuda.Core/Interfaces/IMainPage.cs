using Barbuuuda.Models.MainPage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Interfaces {
    /// <summary>
    /// Интерфейс определяет методы главной страницы.
    /// </summary>
    public interface IMainPage {
        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        Task<FonDto> GetFonContent();

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA"
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        Task<IList<WhyDto>> GetWhyContent();

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ"
        /// </summary>
        /// <returns>Объект WorkDto</returns>
        Task<IList<WorkDto>> GetWorkContent();

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        Task<IList<PrivilegeDto>> GetPrivilegeContent();

        /// <summary>
        /// Метод выгружает данные для блока "Преимущества"
        /// </summary>
        /// <returns>Объект Advantage</returns>
        Task<IList<AdvantageDto>> GetAdvantageContent();
    }
}
