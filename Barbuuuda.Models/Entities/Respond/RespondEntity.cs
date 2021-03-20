using Barbuuuda.Models.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Respond
{
    /// <summary>
    /// Класс сопоставляется с таблицей ставок к заданиям.
    /// </summary>
    [Table("Responds", Schema = "dbo")]
    public sealed class RespondEntity
    {
        /// <summary>
        /// Первичный ключ.
        /// </summary>
        public int RespondId { get; set; }

        /// <summary>
        /// Цена ставки (без комиссии).
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Внешний ключ к таблице пользователей.
        /// </summary>
        public int Id { get; set; }
        public UserEntity User { get; set; }
    }
}
