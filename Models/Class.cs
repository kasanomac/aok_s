public class Class
{
    public int Id { get; set; }
    public string ClassName { get; set; }

    // Navigation property（中間テーブルへの）
    public ICollection<ClassMajor> ClassMajors { get; set; } = new List<ClassMajor>();
}