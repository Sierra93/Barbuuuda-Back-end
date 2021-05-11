using Barbuuuda.Core.Data;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис общих методов.
    /// </summary>
    public class CommonMethodsService
    {
        private readonly ApplicationDbContext _db;
        private readonly PostgreDbContext _postgre;

        public CommonMethodsService(ApplicationDbContext db, PostgreDbContext postgre)
        {
            _db = db;
            _postgre = postgre;
        }

        /// <summary>
        /// Метод убирает пробелы в начале и в конце в строки.
        /// </summary>
        /// <param name="str">Строка, в которой нужно убрать пробелы.</param>
        /// <returns>Новая строка без пробелов в начале и в конце.</returns>
        public static string ReplaceSpacesString(string str)
        {
            str = str.Trim();

            return str;
        }
    }
}
