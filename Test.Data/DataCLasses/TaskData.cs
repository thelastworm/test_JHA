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
    public class Taskdata : ITaskData
    {

        private TestDbContext _db;

        public Taskdata(TestDbContext db)
        {
            _db = db;
        }

        public void Add(Test.Core.Task task)
        {
            _db.Add(task);
        }

        public void Delete(Test.Core.Task task)
        {
            _db.Remove(task);
        }

        public async Task<List<Test.Core.Task>> GetAllAsync(string search, int? softwareengineerId)
        {
            if (softwareengineerId != null)
                return await _db.Tasks.Include(i=> i.Employee).Include(i => i.Tasks).Where( i => i.AssignedForId==softwareengineerId && (i.Type.Contains(search) || i.Name.Contains(search) || search == null)).ToListAsync();
            else
                return await _db.Tasks.Include(i => i.Employee).Include(i => i.Tasks).Where(i => i.Type.Contains(search) || i.Name.Contains(search) || search == null).ToListAsync();

        }

        public Task<Test.Core.Task> GetTaskAsync(int id)
        {
            return   _db.Tasks.Where(i => i.Id == id).FirstOrDefaultAsync();

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Update(Test.Core.Task task)
        {
            throw new NotImplementedException();
        }
    }
}

        
       
 
