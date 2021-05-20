using Barbuuuda.Models.MainPage;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Barbuuuda.Models.Entities.MainPage.Output;

namespace Barbuuuda.Core.Interfaces
{
    /// <summary>
    /// Интерфейс определяет методы главной страницы.
    /// </summary>
    public interface IMainPage
    {
        /// <summary>
        /// Метод получает информацию для главного фона.
        /// </summary>
        /// <returns>Объект фона.</returns>
        Task<FonEntity> GetFonContent();

        /// <summary>
        /// Метод выгружает данные для блока "ПОЧЕМУ BARBUUUDA"
        /// </summary>
        /// <returns>Все объекты WhyDto</returns>
        Task<IList<WhyEntity>> GetWhyContent();

        /// <summary>
        /// Метод выгружает данные для блока "КАК ЭТО РАБОТАЕТ"
        /// </summary>
        /// <returns>Объект WorkDto</returns>
        Task<IList<WorkEntity>> GetWorkContent();

        /// <summary>
        /// Метод выгружает данные для блока "ЧТО ВЫ ПОЛУЧАЕТЕ"
        /// </summary>
        /// <returns>Все объекты PrivilegeDto</returns>
        Task<IList<PrivilegeEntity>> GetPrivilegeContent();

        /// <summary>
        /// Метод выгружает данные для блока "Преимущества"
        /// </summary>
        /// <returns>Объект Advantage</returns>
        Task<IList<AdvantageEntity>> GetAdvantageContent();

        /// <summary>
        /// Метод выгружает список категорий заданий.
        /// </summary>
        /// <returns></returns>
        Task<IList> GetCategoryList();


        /// <summary>
        /// Метод полчает данные долгосрочного сотрудничества.
        /// </summary>
        /// <returns>ОБъект с данными.</returns>
        Task<HopeEntity> GetHopeContent();

        /// <summary>
        /// Метод выгружает 5 последних заданий. Не важно, чьи они.
        /// </summary>
        /// <returns>Список с 5 заданиями.</returns>
        Task<IEnumerable> GetLastTasksAsync();

        /// <summary>
        /// Метод получит контактные данные сервиса.
        /// </summary>
        /// <returns>Контактная информация.</returns>
        Task<ContactOutput> GetContactsAsync();
    }
}
