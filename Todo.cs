using System.ComponentModel.DataAnnotations;

/// <summary>
/// A Todo item.
/// </summary>
public class Todo
{
    /// <summary>
    /// Id of the Todo item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Title of the Todo item.
    /// </summary>
    [Required]
    public string Title { get; set; } = default!;
    /// <summary>
    /// Content of the Todo item.
    /// </summary>
    public string Content { get; set; } = default!;
    /// <summary>
    /// Due date of the Todo item.
    /// </summary>
    public string? DueOn { get; set; }
    /// <summary>
    /// Completion date of the Todo item.
    /// </summary>
    public string? CompletedOn { get; set; }
}