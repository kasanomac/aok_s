using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aok_s.Models;

namespace aok_s.Areas.Identity.Data;

public class aok_sIdentityDbContext : IdentityDbContext<IdentityUser>
{
    public aok_sIdentityDbContext(DbContextOptions<aok_sIdentityDbContext> options)
        : base(options)
    {
    }

    public DbSet<Semester> Semesters { get; set; } = default!;
    public DbSet<ClassFormation> ClassFormations { get; set; } = default!;
    public DbSet<Department> Departments { get; set; } = default!;
    public DbSet<Major> Majors { get; set; } = default!;
    public DbSet<Class> Classes { get; set; } = default!;
    public DbSet<ClassMajor> ClassMajors { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Semester → ClassFormation（1対多）
        builder.Entity<ClassFormation>()
            .HasOne(cf => cf.Semester)
            .WithMany(s => s.ClassFormations)
            .HasForeignKey(cf => cf.SemesterId);

        // ClassFormation → Department（1対多）
        builder.Entity<Department>()
            .HasOne(d => d.ClassFormation)
            .WithMany(cf => cf.Departments)
            .HasForeignKey(d => d.ClassFormationId);

        // Department → Major（1対多）
        builder.Entity<Major>()
            .HasOne(m => m.Department)
            .WithMany(d => d.Majors)
            .HasForeignKey(m => m.DepartmentId);

        // ClassMajor（中間テーブル）
        builder.Entity<ClassMajor>()
            .HasKey(cm => new { cm.ClassId, cm.MajorId });

        builder.Entity<ClassMajor>()
            .HasOne(cm => cm.Class)
            .WithMany(c => c.ClassMajors)
            .HasForeignKey(cm => cm.ClassId);

        builder.Entity<ClassMajor>()
            .HasOne(cm => cm.Major)
            .WithMany(m => m.ClassMajors)
            .HasForeignKey(cm => cm.MajorId);
    }
}
