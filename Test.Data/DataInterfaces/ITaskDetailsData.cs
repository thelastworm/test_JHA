using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Data
{
    public interface ITaskDetailsData
    {
        Task<IEnumerable<TaskDetails>> GetAllAsync(string search, int? taskId);
        Task<TaskDetails> GetTaskDetailsAsync(int id);
        void Add(TaskDetails taskDetails);
        void Update(TaskDetails taskDetails);
        void Delete(TaskDetails taskDetails);
        Task<int> SaveChangesAsync();


    }
}
