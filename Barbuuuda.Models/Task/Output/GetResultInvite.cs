using System.Collections.Generic;

namespace Barbuuuda.Models.Task.Output
{
    /// <summary>
    /// Класс со списком выходных моделей приглашений.
    /// </summary>
    public class GetResultInvite
    {
        /// <summary>
        /// Список с приглашениями.
        /// </summary>
        public List<InviteOutput> Invities { get; set; } = new List<InviteOutput>();

        /// <summary>
        /// Кол-во приглашений.
        /// </summary>
        public int Count => Invities.Count;
    }
}
