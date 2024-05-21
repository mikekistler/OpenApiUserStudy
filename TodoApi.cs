using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

internal static class TodosApi
{
    private static Dictionary<int, Todo> todos = new Todo[]
    {
        new Todo { Id = 1, Title = "First Todo", Content = "This is the first todo"},
        new Todo { Id = 2, Title = "Second Todo", Content = "This is the second todo"},
    }.ToDictionary(todo => todo.Id);

    public static RouteGroupBuilder MapTodos(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/todos");

        group.WithTags("Todos");

        // List all todos
        group.MapGet("/", Ok<Todo[]> ([Description("Offset of first item")] int? offset, [Description("Maximum number of items to return")] int? limit) =>
        {
            return TypedResults.Ok(todos.Values.Skip(offset ?? 0).Take(limit ?? int.MaxValue).ToArray());
        })
        .WithDescription("List all todos")
        .WithName("ListTodos");

        // Get a specific todo by id
        group.MapGet("/{id}", Results<Ok<Todo>, NotFound<ProblemDetails>> ([Description("The ID of the todo to get")] int id) =>
        {
            if (todos.ContainsKey(id))
            {
                return TypedResults.Ok(todos[id]);
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        })
        .WithDescription("Get a specific todo by id")
        .WithName("GetTodo");

        // Create or replace a new todo
        group.MapPut("/{id}", Results<Ok<Todo>, Created<Todo>> ([Description("The ID of the todo to create")] int id, [Description("The todo item contents")] Todo Todo) =>
        {
            bool exists = todos.ContainsKey(id);
            todos[id] = Todo;
            return exists ? TypedResults.Ok(Todo) : TypedResults.Created($"/{id}", Todo);
        })
        .WithDescription("Create or replace a new todo")
        .WithName("CreateTodo");

        // Delete a todo
        group.MapDelete("/{id}", Results<NoContent, NotFound<ProblemDetails>> ([Description("The ID of the todo to delete")] int id) =>
        {
            if (todos.ContainsKey(id))
            {
                todos.Remove(id);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        })
        .WithDescription("Delete a todo")
        .WithName("DeleteTodo");

        return group;
    }
}