using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Services
{
    /// <summary>
    /// Сервис методов расширений.
    /// </summary>
    public static class ExtensionService
    {
        /// <summary>
        /// Метод расширения для реверсирования коллекции типа IEnumerable.
        /// </summary>
        /// <param name="list">Коллекция типа IEnumerable, которую нужно реверснуть.</param>
        /// <returns></returns>
        public static IEnumerable<T> Reverse<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }
    }
}
