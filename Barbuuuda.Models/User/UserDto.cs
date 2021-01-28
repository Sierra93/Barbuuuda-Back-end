using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Barbuuuda.Models.User {
    /// <summary>
    /// Класс сопоставляется с таблицей пользователей.
    /// </summary>
    [Table("AspNetUsers")]
    public sealed class UserDto : IdentityUser {
        //[Column("UserLogin", TypeName = "varchar(100)")]
        //public string UserLogin { get; set; }

        [Column("UserPassword", TypeName = "varchar(100)")]
        public string UserPassword { get; set; }

        //[Column("UserEmail", TypeName = "varchar(100)")]
        ////[Required(ErrorMessage = "Не указан электронный адрес")]
        ////[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        //public string UserEmail { get; set; }

        [Column("UserType", TypeName = "varchar(50)")]
        public string UserType { get; set; }    // Тип пользователя: Заказчик или Исполнитель.

        //[Column("UserPhone", TypeName = "varchar(100)")]
        //public string UserPhone { get; set; }   // Номер телефона пользователя.

        [Column("LastName", TypeName = "varchar(100)")]
        public string LastName { get; set; }    // Фамилия.

        [Column("FirstName", TypeName = "varchar(100)")]
        public string FirstName { get; set; }   // Имя.

        [Column("Patronymic", TypeName = "varchar(100)")]
        public string Patronymic { get; set; }  // Отчество.

        //[Column("Token", TypeName = "text")]
        //public string Token { get; set; }   // Токен юзера.

        [Column("UserIcon", TypeName = "text")]
        public string UserIcon { get; set; }    // Путь к иконке пользователя.

        [Column("CountPositive", TypeName = "integer")]
        public int CountPositive { get; set; }  // Кол-во положительных отзывов исполнителя.

        [Column("CountNegative", TypeName = "integer")]
        public int CountNegative { get; set; }  // Кол-во отрицательных отзывов исполнителя.

        [Column("Rating", TypeName = ("numeric(12,2)"))]
        public double Rating { get; set; }  // Рейтинг исполнителя.

        [Column("IsOnline", TypeName = "boolean")]
        public bool IsOnline { get; set; }

        [Column("DateRegister", TypeName = "timestamp")]
        public DateTime DateRegister { get; set; }  // Дата регистрации пользователя.

        [Column("AboutInfo", TypeName = "text")]
        public string AboutInfo { get; set; }   // Информация "Обо мне".

        [Column("Score", TypeName = "numeric(12,2)")]
        public decimal? Score { get; set; }     // Счет пользователя.

        [Column("Plan", TypeName = "varchar(10)")]
        public string Plan { get; set; }    // План: PRO или базовый.

        [Column("City", TypeName = "varchar(200)")]
        public string City { get; set; }

        [Column("Age", TypeName = "integer")]
        public int Age { get; set; }

        [Column("Gender", TypeName = "varchar(1)")]
        public string Gender { get; set; }    // M - мужчина, F - женщина.
    }
}
