namespace Barbuuuda.Models.Chat.Input
{
    /// <summary>
    /// Класс входной модели чата.
    /// </summary>
    public class ChatInput
    {
        /// <summary>
        /// Сообщение.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }
    }
}
