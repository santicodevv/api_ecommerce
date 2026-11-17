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

            var users = _mapper.Map<List<UserDto>>(result);

            return Ok( new { data = users });
        }

        [HttpGet("{id:int}", Name = "GetUser")]
        public async Task<ActionResult<List<User>>> GetUserAsync(int id)
        {
            var result = await _user.GetUserAsync(id);

            if (!result.Success)
            {
                return BadRequest( new { message = result.Message } );
            }

            return Ok( new { message = result.Message, data = _mapper.Map<UserDto>(result) });
        }

        [HttpPost(Name = "RegisterUser")]
        public async Task<ActionResult> RegisterUserAsync([FromBody] CreateUserDto createUserDto)
        {
            var result = await _user.Register(createUserDto);

            if (!result.Success)
            {
                return BadRequest( new { message = result.Message });
            }

            var userDto = _mapper.Map<UserDto>(result.Data);

            return CreatedAtAction("GetUser", new { Id = userDto.Id }, userDto );
        }


        [HttpPost("login", Name = "LoginUser")]
        public async Task<ActionResult> LoginUserAsync([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _user.Login(userLoginDto);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}
