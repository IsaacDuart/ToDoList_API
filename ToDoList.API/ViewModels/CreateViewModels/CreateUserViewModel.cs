using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace ToDoList.API.ModelViews.CreateModelViews;
public class CreateUserViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [MaxLength(100, ErrorMessage = "Name can't have more than 100 characters")]
    [MinLength(2, ErrorMessage = "Name can't have less than 2 characters")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is invalid")]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$"
        , ErrorMessage = "Email is invalid")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{8,}$"
        , ErrorMessage = "Password must contain at least one uppercase letter, one " +
                         "lowercase letter, one digit, and one special character.")]
    public string Password { get; set; }
}