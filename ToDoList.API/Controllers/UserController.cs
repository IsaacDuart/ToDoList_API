using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.ModelViews.CreateModelViews;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels;
using ToDoList.API.ViewModels.UpdateViewModels;
using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;
using System;
using Microsoft.AspNetCore.Authorization;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
    
        [Route("/api/v1/users/create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserCreateDTO>(userViewModel);
                
                var userCreated = await _userService.Create(userDTO);
                
                return Ok(ResponseReturn.SucessMessage("User Created", userCreated));
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
        
        [HttpPut]
        [Authorize]
        [Route("/api/v1/users/update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel userViewModel)
        {
            try
            {
                var userDTO = _mapper.Map<UserUpdateDTO>(userViewModel);
                
                var userUpdated = await _userService.Update(userDTO);
                
                return Ok(ResponseReturn.SucessMessage("User Updated",userUpdated));
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

        [HttpDelete]
        [Authorize]
        [Route("/api/v1/users/delete/{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            try
            {
                await _userService.Delete(id);
                
                return Ok(ResponseReturn.SucessMessage("User Deleted", null));
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
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var user = await _userService.GetUserById(id);

                if (user is null)
                {
                     return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("User Found", user));
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
        
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetUsers();
                

                if (users is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("List of Users", users));
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
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/getbyemail/{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmail(email);

                if (user is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("User Found", user));
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
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/users/searchbyname/{name}")]
        public async Task<IActionResult> SearchByName(string name)
        {
            try
            {
                var users = await _userService.SearchByName(name);

                if (users is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("List of Users", users));
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
