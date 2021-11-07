using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.User
{
    /// <summary>
    /// Класс сопоставляется с таблицей пользователей.
    /// </summary>
    [Table("Users", Schema = "User")]
    public class UserEntity : IdentityUser
    {
        [Column("UserPassword", TypeName = "varchar(100)")]
        public string UserPassword { get; set; }

        /// <summary>
        /// Роль пользователя: Заказчик или Исполнитель.
        /// </summary>
        [Column("UserRole", TypeName = "varchar(1)")]
        public string UserRole { get; set; }

        /// <summary>
        /// Фамилия.
        /// </summary>
        [Column("LastName", TypeName = "varchar(100)")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя.
        /// </summary>
        [Column("FirstName", TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество.
        /// </summary>
        [Column("Patronymic", TypeName = "varchar(100)")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Путь к иконке пользователя.
        /// </summary>
        [Column("UserIcon", TypeName = "text")]
        public string UserIcon { get; set; }

        /// <summary>
        /// Дата регистрации пользователя.
        /// </summary>
        [Column("DateRegister", TypeName = "timestamp")]
        public DateTime DateRegister { get; set; }

        /// <summary>
        /// Информация "Обо мне".
        /// </summary>
        [Column("AboutInfo", TypeName = "text")]
        public string AboutInfo { get; set; }

        /// <summary>
        /// Счет пользователя.
        /// </summary>
        //[Column("Score", TypeName = "numeric(12,2)")]
        //public decimal? Score { get; set; }

        /// <summary>
        /// План: PRO или базовый.
        /// </summary>
        [Column("Plan", TypeName = "varchar(10)")]
        public string Plan { get; set; }    

        [Column("City", TypeName = "varchar(200)")]
        public string City { get; set; }

        [Column("Age", TypeName = "integer")]
        public int Age { get; set; }

        /// <summary>
        /// M - мужчина, F - женщина.
        /// </summary>
        [Column("Gender", TypeName = "varchar(1)")]
        public string Gender { get; set; }    

        [Column("RememberMe", TypeName = "boolean")]
        public bool RememberMe { get; set; }        

        [Column("ExecutorSpecializations", TypeName = "jsonb")]
        public ExecutorSpecialization[] Specializations { get; set; }

        /// <summary>
        /// Пройден ли тест.
        /// </summary>
        [Column("IsSuccessedTest", TypeName = "bool")]
        public bool IsSuccessedTest { get; set; }   
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
