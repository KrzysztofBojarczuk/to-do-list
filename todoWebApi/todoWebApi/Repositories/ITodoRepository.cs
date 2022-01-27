using todoWebApi.Models;

namespace todoWebApi.Repositories
{
    public interface ITodoRepository
    {
        Task<List<Todo>> GetAllTodosAsync();
        Task<Todo> GetTodoByIdAsync(int id);
        Task<Todo> CreateTodoAsync(Todo todo);
        Task<Todo> UpdateTodoAsync(Todo updatedTodo);
        Task<Todo> DeleteTodoAsync(int id);
    }
}
