using Microsoft.AspNetCore.Http.HttpResults;

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

        group.MapGet("/", () =>
        {
            return Results.Ok(todos);
        });

        group.MapGet("/{id}", (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                return Results.Ok(todos[id]);
            }
            return Results.NotFound();
        });

        group.MapPut("/{id}", (int id, Todo Todo) =>
        {
            todos[id] = Todo;
            return Results.Ok(Todo);
        });

        group.MapDelete("/{id}", (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                todos.Remove(id);
                return Results.NoContent();
            }
            return Results.NotFound();
        });

        return group;
    }
}