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

        group.MapGet("/", Ok<Todo[]> () =>
        {
            return TypedResults.Ok(todos.Values.ToArray());
        });


        group.MapGet("/{id}", Results<Ok<Todo>, NotFound<ProblemDetails>> (int id) =>
        {
            if (todos.ContainsKey(id))
            {
                return TypedResults.Ok(todos[id]);
            }
            return TypedResults.NotFound<ProblemDetails>(null);
        });

        group.MapPut("/{id}", Results<Ok<Todo>, Created<Todo>> (int id, Todo Todo) =>
        {
            bool exists = todos.ContainsKey(id);
            todos[id] = Todo;
            return exists ? TypedResults.Ok(Todo) : TypedResults.Created($"/{id}", Todo);
        });

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