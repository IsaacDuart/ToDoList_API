using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.ModelViews.CreateModelViews;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels.UpdateViewModels;
using ToDoList.Application.DTO.CreateDTOs;
using ToDoList.Application.DTO.UpdateDTOs;
using ToDoList.Application.Interfaces;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Exception;

namespace ToDoList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignmentListController : ControllerBase
    {
        
        private readonly IMapper _mapper;
        private readonly IAssignmentListService _assignmentListService;
        
        public AssignmentListController(IMapper mapper, IAssignmentListService assignmentListService)
        {
            _mapper = mapper;
            _assignmentListService = assignmentListService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("/api/v1/assignmentlist/create")]
        public async Task<IActionResult> Create([FromBody] CreateAssignmentListViewModel createAssignmentListViewModel)
        {
            try
            {
                var assignmentListDto = _mapper.Map<AssignmentListCreateDTO>(createAssignmentListViewModel);

                var assignmentListCreated = await _assignmentListService.Create(assignmentListDto);
                
                return Ok(ResponseReturn.SucessMessage("Assignment list created successfully", assignmentListCreated));
            }
            
            catch (DomainException e)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(e.Message, e.Errors));
            }
            
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
           
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/assignmentlist/update")]
        public async Task<IActionResult> Update([FromBody] UpdateAssignmentListViewModel updateAssignmentListViewModel)
        {
            try
            {
                var assignmentListDto = _mapper.Map<AssignmentListUpdateDTO>(updateAssignmentListViewModel);
                
                var assignmentListUpdated = await _assignmentListService.Update(assignmentListDto);
                
                return Ok(ResponseReturn.SucessMessage("Assignment list updated successfully", assignmentListDto));
            }
            catch (DomainException e)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
            
            
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/v1/assignmentlist/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _assignmentListService.Delete(id);

                return Ok(ResponseReturn.SucessMessage("Assignment list deleted successfully", null));
            }
            catch (DomainException e)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception)
            {
               return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
            
        }
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/assignmentlist/getbyid/{id}")]
        public async Task<IActionResult> GetAssignmentListById(int id)
        {
            try
            {

                var assignmentList = await _assignmentListService.AssignmentListById(id);

                if (assignmentList is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("Assignment list retrieved successfully", assignmentList));
            }
            catch (DomainException e)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/assignmentlist/getall")]
        public async Task<IActionResult> GetAllAssignmentLists()
        {
            try
            {
                var assignmentLists = await _assignmentListService.AssignmentLists();

                if (assignmentLists is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("Assignment lists retrieved successfully", assignmentLists));
            }
            catch (DomainException e)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/assignmentlist/searchbylistname/{name}")]
        public async Task<IActionResult> GetAssignmentListByListName(string name)
        {
            try
            {
                var assignmentList = await _assignmentListService.SearchByListName(name);

                if (assignmentList is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                
                return Ok(ResponseReturn.SucessMessage("Assignment list retrieved successfully", assignmentList));
            }
            catch (DomainException e)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(e.Message, e.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }
        
        
    }
}
