using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todoWebApi.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using todoWebApi.Models;
using todoWebApi.Repositories;
using Xunit;
using FluentAssertions;

namespace TodoListXunit.Tests.Repository
{
    public class TodoEfTest
    {
        private async Task<DataContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new DataContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.Todos.CountAsync() <= 0)
            {
                for (int i = 1; i < 10; i++)
                {
                    databaseContext.Todos.Add(
                        new Todo()
                        {
                            Comment = "Nauka Testów"

                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;


        }
        [Fact]
        public async Task GetTodoById()
        {
            //Arrange
           var todo = new Todo()
           {
               Comment = "Nauka Testów"
           };

            //Act 

            var dbContext = await GetDatabaseContext();
            var todoRepository = new TodoRepository(dbContext);

            //Assert

            var result = todoRepository.CreateTodoAsync(todo);

            //Assert
            result.Should();

        }
        [Fact]
        public async void TodoRepository_GetPokemonRating_ReturnDecimalBetweenOneAndTen()
        {
            //Arrange
            var todoId = 1;
            var dbContext = await GetDatabaseContext();
            var todoRepository = new TodoRepository(dbContext);


            //Act
            var result = todoRepository.GetTodoByIdAsync(todoId);


            //Assert
            result.Should().NotBe(0);
  
        }
    }
}
