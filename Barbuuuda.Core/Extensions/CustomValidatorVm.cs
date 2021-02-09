using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Barbuuuda.Core.ViewModels.User
{
    /// <summary>
    /// Класс валидирует поля при регистрации юзера.
    /// </summary>
    public sealed class CustomValidatorVm : IUserValidator<UserEntity>
    {
        private List<IdentityError> aErrors = new List<IdentityError>();
        private readonly IdentityDbContext _iden;

        public CustomValidatorVm(IdentityDbContext iden)
        {
            _iden = iden;
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
                UserEntity isLogin = await _iden.AspNetUsers
                    .Where(u => u.UserName.Equals(user.UserName))
                    .FirstOrDefaultAsync();

                // Пытается найти такого юзера по email.
                UserEntity isEmail = await _iden.AspNetUsers
                    .Where(u => u.Email.Equals(user.Email))
                    .FirstOrDefaultAsync();

                // Если есть юзер с таким логином.
                if (isLogin != null)
                {
                    aErrors.Add(new IdentityError
                    {
                        Description = ErrorValidate.LOGIN_ERROR
                    });
                }

                // Если логин содержит admin.
                if (user.UserName.Contains("admin"))
                {
                    aErrors.Add(new IdentityError
                    {
                        Description = ErrorValidate.LOGIN_NOT_ADMIN
                    });
                }

                // Проверяет Email на корректность.
                Match isCorrectEmail = Regex.Match(user.Email, @"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}");
                if (!isCorrectEmail.Success)
                {
                    aErrors.Add(new IdentityError
                    {
                        Description = ErrorValidate.EMAIL_NOT_CORRECT_FORMAT
                    });
                }

                // Если есть юзер с таким email.
                if (isEmail != null)
                {
                    aErrors.Add(new IdentityError
                    {
                        Description = ErrorValidate.EMAIL_ERROR
                    });
                }

                // Если email содержит admin.
                if (user.Email.Contains("admin"))
                {
                    aErrors.Add(new IdentityError
                    {
                        Description = ErrorValidate.LOGIN_NOT_EMAIL
                    });
                }

                // Проверяет наличие номера телефона.                
                //if (string.IsNullOrEmpty(user.PhoneNumber)) {
                //    aErrors.Add(new IdentityError {
                //        Description = ErrorValidate.PHONE_ERROR_EMPTY
                //    });                    
                //}

                //// Проверяет корректность номера телефона.
                //Match matchPhone = Regex.Match(user.PhoneNumber, @"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");
                //if (!matchPhone.Success && !string.IsNullOrEmpty(user.PhoneNumber)) {
                //    aErrors.Add(new IdentityError {
                //        Description = ErrorValidate.PHONE_ERROR_NOT_CORRECT_FORMAT
                //    });
                //}

                return await Task.FromResult(aErrors.Count == 0 ?
               IdentityResult.Success : IdentityResult.Failed(aErrors.ToArray()));
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
