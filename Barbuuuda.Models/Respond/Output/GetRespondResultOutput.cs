using System.Collections.Generic;

namespace Barbuuuda.Models.Respond.Output
{
    /// <summary>
    /// Класс выходной модели со списком и кол-вом ставок.
    /// </summary>
    public class GetRespondResultOutput
    {
        /// <summary>
        /// Список ставок.
        /// </summary>
        public List<RespondOutput> Responds { get; set; } = new List<RespondOutput>();

        /// <summary>
        /// Кол-во ставок.
        /// </summary>
        public int Count => Responds.Count;
    }
}
