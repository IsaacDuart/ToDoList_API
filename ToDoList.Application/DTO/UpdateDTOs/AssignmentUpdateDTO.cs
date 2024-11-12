namespace ToDoList.Application.DTO.UpdateDTOs;

public class AssignmentUpdateDTO
{
    public long Id { get; set; }
    public string Description { get; set; } 
    public DateTime DeadLine { get; set; }
    public long UserId { get; set; }
    public long? AssignmentListId { get; set; }
}