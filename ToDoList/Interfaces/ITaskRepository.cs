using System;
using System.Collections.Generic;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITaskRepository
    {
        IEnumerable<ToDoItem> GetAll();
        ToDoItem GetById(int id);
        void Create(ToDoItem item);
        void Update(int id, ToDoItem item);
        void Delete(int id);
    }
}