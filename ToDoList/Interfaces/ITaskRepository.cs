using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<ToDoItem>> GetAllAsync(string userId);
        Task<ToDoItem> GetByIdAsync(int? id);
        Task CreateAsync(ToDoItem item);
        Task UpdateAsync(int? id, ToDoItem item);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}