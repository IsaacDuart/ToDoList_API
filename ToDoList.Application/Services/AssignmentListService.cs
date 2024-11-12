using AutoMapper;
using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.EntitiesDTOs;
using ToDoList.Application.DTO.UpdateDTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;
using ToDoList.Domain.Interfaces.RepositoryPatternEntities;

namespace ToDoList.Application.Services;

public class AssignmentListService : IAssignmentListService
{
    private readonly IMapper _mapper;
    private readonly IAssignmentListRepository _assignmentListRepository;

    public AssignmentListService(IMapper mapper, IAssignmentListRepository assignmentListRepository)
    {
        _mapper = mapper;
        _assignmentListRepository = assignmentListRepository;
    }

    public async Task<AssignmentListDTO> Create(AssignmentListCreateDTO assignmentListCreateDto)
    {
        var assignmentList = _mapper.Map<AssignmentList>(assignmentListCreateDto);
        assignmentList.Validate();
        
        return _mapper.Map<AssignmentListDTO>(await _assignmentListRepository.Create(assignmentList));
    }

    public async Task<AssignmentListDTO> Update(AssignmentListUpdateDTO assignmentListUpdateDto)
    {
        var assignmentList = _mapper.Map<AssignmentList>(assignmentListUpdateDto);
        assignmentList.Validate();
        
        return _mapper.Map<AssignmentListDTO>(await _assignmentListRepository.Update(assignmentList));
    }

    public async Task Delete(long id)
    {
        var assignmentList  = await _assignmentListRepository.GetById(id);
        if (assignmentList == null) throw new DomainException("AssignmentList not found");
        
        await _assignmentListRepository.Delete(id);
    }

    public async Task<AssignmentListDTO?> AssignmentListById(long id) 
        => _mapper.Map<AssignmentListDTO>(await _assignmentListRepository.GetById(id));

    public async Task<IEnumerable<AssignmentListDTO>> AssignmentLists() 
        => _mapper.Map<IEnumerable<AssignmentListDTO>>(await _assignmentListRepository.GetAll());

    public async Task<IEnumerable<AssignmentListDTO>> SearchByListName(string name) 
        => _mapper.Map<IEnumerable<AssignmentListDTO>>(await _assignmentListRepository.SearchByListName(name));
}