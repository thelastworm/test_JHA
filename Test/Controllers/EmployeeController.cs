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
    public class EmployeeController : ControllerBase
    {


        
        private readonly IMapper _mapper;
        private readonly IEmployeeData _repository;


        public EmployeeController(IMapper mapper, IEmployeeData repository)
        {
            
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeModel[]>> Get(string search = null, string roleId = null)
        {
            try
            {
                List<Employee> results = await _repository.GetAllAsync(search, roleId);
                EmployeeModel[] models = _mapper.Map<EmployeeModel[]>(results);
                return models;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmployeeModel>> Get(int id)
        {
            try
            {
                Employee result = await _repository.GetEmployeeAsync(id);
                if (result == null) return NotFound();
                EmployeeModel model = _mapper.Map<EmployeeModel>(result);
                return model;
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        

        [HttpPost]
        public async Task<ActionResult<EmployeeModel>> Post(EmployeeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var employee = _mapper.Map<Employee>(model);
                _repository.Add(employee);
                if (await _repository.SaveChangesAsync() == 1)
                {
                    return Created($"/api/employee/{employee.Id}", _mapper.Map<EmployeeModel>(employee));
                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError);

            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<EmployeeModel>> Put(int id, EmployeeModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var oldEmployee = await _repository.GetEmployeeAsync(id);
                if (oldEmployee == null) return NotFound();
                _mapper.Map(model, oldEmployee);

                if (await _repository.SaveChangesAsync() == 1)
                {
                    return _mapper.Map<EmployeeModel>(oldEmployee);
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

                var oldEmployee = await _repository.GetEmployeeAsync(id);
                if (oldEmployee == null) return NotFound();
                _repository.Delete(oldEmployee);
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
