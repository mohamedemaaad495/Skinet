using Api.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("erorrs/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErorrController: BaseApiController
    {
        public IActionResult Erorr(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}
