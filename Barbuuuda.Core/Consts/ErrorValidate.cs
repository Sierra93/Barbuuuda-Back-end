using System;
using System.Collections.Generic;
using System.Text;

namespace Barbuuuda.Core.Consts {
    public class ErrorValidate {
        public const string LOGIN_ERROR = "Такой логин уже существует";
        public const string EMAIL_ERROR = "Такой email уже существует";
        public const string LOGIN_NOT_ADMIN = "Логин не должен содержать имени admin";
        public const string LOGIN_NOT_EMAIL = "Email не должен содержать имени admin";
        public const string EMAIL_NOT_CORRECT_FORMAT = "Email некорректный";
        public const string PHONE_ERROR_EMPTY = "Не указан номер телефона";
        public const string PHONE_ERROR_NOT_CORRECT_FORMAT = "Номер телефона некорректный";
    }
}
