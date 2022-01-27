using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using todoWebApi.Dtos;
using todoWebApi.Models;
using todoWebApi.Repositories;

namespace todoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {

        private readonly ITodoRepository _todosRepo;
        private readonly IMapper _mapper;
        public TodoController(ITodoRepository repo, IMapper mapper)
        {
            _todosRepo = repo;
            _mapper = mapper;
        }
        [HttpGet("Get")]
        public async Task<IActionResult> GetAllTodo()
        {
            var todo = await _todosRepo.GetAllTodosAsync();
            var todoGet = _mapper.Map<List<TodoGetDto>>(todo);

            return Ok(todoGet);
        }
        [Route("Get/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetTodoById(int id)
        {
            var todo = await _todosRepo.GetTodoByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            var todoGet = _mapper.Map<TodoGetDto>(todo);
            return Ok(todoGet);
        }
        [HttpPost("Post")]
        public async Task<IActionResult> CreateTodo([FromBody] TodoCreateDto todo)
        {
            var domainTodo = _mapper.Map<Todo>(todo);
            await _todosRepo.CreateTodoAsync(domainTodo);
            var todoGet = _mapper.Map<TodoGetDto>(domainTodo);
            return CreatedAtAction(nameof(GetTodoById), new { id = domainTodo.TodoId }, todoGet);
        }
        [HttpPut]
        [Route("Put/{id}")]
        public async Task<IActionResult> UpdateTodo([FromBody] TodoCreateDto updated, int id)
        {
            var toUpdate = _mapper.Map<Todo>(updated);
            toUpdate.TodoId = id;
            await _todosRepo.UpdateTodoAsync(toUpdate);
            return NoContent();
        }
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _todosRepo.DeleteTodoAsync(id);

            if (todo == null)
            {
                return NotFound();
            }
            return NoContent();
        }

    }
}
