using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDetailsController : ControllerBase
    {
        private readonly ITaskDetailsData _repository;
        private readonly IMapper _mapper;
        private readonly ITaskData _taskRepository;

        public TaskDetailsController(ITaskDetailsData repository, IMapper mapper,ITaskData taskRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<TaskDetailsModel[]>> Get(string search = null, int? taskId = null)
        {
            try
            {
                 

                var results = await _repository.GetAllAsync(search, taskId);
                TaskDetailsModel[] models = _mapper.Map<TaskDetailsModel[]>(results);
                return models;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskModel>> Get(int id)
        {
            try
            {

                var result = await _repository.GetTaskDetailsAsync(id);

                if (result == null) return NotFound();
                TaskModel model = _mapper.Map<TaskModel>(result);
                return model;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost]
        public async Task<ActionResult<TaskDetailsModel>> Post(TaskDetailsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var task = await _taskRepository.GetTaskAsync(model.TaskId.GetValueOrDefault());
                if (task == null)
                {
                    return NotFound("Task not Found");
                }



                var taskDetails = _mapper.Map<Test.Core.TaskDetails>(model);
                _repository.Add(taskDetails);
                if (await _repository.SaveChangesAsync() == 1)
                {
                    return Created($"/api/taskdetails/{taskDetails.Id}", _mapper.Map<TaskDetailsModel>(taskDetails));
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError);

            }

            return BadRequest();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDetailsModel>> Put(int id, TaskDetailsModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var task = await _taskRepository.GetTaskAsync(model.TaskId.GetValueOrDefault());
                if (task == null)
                {
                    return NotFound("Task not Found");
                }
                var oldTask = await _repository.GetTaskDetailsAsync(id);
                if (oldTask == null) return NotFound();
                _mapper.Map(model, oldTask);

                if (await _repository.SaveChangesAsync() == 1)
                {
                    return _mapper.Map<TaskDetailsModel>(oldTask);
                }
                return BadRequest();
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError);

            }

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {

                var oldTaskDetail = await _repository.GetTaskDetailsAsync(id);
                if (oldTaskDetail == null) return NotFound();

                var task = await _taskRepository.GetTaskAsync(oldTaskDetail.TaskId.GetValueOrDefault());
                if (task == null)
                {
                    return NotFound("Task not Found");
                }

                _repository.Delete(oldTaskDetail);
                if (await _repository.SaveChangesAsync() == 1)
                {
                    return Ok();
                }
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest("Failed to delete");
        }

    }
}
