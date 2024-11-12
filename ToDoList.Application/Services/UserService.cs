using System.Threading.Channels;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;

namespace ToDoList.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserService> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(IMapper mapper, IUserRepository userRepository, ILogger<UserService> logger, IPasswordHasher<User> passwordHasher)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _logger = logger;
        _passwordHasher = passwordHasher;
    }

    public async Task<UserDTO> Create(UserCreateDTO userCreateDto)
    {
        var userExists = _userRepository.GetByEmail(userCreateDto.Email);
        
        if (userExists != null) throw new DomainException("Email already exists");
        
        var user = _mapper.Map<User>(userCreateDto);
        
        user.Password = _passwordHasher.HashPassword(user, user.Password);
        user.Validate();

        var userCreated = await _userRepository.Create(user);
        

        return _mapper.Map<UserDTO>(userCreated);
    }

    public async Task<UserDTO> Update(UserUpdateDTO userUpdateDto)
    {
        var userExists = _userRepository.GetByEmail(userUpdateDto.Email);
        
        if (userExists == null) throw new DomainException("User not found");
        
        var user = _mapper.Map<User>(userUpdateDto);
        user.Password = _passwordHasher.HashPassword(user, user.Password);
        user.Validate();
        
        return _mapper.Map<UserDTO>(await _userRepository.Update(user));
    }

    public async Task Delete(long id)
    {
        var user = await _userRepository.GetById(id);
        if (user == null) throw new DomainException("User not found");
        
        await _userRepository.Delete(id);
    }

    public async Task<UserDTO?> GetUserById(long id) 
        => _mapper.Map<UserDTO>(await _userRepository.GetById(id)); //Função

    public async Task<IEnumerable<UserDTO>> GetUsers()
    {
        var users = await _userRepository.GetAll();
        
        return _mapper.Map<IEnumerable<UserDTO>>(users); 
    }
    
    public async Task<UserDTO?> GetUserByEmail(string email) 
        => _mapper.Map<UserDTO>(await _userRepository.GetByEmail(email)); //Função

    public async Task<IEnumerable<UserDTO>> SearchByName(string name) 
        => _mapper.Map<IEnumerable<UserDTO>>(await _userRepository.SearchByName(name)); //Função
}