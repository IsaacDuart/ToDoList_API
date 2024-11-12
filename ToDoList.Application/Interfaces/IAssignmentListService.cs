using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;

namespace ToDoList.Application.Interfaces;

public interface IAssignmentListService
{
    Task<AssignmentListDTO> Create(AssignmentListCreateDTO assignmentListCreateDto);
    Task<AssignmentListDTO> Update(AssignmentListUpdateDTO assignmentListUpdateDto);
    Task Delete(long id);
    Task<AssignmentListDTO?> AssignmentListById(long id);
    Task<IEnumerable<AssignmentListDTO>> AssignmentLists(); 
    Task<IEnumerable<AssignmentListDTO>> SearchByListName(string name);
}