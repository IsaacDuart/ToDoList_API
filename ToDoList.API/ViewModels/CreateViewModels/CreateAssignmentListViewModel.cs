namespace ToDoList.API.ModelViews.CreateModelViews;
using System.ComponentModel.DataAnnotations;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
public class CreateAssignmentListViewModel
{
    [Required(ErrorMessage = "Please enter a name" )]
    public string Name { get; set; }
    
    
    [Required(ErrorMessage = "Please enter a Id")]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter valid number")]
    public long UserId { get; set; }
}