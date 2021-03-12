using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Test.Core;
using Test.Data;
using Test.Models;

namespace Test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {



        private readonly IMapper _mapper;
        private readonly ITaskData _repository;


        public TaskController(IMapper mapper, ITaskData repository)
        {

            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<TaskModel[]>> Get(string search = null, int? softwareengineerId=null)
        {
            try
            {
                var results = await _repository.GetAllAsync(search,softwareengineerId);
                TaskModel[] models = _mapper.Map<TaskModel[]>(results);
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
                var result = await _repository.GetTaskAsync(id);
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
        public async Task<ActionResult<TaskModel>> Post(TaskModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var task = _mapper.Map<Test.Core.Task>(model);
                _repository.Add(task);
                if (await _repository.SaveChangesAsync() == 1)
                {
                    return Created($"/api/task/{task.Id}", _mapper.Map<TaskModel>(task));
                }

        }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError);

    }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Put(int id, TaskModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var oldTask = await _repository.GetTaskAsync(id);
                if (oldTask == null) return NotFound();
                _mapper.Map(model, oldTask);

                if (await _repository.SaveChangesAsync() == 1)
                {
                    return _mapper.Map<TaskModel>(oldTask);
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

                var oldTask = await _repository.GetTaskAsync(id);
                if (oldTask == null) return NotFound();
                _repository.Delete(oldTask);
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
