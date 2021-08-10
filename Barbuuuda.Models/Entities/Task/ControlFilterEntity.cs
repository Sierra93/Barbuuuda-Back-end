using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Task
{
    /// <summary>
    /// Класс сопоставляется с таблицей для контрола селекта фильтрации заданий.
    /// </summary>
    [Table("ControlFilters", Schema = "dbo")]
    public class ControlFilterEntity
    {
        /// <summary>
        /// PK.
        /// </summary>
        [Key]
        public long FilterId { get; set; }

        /// <summary>
        /// Ключ фильтрации.
        /// </summary>
        public string FilterKey { get; set; }

        /// <summary>
        /// Значение фильтрации.
        /// </summary>
        public string FilterValue { get; set; }
    }
}
