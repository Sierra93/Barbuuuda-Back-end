using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.User
{
    /// <summary>
    /// Класс сопоставляется с таблицей пользователей.
    /// </summary>
    [Table("AspNetUsers")]
    public sealed class UserEntity : IdentityUser
    {
        [Column("UserPassword", TypeName = "varchar(100)")]
        public string UserPassword { get; set; }

        [Column("UserRole", TypeName = "varchar(1)")]
        public string UserRole { get; set; }    // Роль пользователя: Заказчик или Исполнитель.

        [Column("LastName", TypeName = "varchar(100)")]
        public string LastName { get; set; }    // Фамилия.

        [Column("FirstName", TypeName = "varchar(100)")]
        public string FirstName { get; set; }   // Имя.

        [Column("Patronymic", TypeName = "varchar(100)")]
        public string Patronymic { get; set; }  // Отчество.

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

        [Column("RememberMe", TypeName = "boolean")]
        public bool RememberMe { get; set; }        

        [Column("ExecutorSpecializations", TypeName = "jsonb")]
        public ExecutorSpecialization[] Specializations { get; set; }

        [Column("IsSuccessedTest", TypeName = "bool")]
        public bool IsSuccessedTest { get; set; }   // Пройден ли тест.
    }

    /// <summary>
    /// Класс сопоставляется с json столбцом специализаций таблицы юзеров (Используется только для исполнителей).
    /// </summary>
    [NotMapped]
    public sealed class ExecutorSpecialization
    {
        public string SpecName { get; set; }
    }    
}
