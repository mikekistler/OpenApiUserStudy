using System.ComponentModel.DataAnnotations;

public class Todo
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
    public DateTime? DueOn { get; set; }
    public DateTime? CompletedOn { get; set; }
}