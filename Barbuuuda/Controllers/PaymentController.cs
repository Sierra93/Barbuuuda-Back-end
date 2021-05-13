using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Barbuuuda.Controllers
{
    /// <summary>
    /// Контроллер платежной системы.
    /// </summary>
    [ApiController, Route("payment")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PaymentController : BaseController
    {
        public PaymentController()
        {

        }

        [HttpGet, Route("paytest")]
        public IActionResult PayTest()
        {
            return Ok();
        }
    }
}
