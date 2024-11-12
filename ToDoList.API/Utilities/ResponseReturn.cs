using System.Data;
using ToDoList.API.ViewModels;

namespace ToDoList.API.Utilities;

public static class ResponseReturn
{
    public static ResultViewModel Error(string error) => new ResultViewModel()
    {
        Message = error,
        Success = false,
        Data = null
    };
    public static ResultViewModel DataNotFoundMessage() => new ResultViewModel()
    {
        Message = "Not found",
        Success = false,
        Data = null
    };
    public static ResultViewModel SucessMessage(string message, dynamic data) => new ResultViewModel
    {
        Message = message,
        Success = true,
        Data = data
    };
    
    public static ResultViewModel ApplicatioErrorMessage() => new ResultViewModel
    {
        Message = "Internal Error",
        Success = false,
        Data = null
    };
    
    public static ResultViewModel DomainErrorMessage(string message) => new ResultViewModel
    {
        Message = message,
        Success = false,
        Data = null
    };
    
    public static ResultViewModel DomainErrorMessage(string message, IReadOnlyCollection<string> errors) => new ResultViewModel
    {
        Message = message,
        Success = false,
        Data = errors
    };
    
    public static ResultViewModel UnauthorizedErrorMessage() => new ResultViewModel
    {
        Message = "Login and Password are invalid",
        Success = false,
        Data = null
    };
    
}