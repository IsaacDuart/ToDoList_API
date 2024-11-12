using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;

namespace ToDoList.Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> Create(UserCreateDTO userCreateDto);
    Task<UserDTO> Update(UserUpdateDTO userUpdateDto);
    Task Delete(long id);
    Task<UserDTO?> GetUserById(long id);
    Task<IEnumerable<UserDTO>> GetUsers();
    Task<UserDTO?> GetUserByEmail(string email);
    Task<IEnumerable<UserDTO>> SearchByName(string name);

}