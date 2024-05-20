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

        // List all todos
        group.MapGet("/", Ok<Todo[]> () =>
        {
            return TypedResults.Ok(todos.Values.ToArray());
        });

        // Get a specific todo by id
        group.MapGet("/{id}", Results<Ok<Todo>, NotFound<ProblemDetails>> (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                return TypedResults.Ok(todos[id]);
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        });

        // Create or replace a new todo
        group.MapPut("/{id}", Results<Ok<Todo>, Created<Todo>> (int id, Todo Todo) =>
        {
            bool exists = todos.ContainsKey(id);
            todos[id] = Todo;
            return exists ? TypedResults.Ok(Todo) : TypedResults.Created($"/{id}", Todo);
        });

        // Delete a todo
        group.MapDelete("/{id}", Results<NoContent, NotFound<ProblemDetails>> (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                todos.Remove(id);
                return TypedResults.NoContent();
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        });

        return group;
    }
}