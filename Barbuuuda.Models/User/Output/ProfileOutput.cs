namespace Barbuuuda.Models.User.Output
{
    /// <summary>
    /// Класс выходной модели
    /// </summary>
    public class ProfileOutput
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email пользователя.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Конт.телефон пользователя.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Url иконки.
        /// </summary>
        public string UserIcon { get; set; }

        /// <summary>
        /// Информация обо мне.
        /// </summary>
        public string AboutInfo { get; set; }

        /// <summary>
        /// План. PRO или для всех.
        /// </summary>
        public string Plan { get; set; }

        /// <summary>
        /// Город.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Возраст.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Дата регистрации на сервисе.
        /// </summary>
        public string DateRegister { get; set; }

        /// <summary>
        /// Сумма счета.
        /// </summary>
        public string ScoreMoney { get; set; }
    }
}
