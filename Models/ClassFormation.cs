namespace aok_s.Models;
public class ClassFormation
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public int SemesterId { get; set; }
    public Semester Semester { get; set; } = default!;

    public ICollection<Department> Departments { get; set; } = new List<Department>();
}