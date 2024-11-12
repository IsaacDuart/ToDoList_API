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
    public class AssignmentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAssignmentService _assignmentService;

        public AssignmentController(IMapper mapper, IAssignmentService assignmentService)
        {
            _mapper = mapper;
            _assignmentService = assignmentService;
        }
        
        [HttpPost]
        [Authorize]
        [Route("/api/v1/assignment/create")]
        public async Task<IActionResult> CreateAssignment([FromBody] CreateAssignmentViewModel createAssignmentViewModel)
        {
            try
            {
                var assignmentDTO = _mapper.Map<AssignmentCreateDTO>(createAssignmentViewModel);
                
                var assignmentCreated = await _assignmentService.Create(assignmentDTO);
                
                return Ok(ResponseReturn.SucessMessage("Assignment created", assignmentCreated));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }

        [HttpPut]
        [Authorize]
        [Route("/api/v1/assignment/update")]
        public async Task<IActionResult> UpdateAssignment([FromBody] UpdateAssignmentViewModel updateAssignmentViewModel)
        {
            try
            {
                var assignmentDTO = _mapper.Map<AssignmentUpdateDTO>(updateAssignmentViewModel);
                
                var assignmentUpdated = await _assignmentService.Update(assignmentDTO);
                
                
                
                return Ok(ResponseReturn.SucessMessage("Assignment updated", assignmentUpdated));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
          
        }

        [HttpDelete]
        [Authorize]
        [Route("/api/v1/assignment/delete/{id}")]
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            try
            {
                await _assignmentService.Delete(id);
                return Ok(ResponseReturn.SucessMessage("Assignment Deleted", null));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }

        [HttpGet]
        [Authorize]
        [Route("/api/v1/assignment/assignmentbyid/{id}")]
        public async Task<IActionResult> GetAssignmentById(int id)
        {
            try
            {
                var assignment = await _assignmentService.AssignmentById(id);
                 
                if (assignment is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                return Ok(ResponseReturn.SucessMessage("Assignment Found", assignment));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/assignment/allassignment")]
        public async Task<IActionResult> AllAssignemnts()
        {
            try
            {
                var assignments = await _assignmentService.Assignments();
                
                if (assignments is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                return Ok(ResponseReturn.SucessMessage("Assignments Found", assignments));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }
        
        [HttpGet]
        [Authorize]
        [Route("/api/v1/assignment/getunfinished")]
        public async Task<IActionResult> UnfinishedAssignments()
        {
            try
            {
                var assignments = await _assignmentService.GetUnfinishedAssignments();

                if (assignments is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }
                return Ok(ResponseReturn.SucessMessage("Assignments Found", assignments));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }

        [Authorize]
        [HttpPatch("{id:long}/conclude")]
        public async Task<IActionResult> ConcludeAssignment(long id)
        {
            try
            {
                var assignment = await _assignmentService.AssignmentById(id);

                if (assignment is null)
                {
                    return BadRequest(ResponseReturn.DataNotFoundMessage());
                }

                if (assignment.DeadLine < DateTime.Now)
                {
                   return BadRequest(ResponseReturn.Error("Deadline has passed"));
                }
                
                await _assignmentService.FinishTask(id);
                return Ok(ResponseReturn.SucessMessage("Assignment Finished", null));
            }
            catch (DomainException ex)
            {
                return BadRequest(ResponseReturn.DomainErrorMessage(ex.Message, ex.Errors));
            }
            catch (Exception)
            {
                return StatusCode(500, ResponseReturn.ApplicatioErrorMessage());
            }
        }
        
        
    }
}
