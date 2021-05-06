﻿using System;

namespace Barbuuuda.Core.Exceptions
{
    /// <summary>
    /// Исключение возникает, если диалога не найдено.
    /// </summary>
    public class NotFoundDialogIdException : Exception
    {
        public NotFoundDialogIdException(long? dialogId) : base($"Диалога с DialogId {dialogId} не найдено")
        {

        }
    }
}
