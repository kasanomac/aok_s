public class Major
{
    public required int Id { get; set; }
    public required string MajorName { get; set; }

      // 外部キー（Departmentへの参照）
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = default!;

    // 関連するClass（1つの専攻に複数の授業）
    //public ICollection<Class> Classes { get; set; } = new List<Class>();
}