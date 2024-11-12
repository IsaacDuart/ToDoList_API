namespace ToDoList.API.ModelViews.CreateModelViews;
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

public class CreateAssignmentViewModel
{
    [MaxLength(150, ErrorMessage = "Maximum length is 150 characters")]
    [Required(ErrorMessage = "Please enter a description")]
    public string Description { get; set; } 
    
    [Required(ErrorMessage = "Please enter a Deadline")]
    public DateTime DeadLine { get; set; }
    
    [Required(ErrorMessage = "Please enter userId")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter valid number")]
    public long UserId { get; set; }
    
    [Required(ErrorMessage = "Please enter assignmentListId")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter valid number")]
    public long? AssignmentListId { get; set; }
    
    
}