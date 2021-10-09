using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _sender;

        protected ISender Sender => _sender ??= (ISender)HttpContext.RequestServices.GetService(typeof(ISender));
    }
}
