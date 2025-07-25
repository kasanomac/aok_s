public class Class
{
    public int Id { get; set; }
    public string ClassName { get; set; }

    public string? Period { get; set; }
    public string? Teacher { get; set; }

    // Navigation property（中間テーブルへの）
    public ICollection<ClassMajor> ClassMajors { get; set; }
}