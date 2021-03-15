using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Data
{
    public interface IEmployeeData
    {
        Task<List<Employee>> GetAllAsync(string search, string roleId);
        Task<Employee> GetEmployeeAsync(int id);
        void Add(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        Task<int> SaveChangesAsync();

    }
}
