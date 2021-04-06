using System.Collections.Generic;

namespace Barbuuuda.Models.Respond.Outpoot
{
    /// <summary>
    /// Класс выходной модели со списком и кол-вом ставок.
    /// </summary>
    public class GetRespondResultOutpoot
    {
        /// <summary>
        /// Список ставок.
        /// </summary>
        public List<RespondOutpoot> Responds { get; set; } = new List<RespondOutpoot>();

        /// <summary>
        /// Кол-во ставок.
        /// </summary>
        public int Count { get { return Responds.Count; } set { } }
    }
}
