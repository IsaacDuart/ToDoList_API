using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;

namespace ToDoList.Application.Interfaces;

public interface IAssignmentService
{
    Task<AssignmentDTO> Create(AssignmentCreateDTO assignmentCreateDto);
    Task<AssignmentDTO> Update(AssignmentUpdateDTO assignmentUpdateDto);
    Task Delete(long id);
    Task FinishTask(long id);
    Task<AssignmentDTO?> AssignmentById(long id);
    Task<IEnumerable<AssignmentDTO>> Assignments();
    Task<IEnumerable<AssignmentDTO>> GetUnfinishedAssignments();
}