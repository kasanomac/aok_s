public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; } = string.Empty; //学部

    public int SemesterId { get; set; }
    public Semester Semester { get; set; } = default!;

    //public ICollection<Major> Majors { get; set; } = new List<Major>();
}
