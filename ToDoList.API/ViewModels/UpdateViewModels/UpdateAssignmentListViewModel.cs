namespace ToDoList.API.ViewModels.UpdateViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;



public class UpdateAssignmentListViewModel
{
    [Required(ErrorMessage = "Please enter a Id")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter valid number")]
    public long Id { get; set; }
    
    [Required(ErrorMessage = "Please enter a name" )]
    public string Name { get; set; }
    
    
    [Required(ErrorMessage = "Please enter a Id")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter valid number")]
    public long UserId { get; set; }
}