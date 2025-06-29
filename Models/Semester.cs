public class Semester
{
    public int Id { get; set; } //SemesterId
    public string SemesterName { get; set; } = string.Empty; // 前期、後期
    public ICollection<Department> Departments { get; set; } = new List<Department>();
}
