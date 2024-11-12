namespace ToDoList.Application.DTO.EntitiesDTOs;

public class AssignmentDTO
{
    public long Id { get; set; }
    public string Description { get; set; } 
    public bool Conclued { get; set; } = false;
    public DateTime ConcluedAt { get; set; }
    public DateTime DeadLine { get; set; }
    public long UserId { get; set; }
    public long? AssignmentListId { get; set; }
}