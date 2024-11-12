using ToDoList.Application.DTO;

namespace ToDoList.Application.Interfaces;

public interface IAuthService
{
    Task<AuthenticateDTO> Authenticate(LoginDTO loginDto);
}