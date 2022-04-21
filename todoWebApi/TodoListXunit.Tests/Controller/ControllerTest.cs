using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoWebApi.Controllers;
using todoWebApi.Dtos;
using todoWebApi.Models;
using todoWebApi.Repositories;
using Xunit;

namespace TodoListXunit.Tests.Controller
{
    public  class ControllerTest
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public ControllerTest()
        {
            _todoRepository = A.Fake<ITodoRepository>();
            _mapper = A.Fake<IMapper>();
        }
        [Fact]
        public async Task TodoController_GetAllTodo_ReturnOkAsync()
        {
            //Arrange
            var todos = A.Fake<ICollection<TodoGetDto>>();

            var todoList = A.Fake<List<TodoGetDto>>();

            A.CallTo(() => _mapper.Map<List<TodoGetDto>>(todos)).Returns(todoList);

            var controller = new TodoController(_todoRepository, _mapper);

            //Act
            var result = await controller.GetAllTodo();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));
        }        

        [Fact]
        public async Task TodoController_GetTodoById_ReturnOkAsync()
        {
            //Arrange

            int todoId = 1;
            var todoMap = A.Fake<Todo>();
            var todo = A.Fake<Todo>();
            var todoCreate = A.Fake<TodoCreateDto>();
            var todos = A.Fake<ICollection<TodoGetDto>>();
            var todoList = A.Fake<List<TodoGetDto>>();

            A.CallTo(() => _mapper.Map<Todo>(todoCreate)).Returns(todo);
            A.CallTo(() => _todoRepository.CreateTodoAsync(todo)).Returns(todo);


            var controller = new TodoController(_todoRepository, _mapper);
            ////Act
            var resut = controller.CreateTodo(todoCreate);

            //Assert

            resut.Should().NotBeNull();

        }
    }
}
