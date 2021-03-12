using System;
using System.Collections.Generic;
using System.Text;
using Test.Core;
using Test.Data;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace test.data
{
    public class Employeedata : IEmployeeData
    {

        private TestDbContext _db;

        public Employeedata(TestDbContext db)
        {
            _db = db;
        }

        public void Add(Employee employee)
        {
            _db.Add(employee);
        }

        public void Delete(Employee employee)
        {
            _db.Remove(employee);
        }

        public async Task<List<Employee>> GetAllAsync(string search)
        {
            return await _db.Employees.Where( i => i.FirstName.Contains(search) || i.LastName.Contains(search) || search == null).ToListAsync();
        }

        public Task<Employee> GetEmployeeAsync(int id)
        {
            return   _db.Employees.Where(i => i.Id == id).FirstOrDefaultAsync();

        }

        public async Task<int> SaveChangesAsync()
        {
           return await _db.SaveChangesAsync();
        }

        public void Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}

        
       
 
