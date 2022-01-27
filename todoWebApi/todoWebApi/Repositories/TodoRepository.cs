using Microsoft.EntityFrameworkCore;
using todoWebApi.Data;
using todoWebApi.Models;

namespace todoWebApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {

        private readonly DataContext _ctx;
        public TodoRepository(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<Todo> CreateTodoAsync(Todo todo)
        {
            _ctx.Todos.Add(todo);
            await _ctx.SaveChangesAsync();
            return todo;
        }

        public async Task<Todo> DeleteTodoAsync(int id)
        {
            var todo = await _ctx.Todos.SingleOrDefaultAsync(h => h.TodoId == id);

            if (todo == null)
            {
                return null;
            }
            _ctx.Todos.Remove(todo);
            await _ctx.SaveChangesAsync();
            return todo;
        }

        public async Task<List<Todo>> GetAllTodosAsync()
        {
            return await _ctx.Todos.ToListAsync();
        }

        public async Task<Todo> GetTodoByIdAsync(int id)
        {
            var todo = await _ctx.Todos.FirstOrDefaultAsync(h => h.TodoId == id);
            if (todo == null)
            {
                return null;
            }
            return todo;
        }

        public async Task<Todo> UpdateTodoAsync(Todo updatedTodo)
        {
            _ctx.Todos.Update(updatedTodo);
            await _ctx.SaveChangesAsync();
            return updatedTodo;
        }
    }
}
