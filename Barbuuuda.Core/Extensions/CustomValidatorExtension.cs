using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Barbuuuda.Core.Extensions.User
{
    /// <summary>
    /// Класс валидирует поля при регистрации юзера.
    /// </summary>
    public sealed class CustomValidatorExtension : IUserValidator<UserEntity>
    {
        private List<IdentityError> errors = new List<IdentityError>();
        private readonly PostgreDbContext _postgre;

        public CustomValidatorExtension(PostgreDbContext postgre)
        {
            _postgre = postgre;
        }

        /// <summary>
        /// Метод валидирует все поля регистрации.
        /// </summary>
        /// <param name="manager">Объект регистрации.</param>
        /// <param name="user">Объект с данными юзера для валидации.</param>
        /// <returns></returns>
        public async Task<IdentityResult> ValidateAsync(UserManager<UserEntity> manager, UserEntity user)
        {
            try
            {
                // Пытается найти такого юзера по логину.
                UserEntity isLogin = await _postgre.Users
                    .Where(u => u.UserName.Equals(user.UserName))
                    .FirstOrDefaultAsync();

                // Пытается найти такого юзера по email.
                UserEntity isEmail = await _postgre.Users
                    .Where(u => u.Email.Equals(user.Email))
                    .FirstOrDefaultAsync();

                // Если есть юзер с таким логином.
                if (isLogin != null)
                {
                    errors.Add(new IdentityError
                    {
                        Description = ErrorValidate.LOGIN_ERROR
                    });
                }

                // Если логин содержит admin.
                if (user.UserName.Contains("admin"))
                {
                    errors.Add(new IdentityError
                    {
                        Description = ErrorValidate.LOGIN_NOT_ADMIN
                    });
                }

                // Проверяет Email на корректность.
                Match isCorrectEmail = Regex.Match(user.Email, RegularExpressions.REGULAR_EMAIL);
                if (!isCorrectEmail.Success)
                {
                    errors.Add(new IdentityError
                    {
                        Description = ErrorValidate.EMAIL_NOT_CORRECT_FORMAT
                    });
                }

                // Если есть юзер с таким email.
                if (isEmail != null)
                {
                    errors.Add(new IdentityError
                    {
                        Description = ErrorValidate.EMAIL_ERROR
                    });
                }

                // Если email содержит admin.
                if (user.Email.Contains("admin"))
                {
                    errors.Add(new IdentityError
                    {
                        Description = ErrorValidate.LOGIN_NOT_EMAIL
                    });
                }

                // Проверяет наличие номера телефона.                
                //if (string.IsNullOrEmpty(user.PhoneNumber)) {
                //    errors.Add(new IdentityError {
                //        Description = ErrorValidate.PHONE_ERROR_EMPTY
                //    });                    
                //}

                //// Проверяет корректность номера телефона.
                //Match matchPhone = Regex.Match(user.PhoneNumber, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");
                //if (!matchPhone.Success && !string.IsNullOrEmpty(user.PhoneNumber)) {
                //    errors.Add(new IdentityError {
                //        Description = ErrorValidate.PHONE_ERROR_NOT_CORRECT_FORMAT
                //    });
                //}

                return await Task.FromResult(errors.Count == 0 ?
               IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Метод проверяет, логин передан или email.
        /// </summary>
        /// <param name="inputParam">Входной параметр.</param>
        /// <returns>Статус проверки: true - если передан email, false - если передан логин.</returns>
        public static bool CheckIsEmail(string inputParam)
        {
            if (string.IsNullOrEmpty(inputParam))
            {
                return false;
            }

            const RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture;
            Regex emailValidator = new Regex(RegularExpressions.REGULAR_EMAIL, options);

            return emailValidator.IsMatch(inputParam);
        }
    }
}
