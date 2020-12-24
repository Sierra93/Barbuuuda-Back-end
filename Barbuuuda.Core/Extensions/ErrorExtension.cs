using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace Barbuuuda.Core.Extensions {
    public class ErrorExtension : Controller {
        // Если пароль не верен.
        public JsonResult ThrowErrorLogin() {
            return Json(new { HttpStatusCode.BadRequest, responseText = "Email или пароль введены не верно" });
        }
    }
}
