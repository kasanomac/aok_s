namespace aok_s.Models;
public class Semester
{
    public int Id { get; set; } //SemesterId
    public string SemesterName { get; set; } = string.Empty; // 前期、後期

    public ICollection<ClassFormation> ClassFormations { get; set; } = new List<ClassFormation>();
}
