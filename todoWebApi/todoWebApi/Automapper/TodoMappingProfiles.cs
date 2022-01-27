using AutoMapper;
using todoWebApi.Dtos;
using todoWebApi.Models;

namespace todoWebApi.Automapper
{
    public class TodoMappingProfiles : Profile
    {
        public TodoMappingProfiles()
        {
            CreateMap<TodoCreateDto, Todo>();
            CreateMap<Todo, TodoGetDto>();
        }
    }
}
