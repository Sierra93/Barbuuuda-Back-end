using Barbuuuda.Core.Consts;
using Barbuuuda.Core.Data;
using Barbuuuda.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbuuuda.Core.ViewModels.User {
    /// <summary>
    /// Класс валидирует поля при регистрации юзера.
    /// </summary>
    public sealed class CustomValidatorVm : IUserValidator<UserDto> {
        private List<IdentityError> aErrors = new List<IdentityError>();
        private readonly IdentityDbContext _iden;

        public CustomValidatorVm(IdentityDbContext iden) {
            _iden = iden;
        }

        /// <summary>
        /// Метод валидирует все поля регистрации.
        /// </summary>
        /// <param name="manager">Объект регистрации.</param>
        /// <param name="user">Объект с данными юзера для валидации.</param>
        /// <returns></returns>
        public async Task<IdentityResult> ValidateAsync(UserManager<UserDto> manager, UserDto user) {
            try {
                UserDto isLogin = await _iden.AspNetUsers
                    .Where(u => u.UserName.Equals(user.UserName))
                    .FirstOrDefaultAsync();

                UserDto isEmail = await _iden.AspNetUsers
                    .Where(u => u.Email.Equals(user.Email))
                    .FirstOrDefaultAsync();

                // Если есть юзер с таким логином.
                if (isLogin != null) {
                    aErrors.Add(new IdentityError {
                        Description = ErrorValidate.LOGIN_ERROR
                    });
                }

                // Если есть юзер с таким email.
                if (isEmail != null) {
                    aErrors.Add(new IdentityError {
                        Description = ErrorValidate.EMAIL_ERROR
                    });
                }

                return await Task.FromResult(aErrors.Count == 0 ?
               IdentityResult.Success : IdentityResult.Failed(aErrors.ToArray()));
            }

            catch (Exception ex) {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
