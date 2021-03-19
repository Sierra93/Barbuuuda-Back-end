﻿using Barbuuuda.Models.User;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Barbuuuda.Models.Entities.Respond
{
    /// <summary>
    /// Класс сопоставляется с таблицей шаблонов ставок.
    /// </summary>
    [Table("RespondTemplates", Schema = "dbo")]
    public sealed class RespondTemplateEntity
    {
        /// <summary>
        /// Первичный ключ.
        /// </summary>
        public int TemplateId { get; set; }

        /// <summary>
        /// Код шаблона.
        /// </summary>
        public Guid TemplateCode { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Id пользователя, который сохранил шаблон. Внешний ключ к таблице пользователей.
        /// </summary>
        public int Id { get; set; }
        public UserEntity User { get; set; }

        /// <summary>
        /// Название шаблона.
        /// </summary>
        [DefaultValue("")]
        public string TemplateName { get; set; }

        /// <summary>
        /// Комментарий.
        /// </summary>
        [DefaultValue("")]
        public string TemplateComment { get; set; }
    }
}
