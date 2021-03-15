using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test.Core;
using Test.Models;

namespace Test
{
    public class TestProfile : Profile
    {
        public TestProfile()
        {
            this.CreateMap<Employee, EmployeeModel>();
            this.CreateMap<EmployeeModel, Employee>();
            this.CreateMap<Core.Task, TaskModel>().ForMember(e => e.SoftwareEngineerFirstName, o => o.MapFrom(m => m.Employee.FirstName))
                                                  .ForMember(e => e.SoftwareEngineerLastName, o => o.MapFrom(m => m.Employee.LastName))
                                                  .ForMember(e => e.SoftwareEngineerId, o => o.MapFrom(m => m.AssignedForId));
                                                  

            this.CreateMap<TaskModel,Core.Task > ().ForMember(e => e.AssignedForId, o => o.MapFrom(m => m.SoftwareEngineerId));

            this.CreateMap<TaskDetailsModel, TaskDetails>();
            this.CreateMap<TaskDetails,TaskDetailsModel>();


        }
    }
}
