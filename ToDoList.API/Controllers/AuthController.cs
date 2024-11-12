using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Token;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            _mapper = mapper;
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel login)
        {
            try
            {
               var user = _mapper.Map<LoginDTO>(login);
               
               var token = await _authService.Authenticate(user);
               return Ok(ResponseReturn.SucessMessage("Login success", token));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }
    }
}
