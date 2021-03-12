using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Test.Core;

namespace Test.Data
{
    public interface ITaskData
    {
        Task<List<Core.Task>> GetAllAsync(string search, int? softwareengineerId);
        Task<Core.Task> GetTaskAsync(int id);
        void Add(Core.Task task);
        void Update(Core.Task task);
        void Delete(Core.Task task);
        Task<int> SaveChangesAsync();


    }
}
