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
    public class TaskDetailsData : ITaskDetailsData
    {

        private TestDbContext _db;

        public TaskDetailsData(TestDbContext db)
        {
            _db = db;
        }

        public void Add(TaskDetails taskDetails)
        {
            _db.Add(taskDetails);
        }

        public void Delete(TaskDetails taskDetails)
        {
            _db.Remove(taskDetails);
        }

        public async Task<IEnumerable<TaskDetails>> GetAllAsync(string search, int? taskId)
        {
            if (taskId != null)
                return await _db.TaskDetails.Where(i => (i.Description.Contains(search) && i.TaskId == taskId) || (search == null && i.TaskId == taskId)).ToListAsync();
            else
                return await _db.TaskDetails.Where(i => (i.Description.Contains(search)) || search == null).ToListAsync();


        }

        public Task<TaskDetails> GetTaskDetailsAsync(int id)
        {
            return _db.TaskDetails.Where(i => i.Id == id).FirstOrDefaultAsync();

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }

        public void Update(TaskDetails taskDetails)
        {
            throw new NotImplementedException();
        }
    }
}




