namespace Barbuuuda.Models.Executor.Input
{
    /// <summary>
    /// Класс входной модели для получения следующего вопроса теста.
    /// </summary>
    public class NextQuestionInput
    {
        /// <summary>
        /// Номер вопроса, данные которого нужно выдать.
        /// </summary>
        public int NumberQuestion { get; set; }
    }
}
