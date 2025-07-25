namespace aok_s.Models;
public class Department
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // public int SemesterId { get; set; }
    // public Semester Semester { get; set; } = default!;

    public int ClassFormationId { get; set; }
    public ClassFormation ClassFormation { get; set; } = default!;

    public ICollection<Major> Majors { get; set; } = new List<Major>();
}