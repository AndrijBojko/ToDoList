using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Repositories;
using ToDoList.Interfaces;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;

        public TaskController(Context context)
        {
            _repository = new TaskRepository(context);
        }

        [HttpGet]
        public IEnumerable<ToDoItem> GetAll()
        {
            return _repository.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(int id)
        {
            var item = _repository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ToDoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            _repository.Create(item);

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ToDoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var task = _repository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }

            _repository.Update(id, item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var task = _repository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }

            _repository.Delete(id);
            return new NoContentResult();
        }


    }
}