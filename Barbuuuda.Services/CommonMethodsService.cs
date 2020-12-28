using Barbuuuda.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Barbuuuda.Services {
    /// <summary>
    /// Сервис общих методов.
    /// </summary>
    public class CommonMethodsService<TEnum> {
        ApplicationDbContext _db;

        public CommonMethodsService(ApplicationDbContext db) {
            _db = db;
        }

        /// <summary>
        /// Метод получает description перечислителя, который передан в метод.
        /// </summary>
        /// <param name="tenum"></param>
        /// <returns>Description.</returns>
        public string GetEnumDescription(Enum tenum) {
            Type type = tenum.GetType();
            MemberInfo[] memInfo = type.GetMember(tenum.ToString());

            if (memInfo != null && memInfo.Length > 0) {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0) {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return tenum.ToString();
        }
    }
}
