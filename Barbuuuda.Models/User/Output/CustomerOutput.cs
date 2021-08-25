namespace Barbuuuda.Models.User.Output
{
    /// <summary>
    /// Выходная модель заказчика.
    /// </summary>
    public class CustomerOutput
    {
        /// <summary>
        /// Логин заказчика задания.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Иконка профиля заказчика задания.
        /// </summary>
        public string UserIcon { get; set; }
    }
}
