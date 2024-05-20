using System.ComponentModel.DataAnnotations;

// A Todo item.
public class Todo
{
    // Id of the Todo item.
    public int Id { get; set; }

    // Title of the Todo item.
    [Required]
    public string Title { get; set; } = default!;

    // Content of the Todo item.
    public string Content { get; set; } = default!;

    // Due date of the Todo item.
    public string? DueOn { get; set; }

    // Completion date of the Todo item.
    public string? CompletedOn { get; set; }
}
