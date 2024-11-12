using AutoMapper;
using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;

namespace ToDoList.Application.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IMapper _mapper;
    private readonly IAssignmentRepository _assignmentRepository;

    public AssignmentService(IMapper mapper, IAssignmentRepository assignmentRepository)
    {
        _mapper = mapper;
        _assignmentRepository = assignmentRepository;
    }
    
    
    public async Task<AssignmentDTO> Create(AssignmentCreateDTO assignmentCreateDto)
    {
        var assignment = _mapper.Map<Assignment>(assignmentCreateDto);
        assignment.Validate();
        
        return _mapper.Map<AssignmentDTO>(await _assignmentRepository.Create(assignment));
    }

    public async Task<AssignmentDTO> Update(AssignmentUpdateDTO assignmentUpdateDto)
    {
        var assignment = _mapper.Map<Assignment>(assignmentUpdateDto);
        assignment.Validate();
        
        return _mapper.Map<AssignmentDTO>(await _assignmentRepository.Update(assignment));
    }

    public async Task Delete(long id)
    {
        var assignment  = await _assignmentRepository.GetById(id);
        if (assignment == null) throw new DomainException("Assignment not found");
        
        await _assignmentRepository.Delete(id);
    }

    public async Task FinishTask(long id)
    {
        var assignment = await _assignmentRepository.GetById(id);

        if (assignment.Conclued == true) return;
        
        assignment.FinishTask();
        await _assignmentRepository.Update(assignment);
    }

    public async Task<AssignmentDTO?> AssignmentById(long id) 
        => _mapper.Map<AssignmentDTO>(await _assignmentRepository.GetById(id));

    public async Task<IEnumerable<AssignmentDTO>> Assignments() 
        => _mapper.Map<IEnumerable<AssignmentDTO>>(await _assignmentRepository.GetAll());
    
    public async Task<IEnumerable<AssignmentDTO>> GetUnfinishedAssignments() 
        => _mapper.Map<IEnumerable<AssignmentDTO>>(await _assignmentRepository.GetUnfinishedAssignments());
}