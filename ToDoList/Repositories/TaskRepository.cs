using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ToDoList.Repositories
{
    public class TaskRepository: ITaskRepository
    {
        private readonly Context _context;

        public TaskRepository(Context context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ToDoItem>> GetAllAsync(string userId)
        {
            return await _context.ToDoItems.Where(u => u.IdentityId == userId).ToListAsync();
        }

        public async Task<ToDoItem> GetByIdAsync(int? id)
        {
            return await _context.ToDoItems.FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task CreateAsync(ToDoItem item)
        {
            await _context.ToDoItems.AddAsync(item);
        }

        public async Task UpdateAsync (int? id, ToDoItem item)
        {
            var todo = await _context.ToDoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
            {
                return;
            }

            todo.IsCompleted = item.IsCompleted;
            todo.Description = item.Description;

            _context.ToDoItems.Update(todo);
         }

        public async Task DeleteAsync(int id)
        {
            var todo = await _context.ToDoItems.FirstOrDefaultAsync(t => t.Id == id);
            if (todo == null)
            {
                return;
            }

            _context.ToDoItems.Remove(todo);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
