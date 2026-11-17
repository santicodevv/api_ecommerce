using ApiEcommerce.Interfaces.User;
using ApiEcommerce.Models.User;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _user;
        private readonly IMapper _mapper;
        public UserController(IUserServices userServices, IMapper mapper)
        {
            _user = userServices;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetUsers")]
        public async Task<ActionResult<List<User>>> GetUsersAsync([FromQuery] PaginationParams pagination)
        {
            var result = await _user.GetUsersAsync(pagination);

            if (result.Count == 0)
            {
                return BadRequest( new { message = "No hay usuarios registrado"} );
            }

            return Ok( new { data = result });
        }
    }
}
