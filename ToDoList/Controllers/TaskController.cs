using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using ToDoList.Interfaces;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private readonly ITaskRepository _repository;
        private readonly ILogger _logger;
        private readonly ClaimsPrincipal _caller;
        private readonly Context _appDbContext;

        public TaskController(ITaskRepository repo, ILogger<TaskController> logger, UserManager<User> userManager, Context appDbContext, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repo;
            _logger = logger;
            _caller = httpContextAccessor.HttpContext.User;
            _appDbContext = appDbContext;
        }


        [HttpGet]
        public async Task<IEnumerable<ToDoItem>> GetAll()
        {
            var userId = _caller.Claims.Single(c => c.Type == "id").Value; 
           
            return await _repository.GetAllAsync(userId);
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Getting item {id}");

            var item = await _repository.GetByIdAsync(id);

            if (item == null)
            {
                _logger.LogWarning($"Item {id} not found");
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ToDoItem item)
        {
            var userId = _caller.Claims.Single(c => c.Type == "id").Value;
            item.IdentityId = userId;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _repository.CreateAsync(item);
            await _repository.SaveAsync();

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, [FromBody] ToDoItem item)
        {
            if (!ModelState.IsValid || id == null)
            {
                return BadRequest();
            }

            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            var userId = _caller.Claims.Single(c => c.Type == "id").Value;
            item.IdentityId = userId;

            await _repository.UpdateAsync(id, item);
            await _repository.SaveAsync();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _repository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _repository.DeleteAsync(id);
            await _repository.SaveAsync();
            return new NoContentResult();
        }


    }
}