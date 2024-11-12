namespace ToDoList.Application.DTO.CreateDTOs;

public class AssignmentCreateDTO
{
    public string Description { get; set; } 
    public DateTime DeadLine { get; set; }
    public long UserId { get; set; }
    public long? AssignmentListId { get; set; }
}