using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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

        group.MapGet("/", Ok<Todo[]> (int? offset, int? limit) =>
        {
            return TypedResults.Ok(todos.Values.Skip(offset ?? 0).Take(limit ?? int.MaxValue).ToArray());
        })
        .WithName("ListTodos")
        .WithOpenApi(operation =>
        {
            operation.Description = "List all Todo items.";
            var offsetParam = operation.Parameters.First(p => p.Name == "offset");
            offsetParam.Description = "Offset of first item";
            var limitParam = operation.Parameters.First(p => p.Name == "limit");
            limitParam.Description = "Maximum number of items to return";
            return operation;
        });

        group.MapGet("/{id}", Results<Ok<Todo>, NotFound<ProblemDetails>> (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                return TypedResults.Ok(todos[id]);
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        })
        .WithName("GetTodo")
        .WithOpenApi(operation =>
        {
            operation.Description = "Get a Todo item.";
            var idParam = operation.Parameters.First(p => p.Name == "id");
            idParam.Description = "The ID of the Todo to return.";
            return operation;
        });

        group.MapPut("/{id}", Results<Ok<Todo>, Created<Todo>> (int id, Todo Todo) =>
        {
            bool exists = todos.ContainsKey(id);
            todos[id] = Todo;
            return exists ? TypedResults.Ok(Todo) : TypedResults.Created($"/{id}", Todo);
        })
        .WithName("CreateOrReplaceTodo")
        .WithOpenApi(operation =>
        {
            operation.Description = "Create or replace a Todo item.";
            var idParam = operation.Parameters.First(p => p.Name == "id");
            idParam.Description = "The ID of the Todo to create or replace.";
            return operation;
        });

        group.MapDelete("/{id}", Results<NoContent, NotFound<ProblemDetails>> (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                todos.Remove(id);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        })
        .WithName("DeleteTodo")
        .WithOpenApi(operation =>
        {
            operation.Description = "Delete a Todo item.";
            var idParam = operation.Parameters.First(p => p.Name == "id");
            idParam.Description = "The ID of the Todo to delete.";
            return operation;
        });

        return group;
    }
}