using System.Collections.Generic;
using ToDoList.Models;
using ToDoList.Interfaces;
using System.Linq;

namespace ToDoList.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        private readonly Context _context;

        public TaskRepository(Context context)
        {
            _context = context;
        }

        public IEnumerable<ToDoItem> GetAll()
        {
            return _context.ToDoItems.ToList();
        }

        public ToDoItem GetById(int? id)
        {
            return _context.ToDoItems.FirstOrDefault(t => t.Id == id);

        }

        public void Create(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            _context.SaveChanges();
        }

        public void Update(int? id, ToDoItem item)
        {
            var todo = _context.ToDoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return;
            }

            todo.IsCompleted = item.IsCompleted;
            todo.Description = item.Description;

            _context.ToDoItems.Update(todo);
            _context.SaveChanges();
         }

        public void Delete(int id)
        {
            var todo = _context.ToDoItems.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return;
            }

            _context.ToDoItems.Remove(todo);
            _context.SaveChanges();
        }
    }
}
