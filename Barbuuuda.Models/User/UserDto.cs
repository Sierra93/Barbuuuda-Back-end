using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.User {
    /// <summary>
    /// Модель описывает пользователя сервиса.
    /// </summary>
    [Table("Users", Schema = "dbo")]
    public sealed class UserDto {
        [Key, Column("user_id")]
        public int UserId { get; set; }

        [Column("user_login", TypeName = "varchar(100)")]
        public string UserLogin { get; set; }

        [Column("user_password", TypeName = "varchar(100)")]
        public string UserPassword { get; set; }

        [Column("user_email", TypeName = "varchar(100)")]
        [Required(ErrorMessage = "Не указан электронный адрес")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        public string UserEmail { get; set; }

        [Column("user_type", TypeName = "varchar(50)")]
        public string UserType { get; set; }    // Тип пользователя: Заказчик или Исполнитель.

        [Column("user_phone", TypeName = "varchar(100)")]
        public string UserPhone { get; set; }   // Номер телефона пользователя.

        [Column("last_name", TypeName = "varchar(100)")]
        public string LastName { get; set; }    // Фамилия.

        [Column("first_name", TypeName = "varchar(100)")]
        public string FirstName { get; set; }   // Имя.

        [Column("patronymic", TypeName = "varchar(100)")]
        public string Patronymic { get; set; }  // Отчество.

        [Column("token", TypeName = "varchar(max)")]
        public string Token { get; set; }   // Токен юзера.

        [Column("user_icon", TypeName = "varchar(max)")]
        public string UserIcon { get; set; }    // Путь к иконке пользователя.

        [Column("count_positive", TypeName = "integer")]
        public int CountPositive { get; set; }  // Кол-во положительных отзывов исполнителя.

        [Column("count_negative", TypeName = "integer")]
        public int CountNegative { get; set; }  // Кол-во отрицательных отзывов исполнителя.

        [Column("rating", TypeName = ("double"))]
        public double Rating { get; set; }  // Рейтинг исполнителя.

        [Column("is_online", TypeName = "boolean")]
        public bool IsOnline { get; set; }

        [Column("date_register", TypeName = "timestamp")]
        public DateTime DateRegister { get; set; }  // Дата регистрации пользователя.
    }
}
